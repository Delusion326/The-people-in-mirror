using UnityEngine;
using UnityEngine.UI;

public class ShowMirrorPanel : BasePanel
{
    public Image image;
    public Image playerImage;

    public Button btnExit;

    public float speed;

    public int maxNum;

    public override void Init()
    {
        btnExit.onClick.AddListener(() =>
        {
            GameLevelMgr.Instance.GameOver("触发真结局");
            GameLevelMgr.Instance.savePoint = 0;

            UIManager.Instance.HideMe<ShowMirrorPanel>();
        });
         
    }

    protected override void Update()
    {
        base.Update();

        if (image.GetComponent<RectTransform>().sizeDelta.x <= Vector2.one.x * 100 * maxNum)
            UpdateScale();
    }

    public void UpdateScale()
    {
        // 放大当前物体
        image.GetComponent<RectTransform>().sizeDelta += Vector2.one * speed * Time.deltaTime;
        playerImage.GetComponent<RectTransform>().sizeDelta += Vector2.one * speed * Time.deltaTime;
    }
}
