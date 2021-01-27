using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Clear : MonoBehaviour
{
    private PlayerXbox g_controller_Script;
    private PushStartScri g_menu_Script;
    private MainCamera_Move g_camera_move_Script;
    private Fade_In_Out g_fade_Script;

    //クリアしたときにタイマーを稼働させるためフラグ
    bool g_clear_flag;

    //クリアしたときにアニメーションで使うタイマー
    float g_clear_timer=4f;

    //クリア時のUI
    [SerializeField]
    GameObject g_clear_UI=null;

    void Start()
    {
        g_menu_Script = GameObject.Find("StartChackObj").GetComponent<PushStartScri>();
        g_camera_move_Script = GameObject.Find("MainCamera").GetComponent<MainCamera_Move>();
        g_fade_Script = GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>();
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
            g_fade_Script.Start_Fade_Out(Move_ResultScene());
        }
    }


    public void This_Stage_Clear() {
        g_clear_flag = true;
    }

    public void Move_Select() {
        g_fade_Script.Start_Fade_Out(Move_SelectScene());
    }

    public void Move_Tutrial() {
        g_fade_Script.Start_Fade_Out(Move_TutrialScene());
    }

    private IEnumerator Move_ResultScene() {
        SceneManager.LoadScene("Result");
        yield break;
    }

    private IEnumerator Move_SelectScene() {
        SceneManager.LoadScene("SelectScene");
        yield break;
    }

    private IEnumerator Move_TutrialScene() {
        SceneManager.LoadScene("TutrialScene");
        yield break;
    }
}
