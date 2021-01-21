using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Clear : MonoBehaviour
{
    private PlayerXbox g_controller_Script;
    private PushStartScri g_menu_Script;
    private MainCamera_Move g_camera_move_Script;

    //クリアしたときにタイマーを稼働させるためフラグ
    bool g_clear_flag;

    //クリアしたときにアニメーションで使うタイマー
    float g_clear_timer=4f;

    //クリア時のUI
    [SerializeField]
    GameObject g_clear_UI;

    void Start()
    {
        g_menu_Script = GameObject.Find("StartChackObj").GetComponent<PushStartScri>();
        g_camera_move_Script = GameObject.Find("MainCamera").GetComponent<MainCamera_Move>();
    }

    void Update()
    {
        if (g_clear_flag == true) {

            g_controller_Script = GameObject.FindWithTag("Player").GetComponent<PlayerXbox>();
            g_controller_Script.enabled = false;
            g_menu_Script.enabled = false;
            g_camera_move_Script.enabled = false;

            g_clear_UI.SetActive(true);
            g_clear_timer -= Time.deltaTime;
        }
        if (g_clear_timer < 0) {
        Move_StageSelect();
        }
    }

    public void This_Stage_Clear() {
        g_clear_flag = true;
        //Move_StageSelect();
    }

    private void Move_StageSelect() {
        SceneManager.LoadScene("Result");
    }
}
