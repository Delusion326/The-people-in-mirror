using UnityEngine;
using UnityEngine.UI;

public class ShowItemPanel : BasePanel
{
    //当前点击选中的物体
    private Transform nowSelObject;
    //选中的物体的图片
    public Image nowSelSprite;
    //展示道具的信息
    public Text itemInfo;
    //退出按钮
    public Button exitBtn;
    //放大速度
    public float speed;
    //放大倍率
    public int maxNum;

    public override void Init()
    {
        exitBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.HideMe<ShowItemPanel>();
        });

        //nowSelSprite.sprite = nowSelObject.GetComponent<SpriteRenderer>().sprite;
        //string iconName = nowSelObject.GetComponent<ItemObject>().keyItemObject.sprite;
        nowSelSprite.sprite = nowSelObject.GetComponent<ItemObject>().keyItemObject.sprite;
        itemInfo.text = nowSelObject.GetComponent<ItemObject>().keyItemObject.info;

    }


    protected override void Update()
    {
        base.Update();

        if(nowSelSprite.GetComponent<RectTransform>().sizeDelta.x <= Vector2.one.x * 100 * maxNum )
            UpdateScale();
    }

    public void UpdateScale()
    {
        // 放大当前物体
        nowSelSprite.GetComponent<RectTransform>().sizeDelta += Vector2.one * speed * Time.deltaTime;
        
    }

    public void SetObject(Transform obj)
    {
        nowSelObject = obj;
    }
}
