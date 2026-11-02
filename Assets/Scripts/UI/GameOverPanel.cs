using DG.Tweening;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Text txtTitle;

    public Text txtHitTip;
    //标题显示速度
    public float titleSpeed = 2f;
    //闪烁速度
    public float fadeSpeed = 1f;

    public override void Init()
    {
        GameLevelMgr.Instance.isGameOver = true;

        txtTitle.DOFade(1f,titleSpeed);
        txtHitTip.DOFade(0f, fadeSpeed).SetLoops(-1, LoopType.Yoyo);
    }

    public void SetTip(string text)
    {
        txtTitle.text = text;
    }
}
