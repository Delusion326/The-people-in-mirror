using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public KeyItemObject keyItemObject;

    public GameObject outlineObject;

    public void Start()
    {
        for(int i = 0; i < DataManager.Instance.playerItem.Count; i++)
        {
            if (DataManager.Instance.playerItem[i].id == keyItemObject.id)
            {
                //如果玩家拥有这个道具 则隐藏场景中的这个道具
                this.gameObject.SetActive(false);
                break;
            }
        }
        //镜子
        if(this.keyItemObject.id == 13)
        {
            for (int i = 0;i<DataManager.Instance.playerItem.Count;i++)
            {
                if(DataManager.Instance.playerItem[i].id == 10)
                {
                    this.gameObject.SetActive(false);
                    break;
                }
            }
        }
        //工具盒
        if (this.keyItemObject.id == 15)
        {
            for (int i = 0; i < DataManager.Instance.playerItem.Count; i++)
            {
                if (DataManager.Instance.playerItem[i].id == 8 || DataManager.Instance.playerItem[i].id == 6)
                {
                    this.gameObject.SetActive(false);
                    break;
                }
            }
        }

    }


}
