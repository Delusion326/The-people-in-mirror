using UnityEngine;

public class Monster : MonoBehaviour
{
    public int playerUsingItemId;

    private void Update()
    {
        playerUsingItemId = GameLevelMgr.Instance.playerUsingItemID;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (playerUsingItemId == 5)
        {
            this.gameObject.SetActive(false);
        }

        if (playerUsingItemId != 5)
        {
            GameLevelMgr.Instance.GameOver("触发梦中梦结局");
        }
    }
}
