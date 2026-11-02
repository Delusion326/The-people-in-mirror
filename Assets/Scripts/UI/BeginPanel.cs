using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button btnStart;

    public Button btnExit;

    public Button btnSetting;

    protected override void Awake()
    {
        base.Awake();
        isShow = true;
    }

    public override void Init()
    {
        btnStart.onClick.AddListener(() =>
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync("GameScene_1_livingRoom");
            GameLevelMgr.Instance.savePoint = 1;

            ao.completed += (AsyncOperation obj) =>
            {
                GameObject player = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player/Player"));
                CinemachineVirtualCamera vc = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
                player.transform.SetParent(GameObject.Find("BornPos").transform,false);
                vc.Follow = player.transform;
                UIManager.Instance.ShowMe<GamePanel>();

                DialogManager.Instance.SetTextAsset(0);
                UIManager.Instance.ShowMe<DialogPanel>();
            };

        });

        btnExit.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        btnSetting.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowMe<SettingPanel>();
        });
    }

    public override void ShowMe()
    {
        base.ShowMe();
        PlayerObject.Instance.isMove = true;
    }
}
