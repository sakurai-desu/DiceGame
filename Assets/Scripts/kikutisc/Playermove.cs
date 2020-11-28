using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermove : MonoBehaviour {
    //GameObject g_pushstasts;

    Input_Date g_json_Script;
    Playercontroller g_potision_script;
    TypeCheck g_check_script;
    Game_Controller g_arrymovescript;
    Dice_Controller g_dice_con_Script;

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

    Game_Controller g_type_script;

    bool g_arrayflag = false;
    //player位置
    Vector3 g_sponplayer;
    //player位置読み込み用
    Vector3 g_playerpotision;
    [SerializeField]
    float g_cooltimer;

    //どのキーの方向に降りれるのかを判断するために使用するフラグ
    int g_down_rotatenum=100;
    const float g_coolresettimer = 0.2f;
    // Start is called before the first frame update
    void Start() {
        g_dice_con_Script = GameObject.Find("Dice_Controller").GetComponent<Dice_Controller>();
        g_arrymovescript = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_potision_script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_check_script = GameObject.FindGameObjectWithTag("Player").GetComponent<TypeCheck>();
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        //ブロックのタイプを取得
        g_type_script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        //g_pushscript = g_pushstasts.GetComponent<Game_Controller>();
        //配列を一度だけ読み込み用フラグ
        g_arrayflag = true;
        //Game_ControllerのBlockCountを読み込み
        g_createpointer_v = g_potision_script.g_playerpointer_v;
        g_createpointer_h = g_potision_script.g_playerpointer_h;
        g_createpointer_s = g_potision_script.g_playerpointer_s;
        (g_v_PBlockCount, g_s_PBlockCount, g_h_PBlockCount) = g_json_Script.Get_Array_Max();
        g_cooltimer = g_coolresettimer;
    }
    [SerializeField]
    bool g_arrayExistFlag;
    /// <summary>
    /// ｗボタンが押されたフラグ
    /// </summary>
    bool g_wflag;
    /// <summary>
    /// aボタンが押されたフラグ
    /// </summary>
    bool g_aflag;
    /// <summary>
    /// sボタンが押されたフラグ
    /// </summary>
    bool g_sflag;
    /// <summary>
    /// dボタンが押されたフラグ
    /// </summary>
    bool g_dflag;
    bool g_pushflag;
    public bool g_speaceflag;

    int g_loopnum;

    [SerializeField]
    bool g_h_seartchflag;
    // Update is called once per frame
    void Update() {

        if (g_arrayflag == true) {
            //Game_Controller.GetArrayから配列読み込み
            g_sponplayer = g_arrymovescript.Get_Pos(g_potision_script.g_playerpointer_v,
                g_potision_script.g_playerpointer_s, g_potision_script.g_playerpointer_h);
            //Debug.Log(g_sponplayer);
            g_arrayflag = false;
        }
        //if (g_potision_script.g_playerpointer_h > 1) {
        //    g_arrayExistFlag = true;
        //}
        #region 移動制御
        //配列hの上限に達してない時移動(上)
        if (g_potision_script.g_playerpointer_v < g_v_PBlockCount - 1 && g_cooltimer > 0) {

            if (g_speaceflag && Input.GetKeyDown(KeyCode.W)) {

                if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h) != 0 &&
                    g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h + 1) == 0) {
                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h)) {
                        case 0:

                            break;
                        case 20:
                            g_potision_script.g_playerpointer_v++;
                            g_potision_script.g_playerpointer_h++;
                            Player_Move();
                            break;
                        case 50:
                            g_potision_script.g_playerpointer_v++;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                        case 100:
                            g_potision_script.g_playerpointer_v++;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                    }
                }

            } else if (Input.GetKeyDown(KeyCode.W)) {
                HSearch(0);
               if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h - 1) != 0) {

                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h)) {
                        case 0:
                            g_potision_script.g_playerpointer_v++;
                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_v++;
                            Player_Move();
                            break;

                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 0);
                            //dicemove入れればいいんじゃね
                            break;
                    }
                }
                    //上から降りるときのスクリプト
                   else if (g_loopnum!=0) {

                        switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h)) {
                            case 0:
                                g_potision_script.g_playerpointer_v++;
                                g_potision_script.g_playerpointer_h=g_potision_script.g_playerpointer_h-g_loopnum;

                                Player_Move();
                                break;
                            case 20:
                                g_potision_script.g_playerpointer_v++;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;
                                Player_Move();
                                break;

                            case 100:
                                g_potision_script.g_playerpointer_v++;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;
                                Player_Move();
                                //dicemove入れなくてもいいんじゃね
                                break;
                        }
                    }
                
                PushKey();
                g_wflag = true;
            } else if (Input.GetKeyUp(KeyCode.W)) {
                UpKey();
                g_wflag = false;
            }

        }
        //配列hの下限に達してない時移動(下)
        if (g_potision_script.g_playerpointer_v > 0 && g_cooltimer > 0) {
            if (g_speaceflag && Input.GetKeyDown(KeyCode.S)) {
                if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h) != 0 &&
                    g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h + 1) == 0) {
                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h)) {
                        case 0:

                            break;
                        case 20:
                            g_potision_script.g_playerpointer_v--;
                            g_potision_script.g_playerpointer_h++;
                            Player_Move();
                            break;
                        case 50:
                            g_potision_script.g_playerpointer_v--;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                        case 100:
                            g_potision_script.g_playerpointer_v--;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                    }
                }
            } else if (Input.GetKeyDown(KeyCode.S)) {
                HSearch(1);
               if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h - 1) != 0) {

                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h)) {
                        case 0:
                            g_potision_script.g_playerpointer_v--;
                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_v--;
                            Player_Move();
                            break;
                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 1);
                            break;
                    }
                }
                //上から降りるときのスクリプト
                else if (g_loopnum != 0) {

                        switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h)) {
                            case 0:
                                g_potision_script.g_playerpointer_v--;
                                g_potision_script.g_playerpointer_h=g_potision_script.g_playerpointer_h - g_loopnum;

                                Player_Move();
                                break;
                            case 20:
                                g_potision_script.g_playerpointer_v--;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;
                                Player_Move();
                                break;
                            case 100:
                                g_potision_script.g_playerpointer_v--;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;

                                Player_Move();
                                break;
                        }
                    }
                
                PushKey();
                g_sflag = true;
            } else if (Input.GetKeyUp(KeyCode.S)) {
                UpKey();
                g_sflag = false;
            }
        }
        //配列vの下限に達してない時移動(左)
        if (g_potision_script.g_playerpointer_s > 0 && g_cooltimer > 0) {
            if (g_speaceflag && Input.GetKeyDown(KeyCode.A)) {
                if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h) != 0 &&
                    g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h + 1) == 0) {
                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h)) {
                        case 0:

                            break;
                        case 20:
                            g_potision_script.g_playerpointer_s--;
                            g_potision_script.g_playerpointer_h++;
                            Player_Move();
                            break;

                        case 50:
                            g_potision_script.g_playerpointer_s--;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                        case 100:
                            g_potision_script.g_playerpointer_s--;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                    }
                }
            } else if (Input.GetKeyDown(KeyCode.A)) {
                HSearch(2);
               if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h - 1) != 0) {

                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h)) {
                        case 0:
                            g_potision_script.g_playerpointer_s--;
                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_s--;
                            Player_Move();
                            break;
                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 3);
                            break;
                    }
                }
                    //上から降りるときのスクリプト
                   else if (g_loopnum != 0) {

                        switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h)) {
                            case 0:
                                g_potision_script.g_playerpointer_s--;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;

                                Player_Move();
                                break;
                            case 20:
                                g_potision_script.g_playerpointer_s--;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;
                                Player_Move();
                                break;
                            case 100:
                                g_potision_script.g_playerpointer_s--;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;

                                Player_Move();
                                break;
                        }
                    }
                PushKey();
                g_aflag = true;
            } else if (Input.GetKeyUp(KeyCode.A)) {
                UpKey();
                g_aflag = false;
            }
        }
        //配列vの上限に達してない時移動(右)
        if (g_potision_script.g_playerpointer_s < g_s_PBlockCount - 1 && g_cooltimer > 0) {

            if (g_speaceflag && Input.GetKeyDown(KeyCode.D)) {
                if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h) != 0 &&
                    g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h + 1) == 0) {
                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h)) {
                        case 0:

                            break;
                        case 20:
                            g_potision_script.g_playerpointer_s++;
                            g_potision_script.g_playerpointer_h++;
                            Player_Move();
                            break;
                        case 50:
                            g_potision_script.g_playerpointer_s++;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                        case 100:
                            g_potision_script.g_playerpointer_s++;
                            g_potision_script.g_playerpointer_h++;

                            Player_Move();
                            break;
                    }
                }
            } else if (Input.GetKeyDown(KeyCode.D)) {
                HSearch(3);
                    //上から降りるときのスクリプト
                    if (g_loopnum != 0) {

                        switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h)) {
                            case 0:
                                g_potision_script.g_playerpointer_s++;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;

                                Player_Move();
                                break;
                            case 20:
                                g_potision_script.g_playerpointer_s++;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;
                                Player_Move();
                                break;
                            case 100:
                                g_potision_script.g_playerpointer_s++;
                                g_potision_script.g_playerpointer_h = g_potision_script.g_playerpointer_h - g_loopnum;

                                Player_Move();
                                break;
                        }
                    }
               else if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h - 1) != 0) {

                    switch (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h)) {
                        case 0:
                            g_potision_script.g_playerpointer_s++;
                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_s++;
                            Player_Move();
                            break;
                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 2);
                            break;
                    }
                }
                PushKey();
                g_dflag = true;
            } else if (Input.GetKeyUp(KeyCode.D)) {
                UpKey();
                g_dflag = false;
            }
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.Space)) {
            PushKey();
            g_speaceflag = true;
        } else if (Input.GetKeyUp(KeyCode.Space)) {
            UpKey();
            g_speaceflag = false;
        }
        if (g_pushflag) {
            if (g_cooltimer > 0) {
                g_cooltimer -= Time.deltaTime;
            } else {
                g_cooltimer = g_coolresettimer;
            }
            Goal();
        }
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
    public Vector3 Get_potision(int v, int s, int h) {
        return g_playerpotision;
    }

    private void Goal() {
        int g_gettype = g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h - 1);
        Debug.Log("検索");
        if (g_gettype == 20) {
            Debug.Log("ゴールの上");
            //SceneManager.LoadScene("Goal");
        }
    }
    private void PushKey() {
        g_pushflag = true;
    }
    private void UpKey() {
        g_pushflag = false;
    }

    private int HSearch(int g_keynum) {
                g_loopnum = g_check_script.g_dice_check_h;
        //Debug.Log("やってるよ");
        switch (g_keynum) {
            case 0:
                for (int i= g_check_script.g_dice_check_h; i > 0; i--) {
                    g_loopnum--;

                    if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h - g_loopnum+1) != 0) {
                        g_down_rotatenum = 0;
                        Debug.Log(g_loopnum);
                        return g_loopnum;
                    }else if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v + 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h - g_loopnum) == 0) {
                        g_down_rotatenum = 100;
                        return g_loopnum;
                    }
                }
                break;
            case 1:
                //Debug.Log(g_loopnum);
                for (int i = g_check_script.g_dice_check_h; i > 0; i--) {

                    g_loopnum--;
                    if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h - g_loopnum+1) != 0) {
                        g_down_rotatenum = 1;
                        Debug.Log(g_loopnum);
                        return g_loopnum;
                    }
                    else if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v - 1, g_check_script.g_dice_check_s, g_check_script.g_dice_check_h - g_loopnum) == 0) {
                        g_down_rotatenum = 100;
                        return g_down_rotatenum;
                    }
                }
            break;
            case 2:
                for (int i = g_check_script.g_dice_check_h; i > 0; i--) {

                    g_loopnum--;
                    if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h - g_loopnum+1) != 0) {
                        g_down_rotatenum = 2;
                        Debug.Log(g_loopnum);
                        return g_loopnum;
                    }
                    else if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s - 1, g_check_script.g_dice_check_h - g_loopnum) == 0) {
                        g_down_rotatenum = 100;
                        return g_down_rotatenum;
                    }
                }
            break;
            case 3:
                for (int i = g_check_script.g_dice_check_h; i > 0; i--) {
                    g_loopnum--;
                    if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h - g_loopnum+1) != 0) {
                        g_down_rotatenum = 3;
                        Debug.Log(g_loopnum);
                        return g_loopnum;
                    }
                    else if (g_type_script.Get_Obj_Type(g_check_script.g_dice_check_v, g_check_script.g_dice_check_s + 1, g_check_script.g_dice_check_h - g_loopnum) == 0) {
                        g_down_rotatenum = 100;
                        return g_down_rotatenum;
                    }
                }
            break;
        }
        return g_down_rotatenum;
        

    }
}
