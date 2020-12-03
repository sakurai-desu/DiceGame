using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermove : MonoBehaviour {
    //GameObject g_pushstasts;

    private bool g_floorflag = false;
    Input_Date g_json_Script;
    Playercontroller g_potision_script;
    TypeCheck g_check_script;
    Dice_Controller g_dice_con_Script;
    Player_Direction g_direction_Script;
    Game_Controller g_type_script;
    Playermove g_move_Script;
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

    public int g_movecheck_v;
    public int g_movecheck_s;
    public int g_movecheck_h;


    bool g_arrayflag = false;
    //player位置
    Vector3 g_sponplayer;
    //player位置読み込み用
    Vector3 g_playerpotision;
    [SerializeField]
    float g_cooltimer;

    //どのキーの方向に降りれるのかを判断するために使用するフラグ
    int g_down_rotatenum = 100;
    const float g_coolresettimer = 0.2f;

    void Start() {
        g_direction_Script = GameObject.Find("Player_Controller").GetComponent<Player_Direction>();
        g_dice_con_Script = GameObject.Find("Dice_Controller").GetComponent<Dice_Controller>();
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

    int g_dicemovenum;
    void Update() {

        if (g_arrayflag == true) {
            //Game_Controller.GetArrayから配列読み込み
            g_sponplayer = g_type_script.Get_Pos(g_potision_script.g_playerpointer_v,
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
                g_direction_Script.Player_Direction_Change(30);
                //プレイヤーのv方向の一個先
                int type_v_plus = g_type_script.Get_Obj_Type(g_movecheck_v + 1, g_movecheck_s, g_movecheck_h);
                //プレイヤーのv.h方向の一個先
                int type_v_h_plus = g_type_script.Get_Obj_Type(g_movecheck_v + 1, g_movecheck_s, g_movecheck_h + 1);

                if (type_v_plus != 0 && type_v_h_plus == 0) {
                    switch (type_v_plus) {
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
                //プレイヤーの向きを変更
                g_direction_Script.Player_Direction_Change(30);

                g_dicemovenum = HSearch(0);
                int type_vp_hm = g_type_script.Get_Obj_Type(g_movecheck_v + 1, g_movecheck_s, g_movecheck_h - 1);
                int type_v_p = g_type_script.Get_Obj_Type(g_movecheck_v + 1, g_movecheck_s, g_movecheck_h);
                int type_v_p_0= g_type_script.Get_Obj_Type(g_movecheck_v + 1, g_movecheck_s,0);
                if (type_vp_hm != 0) {

                    switch (type_v_p) {
                        case 0:
                            g_potision_script.g_playerpointer_v++;
                            Player_Move();
                            break;
                        //case 20:
                        //    g_potision_script.g_playerpointer_v++;
                        //    Player_Move();
                        //    break;

                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_movecheck_v, g_movecheck_s, g_movecheck_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_movecheck_v + 1, g_movecheck_s, g_movecheck_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 0);
                            //dicemove入れればいいんじゃね
                            break;
                    }
                }

                    //上から降りるときのスクリプト
                    else if (g_dicemovenum != 0 && type_v_p_0 != 0) {

                    switch (type_v_p) {
                        case 0:
                            g_potision_script.g_playerpointer_v++;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;

                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_v++;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;
                            Player_Move();
                            break;

                        case 100:
                            g_potision_script.g_playerpointer_v++;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;
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
                //プレイヤーの向きを変更
                g_direction_Script.Player_Direction_Change(32);

                int type_v_minus = g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s, g_movecheck_h);
                int type_vm_hp = g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s, g_movecheck_h + 1);
                if (type_v_minus != 0 && type_vm_hp == 0) {
                    switch (g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s, g_movecheck_h)) {
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
                //プレイヤーの向きを変更
                g_direction_Script.Player_Direction_Change(32);
                g_dicemovenum = HSearch(1);

                int type_vm_hm = g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s, g_movecheck_h - 1);
                int type_vm = g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s, g_movecheck_h);
                int type_vm_0 = g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s,0);
                if (type_vm_hm != 0) {

                    switch (type_vm) {
                        case 0:
                            g_potision_script.g_playerpointer_v--;
                            Player_Move();
                            break;
                        //case 20:
                        //    g_potision_script.g_playerpointer_v--;
                        //    Player_Move();
                        //    break;
                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_movecheck_v, g_movecheck_s, g_movecheck_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_movecheck_v - 1, g_movecheck_s, g_movecheck_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 1);
                            break;
                    }
                }
                 //上から降りるときのスクリプト
                 else if (g_dicemovenum != 0 && type_vm_0 != 0) {

                    switch (type_vm) {
                        case 0:
                            g_potision_script.g_playerpointer_v--;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;

                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_v--;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;
                            Player_Move();
                            break;
                        case 100:
                            g_potision_script.g_playerpointer_v--;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;

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
                //プレイヤーの向きを変更
                g_direction_Script.Player_Direction_Change(33);
                int type_sm = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s - 1, g_movecheck_h);
                int type_sm_hp = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s - 1, g_movecheck_h + 1);
                if (type_sm != 0 && type_sm_hp == 0) {
                    switch (type_sm) {
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
                //プレイヤーの向きを変更
                g_direction_Script.Player_Direction_Change(33);
                g_dicemovenum = HSearch(2);
                int type_sm = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s - 1, g_movecheck_h);
                int type_sm_hm = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s - 1, g_movecheck_h - 1);
                int type_sm_0 = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s - 1, 0);
                if (type_sm_hm != 0) {

                    switch (type_sm) {
                        case 0:
                            g_potision_script.g_playerpointer_s--;
                            Player_Move();
                            break;
                        //case 20:
                        //    g_potision_script.g_playerpointer_s--;
                        //    Player_Move();
                        //    break;
                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_movecheck_v, g_movecheck_s, g_movecheck_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_movecheck_v, g_movecheck_s - 1, g_movecheck_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 3);
                            break;
                    }
                }
                    //上から降りるときのスクリプト
                    else if (g_dicemovenum != 0 && type_sm_0 != 0) {

                    switch (type_sm) {
                        case 0:
                            g_potision_script.g_playerpointer_s--;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;

                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_s--;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;
                            Player_Move();
                            break;
                        case 100:
                            g_potision_script.g_playerpointer_s--;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;

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
                //プレイヤーの向きを変更
                g_direction_Script.Player_Direction_Change(31);
                int type_sp = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s + 1, g_movecheck_h);
                int type_sp_hp = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s + 1, g_movecheck_h + 1);
                if (type_sp != 0 && type_sp_hp == 0) {
                    switch (type_sp) {
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
                //プレイヤーの向きを変更
                g_direction_Script.Player_Direction_Change(31);
                g_dicemovenum = HSearch(3);
                int type_sp_hm = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s + 1, g_movecheck_h - 1);
                int type_sp = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s + 1, g_movecheck_h);
                int type_sp_0 = g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s + 1, 0);

                if (type_sp_hm != 0) {

                    switch (type_sp) {
                        case 0:
                            g_potision_script.g_playerpointer_s++;
                            Player_Move();
                            break;
                        //case 20:
                        //    g_potision_script.g_playerpointer_s++;
                        //    Player_Move();
                        //    break;
                        case 100:
                            g_dice_con_Script.Change_Player_Pointer(g_movecheck_v, g_movecheck_s, g_movecheck_h);
                            GameObject dice_obj = g_type_script.Get_Obj(g_movecheck_v, g_movecheck_s + 1, g_movecheck_h);
                            g_dice_con_Script.Storage_Control_Obj(dice_obj, 2);
                            break;
                    }
                }
                //上から降りるときのスクリプト

                else if (g_dicemovenum != 0 && type_sp_0 != 0) {
                    switch (type_sp) {
                        case 0:
                            g_potision_script.g_playerpointer_s++;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;

                            Player_Move();
                            break;
                        case 20:
                            g_potision_script.g_playerpointer_s++;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;
                            Player_Move();
                            break;
                        case 100:
                            g_potision_script.g_playerpointer_s++;
                            g_potision_script.g_playerpointer_h = g_dicemovenum;

                            Player_Move();
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
       
        g_sponplayer = g_type_script.Get_Pos(g_potision_script.g_playerpointer_v, g_potision_script.g_playerpointer_s, g_potision_script.g_playerpointer_h);
        this.gameObject.transform.position = g_sponplayer;
        Player_potision();
        Goal();
         Debug.Log(g_movecheck_v);
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
        //Debug.Log("検索");
        if (g_gettype == 20) {
            Debug.Log("ゴールの上");
            SceneManager.LoadScene("SelectScene");
        }
    }
    private void PushKey() {
        g_pushflag = true;
    }
    private void UpKey() {
        g_pushflag = false;
    }
    private int HSearch(int g_keynum) {
        g_loopnum = 0;
        g_floorflag = false;
        //Debug.Log("やってるよ");
        switch (g_keynum) {
            case 0:
                for (int i = g_potision_script.g_playerpointer_h; i > 0 || g_floorflag == true; i--) {
                    if (g_type_script.Get_Obj_Type(g_movecheck_v + 1, g_movecheck_s, i - 1) == 0) {

                        g_loopnum++;
                    } else if (g_type_script.Get_Obj_Type(g_movecheck_v + 1, g_movecheck_s, i - 1) != 0) {


                        //Debug.Log(g_loopnum);
                        g_loopnum = g_potision_script.g_playerpointer_h - g_loopnum;
                        //Debug.Log(g_loopnum);
                        return g_loopnum;
                    }
                }
                break;
            case 1:
                //Debug.Log(g_loopnum);
                for (int i = g_potision_script.g_playerpointer_h; i > 0 || g_floorflag == true; i--) {
                    if (g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s, i - 1) == 0) {

                        g_loopnum++;
                    } else if (g_type_script.Get_Obj_Type(g_movecheck_v - 1, g_movecheck_s, i - 1) != 0) {


                        //Debug.Log(g_loopnum);
                        g_loopnum = g_potision_script.g_playerpointer_h - g_loopnum;
                        //Debug.Log(g_loopnum);
                        return g_loopnum;
                    }
                }
                break;
            case 2:
                for (int i = g_potision_script.g_playerpointer_h; i > 0 || g_floorflag == true; i--) {
                    if (g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s - 1, i - 1) == 0) {

                        g_loopnum++;
                    } else if (g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s - 1, i - 1) != 0) {


                        //Debug.Log(g_loopnum);
                        g_loopnum = g_potision_script.g_playerpointer_h - g_loopnum;
                        //Debug.Log(g_loopnum);
                        return g_loopnum;
                    }
                }
                break;
            case 3:
                for (int i = g_potision_script.g_playerpointer_h; i > 0 || g_floorflag == true; i--) {
                    if (g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s + 1, i - 1) == 0) {

                        g_loopnum++;
                    } else if (g_type_script.Get_Obj_Type(g_movecheck_v, g_movecheck_s + 1, i - 1) != 0) {


                        //Debug.Log(g_loopnum);
                        g_loopnum = g_potision_script.g_playerpointer_h - g_loopnum;
                        //Debug.Log(g_loopnum);
                        return g_loopnum;
                    }
                }
                break;
        }

        g_loopnum = 0;
        return g_loopnum;

    }
    public void Movecheck() {
        g_movecheck_v = g_check_script.g_dice_check_v;
        g_movecheck_s = g_check_script.g_dice_check_s;
        g_movecheck_h = g_check_script.g_dice_check_h;
    }
}

