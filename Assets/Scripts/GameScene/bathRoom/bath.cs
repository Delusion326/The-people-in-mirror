using UnityEngine;

public class bath : MonoBehaviour
{

    private void OnMouseDown()
    {
        for (int i = 0; i < DataManager.Instance.playerItem.Count; i++)
        {
            if (DataManager.Instance.playerItem[i].id == 3)
            {
                return;
            }
        }
        UIManager.Instance.ShowMe<BathPanel>();
    }

    //private void Update()
    //{

    //    //if()
    //    //{
    //    //    UIManager.Instance.ShowMe<BathPanel>();
    //    //}
    //}
}
