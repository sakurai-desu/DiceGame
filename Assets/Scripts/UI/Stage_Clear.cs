using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Clear : MonoBehaviour
{
    //クリアしたときにタイマーを稼働させるためフラグ
    bool g_crear_flag;

    //クリアしたときにアニメーションで使うタイマー
    float g_crear_timer=2f;
    void Start()
    {
        
    }

    void Update()
    {
        if (g_crear_flag == true) {
            g_crear_timer -= Time.deltaTime;
        }
        if (g_crear_timer < 0) {
        Move_StageSelect();
        }
    }

    public void This_Stage_Clear() {
        g_crear_flag = true;
        //Move_StageSelect();
    }

    private void Move_StageSelect() {
        SceneManager.LoadScene("Result");
    }
}
