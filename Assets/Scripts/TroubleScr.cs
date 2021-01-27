using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TroubleScr : MonoBehaviour
{
    private Input_Date g_json_Script;
    private PlayerXbox g_xbox_con_Script;
    private PushStartScri g_menu_Script;
    private MainCamera_Move g_camera_move_Script;
    /// <summary>
    /// 手数
    /// </summary>
    public int g_troublenum = 10;
    /// <summary>
    /// チュートリアルがあるシーンの名前
    /// </summary>
    private string g_tutoName = "TutrialScene";
    [SerializeField]
    private GameObject g_gameover_UI=null;

    Text g_troublenumtext;

    public int g_max_trouble;

    private GameObject g_bgmObj;
    void Start()
    {
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        g_menu_Script = GameObject.Find("StartChackObj").GetComponent<PushStartScri>();
        g_camera_move_Script = GameObject.Find("MainCamera").GetComponent<MainCamera_Move>();
        g_bgmObj = GameObject.Find("BGM_Source");
        g_troublenumtext = GetComponent<Text>();
        g_troublenum = g_json_Script.g_inputJson.g_trouble;
        g_troublenumtext.text = g_troublenum.ToString();
        g_max_trouble = g_troublenum;
    }

    /// <summary>
    /// 手数をいじる
    /// </summary>
    public void Trouble() {
        if (SceneManager.GetActiveScene().name != g_tutoName) {
        g_troublenum--;
        g_troublenumtext.text = g_troublenum.ToString();
       
        if (g_troublenum <= 0) {
            Debug.Log("ゲームオーバー");
                g_bgmObj.SetActive(false);
            g_xbox_con_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXbox>();
            g_xbox_con_Script.enabled = false;
            g_menu_Script.enabled = false;
            g_camera_move_Script.enabled = false;

            g_gameover_UI.SetActive(true);
            return;
        }
        }
    }

    /// <summary>
    /// 現在の手数を1増やす
    /// </summary>
    public void Trouble_Plus() {
        g_troublenum++;
        g_troublenumtext.text = g_troublenum.ToString();
    }
}
