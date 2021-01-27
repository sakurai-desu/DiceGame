using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultChange : MonoBehaviour
{
    private Fade_In_Out g_fade_Script;
    //ステージを変更させるかさせないかを判断するためのタイマー
    float g_select_scene_timer =3f;

    private bool g_selectscene_flag;

    private void Start()
    {
        g_fade_Script = GameObject.Find("Fade_Image").GetComponent<Fade_In_Out>();
    }

    private void Update()
    {
        if (g_select_scene_timer > 0) {
            g_select_scene_timer -= Time.deltaTime;
        } else if (g_select_scene_timer < 0) {
            g_selectscene_flag = true;
        }
        if (g_selectscene_flag && Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A")) {
            g_fade_Script.Start_Fade_Out(ChangeScene());
        }
    }

    private IEnumerator ChangeScene() {
        SceneManager.LoadScene("SelectScene");
        yield break;
    }
}
