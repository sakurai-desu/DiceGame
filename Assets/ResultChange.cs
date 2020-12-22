using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultChange : MonoBehaviour
{
    //ステージを変更させるかさせないかを判断するためのタイマー
    float g_select_scene_timer=3f;

    private bool g_selectscene_flag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (g_select_scene_timer > 0) {
            g_select_scene_timer -= Time.deltaTime;
        } else if (g_select_scene_timer < 0) {
            g_selectscene_flag = true;
        }
        if (g_selectscene_flag && Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("A")) {
            ChangeScene();
        }
    }
    void ChangeScene() {
        SceneManager.LoadScene("SelectScene");
    }
}
