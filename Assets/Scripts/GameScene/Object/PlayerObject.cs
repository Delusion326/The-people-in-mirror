using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject instance;

    public static PlayerObject Instance => instance;

    private void Awake()
    {
        instance = this;

        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    //角色移动速度
    public float speed = 10;

    private Vector2 mousePos;

    public bool isMove = true;

    public Animator animator;

    public Rigidbody2D rig;

    //当前点击的物体
    public GameObject nowSelObject;

    private void Update()
    {

        HitObject();

        if(GameLevelMgr.Instance.playerUsingItemID == 6)
            this.GetComponentInChildren<Light2D>().intensity = 1;

    }

    private void FixedUpdate()
    {
        Move();
    }


    public void Move()
    {
        if (isMove)
        {
            //print("222");
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
            animator.SetBool("isMove", x != 0 || y != 0);

            rig.AddForce(new Vector2(x, y) * speed);
            //rig.velocity = new Vector2(this.transform.position.x,this.transform.position.y) * speed * Time.fixedDeltaTime;
            rig.velocity = new Vector2(x, y) * speed * Time.fixedDeltaTime;
            //this.transform.Translate(Vector3.right * speed * Time.deltaTime * x, Space.World);//前后
            //this.transform.Translate(Vector3.up * speed * Time.deltaTime * y, Space.World);//左右

        }
        else 
        {
            rig.velocity = new Vector2(0, 0) * speed * Time.fixedDeltaTime;
        }
    }

    public void HitObject()
    {
        //点击事件
        if (Input.GetMouseButtonDown(0))
        {
            //测试
            //DialogManager.Instance.SetTextAsset(2);
            ////UIManager.Instance.ShowMe<DialogPanel>();
            //UIManager.Instance.ShowMe<BathPanel>();
            //DataManager.Instance.SavePlayerItemData();

            //1---收集物品
            //将鼠标点转为世界坐标点
            if (UIManager.Instance.IsExitPanel() && !GameLevelMgr.Instance.isGameOver)
            {
                return;
            }
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //在世界坐标点原地检测 方向0
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            

            if (hit && hit.transform.CompareTag("Interable"))
            {
                print(hit.collider.name);
                nowSelObject = hit.collider.gameObject;
                //放大展示
                UIManager.Instance.ShowMe<ShowItemPanel>().SetObject(hit.collider.gameObject.transform);
                //添加进背包中
                int itemID = hit.collider.GetComponent<ItemObject>().keyItemObject.id;
                GameLevelMgr.Instance.AddPlayerItem(itemID);
                //并在场景中删除点击对象
                //删除物品的轮廓对象
                GameObject.Destroy(hit.collider.GetComponent<ItemObject>().outlineObject.gameObject);
                GameObject.Destroy(hit.collider.gameObject);
            }


            //2---可互动地点
            //点击后显示提示界面 并给提示界面传递提示信息
            if (hit && hit.transform.CompareTag("InterablePoint"))
            {
                print(hit.collider.name);
                nowSelObject = hit.collider.gameObject;
                //显示提示界面
                UIManager.Instance.ShowMe<TipPanel>().ShowTip(hit.collider.GetComponent<ItemObject>().keyItemObject.info);
                if (hit.collider.GetComponent<ItemObject>().keyItemObject.sceneName != null && hit.collider.GetComponent<ItemObject>().keyItemObject.isScene)
                {
                    switch (hit.collider.GetComponent<ItemObject>().keyItemObject.sceneName)
                    {
                        case "GameScene_1_livingRoom":
                            GameLevelMgr.Instance.savePoint = 4;
                            break;
                        case "GameScene_2_bathRoom":
                            GameLevelMgr.Instance.savePoint = 2;
                            break;
                        case "GameScene_3_kitchen":
                            GameLevelMgr.Instance.savePoint = 1;
                            break;
                        case "GameScene_4_bedRoom":
                            GameLevelMgr.Instance.savePoint = 3;
                            break;
                    }
                    Destroy(PlayerObject.Instance.gameObject);
                    AsyncOperation ao = SceneManager.LoadSceneAsync(hit.collider.GetComponent<ItemObject>().keyItemObject.sceneName);
                    ao.completed += (AsyncOperation obj) =>
                    {
                        GameObject player = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Player/Player"));
                        CinemachineVirtualCamera vc = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
                        vc.Follow = player.transform;
                        player.transform.SetParent(GameObject.Find("BornPos").transform, false);
                    };
                }
            }


            //3---游戏结束时隐藏游戏结束面板 并返回初始点
            if (GameLevelMgr.Instance.isGameOver == true)
            {
                //隐藏游戏结束面板 
                UIManager.Instance.HideMe<GameOverPanel>();
                //切换场景 并重置人物位置
                //将游戏结束判断改成 false
                GameLevelMgr.Instance.LoadNextLevel();
                GameLevelMgr.Instance.isGameOver = false;
            }

            //4--特殊处理——工具箱
            if (hit && hit.transform.CompareTag("toolBox"))
            {
                nowSelObject = hit.collider.gameObject;
                //显示提示界面
                UIManager.Instance.ShowMe<ToolBoxPanel>().SetObject(hit.collider.gameObject.transform);
            }

            //5--特殊处理——衣柜
            if (hit && hit.transform.CompareTag("closet"))
            {
                nowSelObject = hit.collider.gameObject;
                //显示提示界面
                UIManager.Instance.ShowMe<ClosetPanel>();
                //print("123");

                switch (GameLevelMgr.Instance.playerUsingItemID)
                {
                    case 8:
                        UIManager.Instance.ShowMe<TipPanel>().ShowTip("门开了");
                        UIManager.Instance.GetPanel<TipPanel>().btnExit.onClick.AddListener(UIManager.Instance.GetPanel<ClosetPanel>().AddGameItem);
                        break;
                    default:
                        UIManager.Instance.ShowMe<TipPanel>().ShowTip(PlayerObject.Instance.nowSelObject.GetComponent<ItemObject>().keyItemObject.info);
                        //print("123");
                        break;
                }
            }
            //6--特殊处理——mirror
            if (hit && hit.transform.CompareTag("mirror"))
            {
                nowSelObject = hit.collider.gameObject;
                //显示提示界面
                //如果使用锤子
                if (GameLevelMgr.Instance.playerUsingItemID == 6)
                {
                    GameObject mirror = new GameObject();
                    ItemObject mirrorItem = mirror.AddComponent<ItemObject>();
                    mirrorItem.keyItemObject = Resources.Load<KeyItemObject>("KeyItemInfo/ItemObject/mirror_break");
                    UIManager.Instance.ShowMe<ShowItemPanel>().SetObject(mirror.transform);

                    int itemID = mirror.GetComponent<ItemObject>().keyItemObject.id;
                    GameLevelMgr.Instance.AddPlayerItem(itemID);
                    Destroy(mirror);
                    UIManager.Instance.GetPanel<ShowItemPanel>().exitBtn.onClick.AddListener(MirrorBreakEnd);
                    Destroy(hit.collider.gameObject);
                }
                else
                {
                    //显示提示界面
                    UIManager.Instance.ShowMe<TipPanel>().ShowTip(hit.collider.GetComponent<ItemObject>().keyItemObject.info);
                }
            }

            //7 -- 特殊处理--大门
              if(hit && hit.transform.CompareTag("door"))
            {
                bool isHave = false;

                for(int i = 0; i < DataManager.Instance.playerItem.Count; i++)
                {
                    if(DataManager.Instance.playerItem[i].id == 10)
                    {
                        isHave = true;
                    }
                }

                if (!isHave)
                {
                    UIManager.Instance.ShowMe<TipPanel>().ShowTip(hit.collider.GetComponent<ItemObject>().keyItemObject.info);
                }
                else
                {
                    UIManager.Instance.ShowMe<ShowMirrorPanel>();
                }
               
            }


                if (hit)
            {
                nowSelObject = hit.collider.gameObject;
                Debug.Log("点击了物品" + hit.collider.gameObject.name);
            }

            

        }
    }

    private void MirrorBreakEnd()
    {
        GameLevelMgr.Instance.GameOver("触发梦中梦结局");
        UIManager.Instance.GetPanel<ShowItemPanel>().exitBtn.onClick.RemoveListener(MirrorBreakEnd);
    }
}
