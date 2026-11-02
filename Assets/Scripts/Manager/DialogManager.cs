using UnityEngine;

public class DialogManager
{
    private static DialogManager instance = new DialogManager();

    public static DialogManager Instance => instance;

    private DialogManager() { }

    //当前读取的文本
    public TextAsset textAsset;

    //当前对话的索引
    private int dialogIndex;

    //当对话的文本 按行分开
    string[] dialogRow;
    //对话面板
    public DialogPanel dialogPanel;
    //当前说话的人的名字
    public string dialogName;

    /// <summary>
    /// 从外界设置对话流程索引 通过索引获取对应文档 实现对话 
    /// 在任务管理器中调用
    /// </summary>
    /// <param name="index"></param>
    public void SetTextAsset(int index)
    {
        switch (index)
        {
            //开始游戏后的对话
            case 0:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_1");          
                break;
            //钟表的对话
            case 1:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_2");  
                break;
            case 2:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_3");
                break;
            case 3:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_4");
                break;
            case 4:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_5");
                break;
            case 5:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_6");
                break;
            case 6:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_7");
                break;
            case 7:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_8");
                break;
            case 8:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_9");
                break;
            case 9:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_10");
                break;
            case 10:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_11");
                break;
            case 11:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_12");
                break;
            case 12:
                textAsset = Resources.Load<TextAsset>("TextAsset/gameScene_13");
                break;
        }
        //Debug.Log(textAsset.name);
        ReadText();
    }
    

    public void ReadText()
    {
        dialogRow = textAsset.text.Split('\n');
        //Debug.Log(dialogRow.Length);
    }
    /// <summary>
    /// 用于一句话的快速调用
    /// </summary>
    /// <param name="text"></param>
    public void UpdataText(string text)
    {
        dialogPanel.dialogText.text = text;
    }

    //public void UpdataImage(string name , string position)
    //{
    //    if(int.Parse(position) == 0)
    //        dialogPanel.txtLeftName.text = name;

    //    if (int.Parse(position) == 1)
    //        dialogPanel.txtRightName.text = name;
    //}

    public void ShowDialogRow()
    {
        if(dialogPanel == null)
        dialogPanel = UIManager.Instance.ShowMe<DialogPanel>();

        for (int i = 1; i < dialogRow.Length; i++)
        {
            string[] row = dialogRow[i].Split(",");
            //Debug.Log(row[0]);

            if ( row[0]!= "end" &&  int.Parse(row[1]) == dialogIndex )
            {
                UpdataText(row[2]);
                dialogIndex = int.Parse(row[3]);
                break;
            }else if(row[0] == "end" && int.Parse(row[1]) == dialogIndex )
            {
                //Debug.Log("AAA");
                UIManager.Instance.HideMe<DialogPanel>();
                break ;
            }
            
        }
    }
    public void nextDialog()
    {
        if(Input.GetKeyDown(KeyCode.F) | 
           Input.GetKeyDown(KeyCode.Space)|
           Input.GetMouseButtonDown(0))
        {
            
            ShowDialogRow();
        }
    }

}
