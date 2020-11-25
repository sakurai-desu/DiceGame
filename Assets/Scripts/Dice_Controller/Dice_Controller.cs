using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Controller : MonoBehaviour {
    private Dice_Squares g_dice_Script;
    private Dice_Rotate g_rotate_Script;

    private Parent_Dice g_parent_Script;
    private Parent_All_Rotation g_parent_rotate_Script;

    [SerializeField]
    private GameObject g_test_con_Obj;
    [SerializeField]
    private GameObject g_con_Obj;
    [SerializeField]
    private GameObject g_next_con_Obj;
    [SerializeField]
    private GameObject g_con_Obj_Parent;

    private int g_player_ver = 0;
    private int g_player_side = 0;
    private int g_player_high = 1;

    private const int g_ver_plus_Para = 0;
    private const int g_ver_minus_Para = 1;
    private const int g_side_plus_Para = 2;
    private const int g_side_minus_Para = 3;

    void Start() {
        g_parent_rotate_Script = GetComponent<Parent_All_Rotation>();
    }

    void Update() {
        //if (Input.GetKeyDown(KeyCode.A)) {
        //    g_con_Obj = g_test_con_Obj;
        //    g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        //    g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
        //}
    //    if (Input.GetKeyDown(KeyCode.A)) {
    //        g_con_Obj = g_test_con_Obj;
    //        g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
    //        g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
    //    }
        if (g_con_Obj == null) {
            return;
        }

        //回転中の間は回転させない
        if (g_rotate_Script.Get_Rotate_Flag()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            Move(g_side_minus_Para);
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            Move(g_side_plus_Para);
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(g_ver_plus_Para);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(g_ver_minus_Para);
        }
    }

    /// <summary>
    /// プレイヤーの指標を更新
    /// </summary>
    /// <param name="ver"></param>
    /// <param name="side"></param>
    /// <param name="high"></param>
    public void Change_Player_Pointer(int ver,int side,int high) {
        g_player_ver = ver;
        g_player_side = side;
        g_player_high = high;
    }

    /// <summary>
    /// サイコロを回転移動させる処理
    /// </summary>
    /// <param name="para"></param>
    private void Move(int para) {
        //回転の中心にしているサイコロの親を取得
        g_con_Obj_Parent = g_con_Obj.transform.parent.gameObject;
        //親のスクリプトを取得
        g_parent_Script = g_con_Obj_Parent.GetComponent<Parent_Dice>();
        switch (para) {
            case g_ver_plus_Para:
                g_next_con_Obj = g_parent_Script.Plus_Ver(g_player_ver, g_player_side, g_player_high);
                break;
            case g_ver_minus_Para:
                g_next_con_Obj = g_parent_Script.Minus_Ver(g_player_ver, g_player_side, g_player_high);
                break;
            case g_side_plus_Para:
                g_next_con_Obj = g_parent_Script.Plus_Side(g_player_ver, g_player_side, g_player_high);
                break;
            case g_side_minus_Para:
                g_next_con_Obj = g_parent_Script.Minus_Side(g_player_ver, g_player_side, g_player_high);
                break;
        }
        //回転の中心にするサイコロを変更
        g_con_Obj = g_next_con_Obj;
        //サイコロのスクリプト取得
        g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        //サイコロのスクリプト取得
        g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
        //サイコロを親オブジェクトごと回転させる処理
        g_parent_rotate_Script.All_Rotation(g_con_Obj, para);
    }

}
