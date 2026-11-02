using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{

    //专门用于控制面板透明度的组件
    private CanvasGroup canvasGroup;
    //淡入淡出的速度
    private float alphaSpeed = 10;
    //当前是隐藏还是显示
    public bool isShow = false;
    //当隐藏完毕后要做的事情
    private UnityAction hideCallBack = null;



    protected virtual void Awake()
    {
        //一开始获得组件
        canvasGroup = GetComponent<CanvasGroup>();
        //如果忘记添加canvasGroup组件
        if (canvasGroup == null)
            canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
    }

    public abstract void Init();

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Update()
    {
        //当处于显示状态时 如果透明度不为1 则加到1为止
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        //淡出
        else if (!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
                canvasGroup.alpha = 0;
            //让面板透明度淡出完成后 再执行的逻辑
            hideCallBack?.Invoke();
        }
    }

    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;

        isShow = true;
        PlayerObject.Instance.isMove = false;
    }

    public virtual void HideMe(UnityAction action)
    {
        isShow = false;
        PlayerObject.Instance.isMove = true;

        hideCallBack = action;
    }


}
