using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    //GameObject g_pushstasts;

    Input_Date g_json_Script;
    Playercontroller g_potision_script;
    TypeCheck g_check_script;
    Game_Controller g_arrymovescript;
    //Playercontrollerの生成位置取得
    [SerializeField]
    private int g_createpointer_v;
    [SerializeField]
    private int g_createpointer_s;
    [SerializeField]
    private int g_createpointer_h;
    //配列最大数取得用
    private int g_s_PBlockCount;

    private int g_v_PBlockCount;

    private int g_h_PBlockCount;


    bool g_arrayflag = false;
    //player位置
    Vector3 g_sponplayer;
    //player位置読み込み用
    Vector3 g_playerpotision;

    // Start is called before the first frame update
    void Start() {
        g_arrymovescript = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_potision_script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_check_script = GameObject.Find("Player(Clone)").GetComponent<TypeCheck>();
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        //g_pushscript = g_pushstasts.GetComponent<Game_Controller>();
        //配列を一度だけ読み込み用フラグ
        g_arrayflag = true;
        //Game_ControllerのBlockCountを読み込み
        g_createpointer_v = g_potision_script.g_playerpointer_v;
        g_createpointer_h = g_potision_script.g_playerpointer_h;
        g_createpointer_s = g_potision_script.g_playerpointer_s;
        (g_v_PBlockCount, g_s_PBlockCount, g_h_PBlockCount) = g_json_Script.Get_Array_Max();
    }

    // Update is called once per frame
    void Update() {

        if (g_arrayflag == true) {
            //Game_Controller.GetArrayから配列読み込み
            g_sponplayer = g_arrymovescript.Get_Pos(g_potision_script.g_playerpointer_v,
                g_potision_script.g_playerpointer_s, g_potision_script.g_playerpointer_h);
            //Debug.Log(g_sponplayer);
            g_arrayflag = false;
        }
        #region 移動制御
        //配列hの上限に達してない時移動(上)
        if (g_potision_script.g_playerpointer_v < g_v_PBlockCount - 1) {
            if (Input.GetKeyDown(KeyCode.W)) {
                g_potision_script.g_playerpointer_v++;
                Player_Move();
            }
        }
        //配列hの下限に達してない時移動(下)
        if (g_potision_script.g_playerpointer_v > 0) {
            if (Input.GetKeyDown(KeyCode.S)) {
                //g_playerpointer_v = g_playerpointer_v++;
                g_potision_script.g_playerpointer_v--;
                Player_Move();
            }

        }
        //配列vの下限に達してない時移動(左)
        if (g_potision_script.g_playerpointer_s > 0) {
            if (Input.GetKeyDown(KeyCode.A)) {
                g_potision_script.g_playerpointer_s--;
                Player_Move();
            }
        }
        //配列vの上限に達してない時移動(右)
        if (g_potision_script.g_playerpointer_s < g_s_PBlockCount - 1) {
            if (Input.GetKeyDown(KeyCode.D)) {
                g_potision_script.g_playerpointer_s++;
                Player_Move();
            }
        }
        #endregion
      
    }
    private void Player_Move() {
        g_sponplayer = g_arrymovescript.Get_Pos(g_potision_script.g_playerpointer_v, g_potision_script.g_playerpointer_s, g_potision_script.g_playerpointer_h);
        this.gameObject.transform.position = g_sponplayer;
        Player_potision();
    }

    void Player_potision() {
        g_playerpotision = g_sponplayer;
        g_check_script.GetComponent<TypeCheck>().Get_p_p();
    }
    public Vector3 Get_potision(int v,int s,int h) {
        return g_playerpotision;
    }

}
