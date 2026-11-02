using UnityEngine.UI;

public class DialogPanel : BasePanel
{
    //对话人的名字
    public Text txtName;

    //当前显示的对话
    public Text dialogText;

    public override void Init()
    {
        DialogManager.Instance.ShowDialogRow();

    }

    protected override void Update()
    {
        base.Update();
        DialogManager.Instance.nextDialog();
    }
}
