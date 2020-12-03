using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeCheck : MonoBehaviour {
    Game_Controller g_tyep_script;
    Playercontroller g_playercontroller;
    Playermove g_potision_script;

    //プレイヤーの位置取得
    [SerializeField]
    private int g_check_v;
    [SerializeField]
    private int g_check_s;
    [SerializeField]
    private int g_check_h;
    Vector3 g_check_vsh;

    //ダイスの場所を探す
    public int g_dice_check_v;
    public int g_dice_check_s;
    public int g_dice_check_h;

    //g_tyep_scriptを入れる
    private int g_type_obj;
    //プレイヤー初期位置を取得用フラグ
    bool g_getdate = false;

    public bool g_v_plus_flag = false;
    public bool g_v_minus_flag = false;
    public bool g_s_plus_flag = false;
    public bool g_s_minus_flag = false;


    // Start is called before the first frame update
    void Start() {
        //プレイヤーの情報を取得
        g_playercontroller = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_potision_script = GameObject.FindGameObjectWithTag("Player").GetComponent<Playermove>();
        //ブロックのタイプを取得
        g_tyep_script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        //g_check_vsh = g_potision_script.Get_potision(g_check_v, g_check_s, g_check_h);
        g_getdate = true;

    }

    // Update is called once per frame
    void Update() {
        //プレイヤー初期位置取得
        if (g_getdate == true) {
            g_check_vsh = g_potision_script.Get_potision(g_check_v, g_check_s, g_check_h);
            g_type_obj = g_tyep_script.Get_Obj_Type(g_playercontroller.g_playerpointer_v, g_playercontroller.g_playerpointer_s, g_playercontroller.g_playerpointer_h);
            g_getdate = false;
            Get_p_p();
        }

        //if (g_v_plus_flag == true) {
        //    g_dicemove.GetComponent<Dicemove>().Move_dice_plus_v();
        //}
        //if (g_v_minus_flag == true) {
        //    g_dicemove.GetComponent<Dicemove>().Move_dice_minus_v();
        //}
        //if (g_s_plus_flag == true) {
        //    g_dicemove.GetComponent<Dicemove>().Move_dice_plus_s();
        //}
        //if (g_s_minus_flag == true) {
        //    g_dicemove.GetComponent<Dicemove>().Move_dice_minus_s();
        //}
    }
    //プレイヤーポジション取得
    public void Get_p_p() {
        //プレイヤーポジション取得
        g_check_vsh = g_potision_script.Get_potision(g_check_v, g_check_s, g_check_h);
        //プレイヤーV値取得
        g_dice_check_v = g_playercontroller.g_playerpointer_v;
        //プレイヤーS値取得
        g_dice_check_s = g_playercontroller.g_playerpointer_s;
        //プレイヤーH値取得
        g_dice_check_h = g_playercontroller.g_playerpointer_h;
        GetComponent<Playermove>().Movecheck();
    }
    public void TypeCheck_block() {

        #region 先にあるもののTypeCheck
        if (g_tyep_script.Get_Obj_Type(g_dice_check_v + 1, g_dice_check_s, g_dice_check_h) == 100) {
            Debug.Log("uedice");
            g_v_plus_flag = true;
        }
        if (g_tyep_script.Get_Obj_Type(g_dice_check_v - 1, g_dice_check_s, g_dice_check_h) == 100) {
            Debug.Log("sitadice");
            g_v_minus_flag = true;
        }
        if (g_tyep_script.Get_Obj_Type(g_dice_check_v, g_dice_check_s + 1, g_dice_check_h) == 100) {
            Debug.Log("migidice");
            g_s_plus_flag = true;
        }
        if (g_tyep_script.Get_Obj_Type(g_dice_check_v, g_dice_check_s - 1, g_dice_check_h) == 100) {
            Debug.Log("hidaridice");
            g_s_minus_flag = true;
        }

        #endregion
    }
}
