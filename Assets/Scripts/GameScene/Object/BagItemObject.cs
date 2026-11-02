using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItemObject : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;

    public string itemInfo;

    public int itemID;

    public bool isScene;
    public string toSceneName;

    //物品被选中时的提示对象
    public string toSceneTip;

    public GameObject selectTips1;
    public GameObject selectTips2;

    public Button btnToScene;

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("点击了物品");
        UIManager.Instance.GetPanel<BagPanel>().txtName.text = itemName;
        UIManager.Instance.GetPanel<BagPanel>().txtItemInfo.text = itemInfo;
        UIManager.Instance.GetPanel<BagPanel>().btnToScene.gameObject.SetActive(isScene);
        switch (itemName) 
        {
            case "日历_2":
                toSceneTip = "前往浴室";
                break;
            case "日历_3":
                toSceneTip = "前往卧室";
                break;
            case "日历_4":
                toSceneTip = "前往客厅";
                break;
        }

        UIManager.Instance.GetPanel<BagPanel>().btnToScene.GetComponentInChildren<Text>().text = toSceneTip; 
        UIManager.Instance.GetPanel<BagPanel>().btnUseItem.gameObject.SetActive(!isScene);
        UIManager.Instance.GetPanel<BagPanel>().nowSelObject = this;

        selectTips1.SetActive(true);
        selectTips2.SetActive(true);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("鼠标进入了物品");
        UIManager.Instance.GetPanel<BagPanel>().txtName.text = itemName;
        UIManager.Instance.GetPanel<BagPanel>().txtItemInfo.text = itemInfo;
        UIManager.Instance.GetPanel<BagPanel>().btnToScene.gameObject.SetActive(isScene);
        UIManager.Instance.GetPanel<BagPanel>().btnUseItem.gameObject.SetActive(!isScene);
        switch (itemName)
        {
            case "日历_2":
                toSceneTip = "前往浴室";
                break;
            case "日历_3":
                toSceneTip = "前往卧室";
                break;
            case "日历_4":
                toSceneTip = "前往客厅";
                break;
        }

        UIManager.Instance.GetPanel<BagPanel>().btnToScene.GetComponentInChildren<Text>().text = toSceneTip;
        UIManager.Instance.GetPanel<BagPanel>().nowSelObject = this;
        selectTips1.SetActive(true);
        selectTips2.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
       //Debug.Log("鼠标离开了物品");
        selectTips1.SetActive(false);
        selectTips2.SetActive(false);
    }
}
