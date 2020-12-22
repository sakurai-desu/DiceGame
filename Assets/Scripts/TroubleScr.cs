using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroubleScr : MonoBehaviour
{
    private PlayerXbox g_xbox_con_Script;
    private MainCamera_Move g_camera_move_Script;
    /// <summary>
    /// 手数
    /// </summary>
    public int g_troublenum = 10;
    [SerializeField]
    private GameObject g_gameover_UI;

    Text g_troublenumtext;

    public int g_max_trouble;
    void Start()
    {
        g_camera_move_Script = GameObject.Find("MainCamera").GetComponent<MainCamera_Move>();
        g_troublenumtext = GetComponent<Text>();
        g_troublenumtext.text = g_troublenum.ToString();
        g_max_trouble = g_troublenum;
    }

    /// <summary>
    /// 手数をいじる
    /// </summary>
    public void Trouble() {
        g_troublenum--;
        g_troublenumtext.text = g_troublenum.ToString();
        if (g_troublenum <= 0) {
            Debug.Log("ゲームオーバー");
            g_xbox_con_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXbox>();
            g_xbox_con_Script.enabled = false;
            g_camera_move_Script.enabled = false;
            g_gameover_UI.SetActive(true);
            return;
        }
    }
}
