using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Controller : MonoBehaviour {
    private Playercontroller g_player_con_Script;
    private Dice_Squares g_dice_Script;
    private Dice_Rotate g_rotate_Script;
    private Parent_Dice g_parent_Script;
    private Parent_All_Rotation g_parent_rotate_Script;

    /// <summary>
    /// 回転の軸にするダイス
    /// </summary>
    private GameObject g_con_Obj;
    /// <summary>
    /// 次に回転の軸にするダイス
    /// </summary>
    private GameObject g_next_con_Obj;
    /// <summary>
    /// 回転の軸にしているダイスの親オブジェクト
    /// </summary>
    private GameObject g_con_Obj_Parent;
    [SerializeField]
    /// <summary>
    /// プレイヤーの指標：縦
    /// </summary>
    private int g_player_ver = 0;
    [SerializeField]
    /// <summary>
    /// プレイヤーの指標：横
    /// </summary>
    private int g_player_side = 0;
    [SerializeField]
    /// <summary>
    /// プレイヤーの指標：高さ
    /// </summary>
    private int g_player_high = 0;
    
    private const int g_ver_plus_Para = 31;
    private const int g_ver_minus_Para = 33;
    private const int g_side_plus_Para = 30;
    private const int g_side_minus_Para = 32;

    void Start() {
        g_player_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_parent_rotate_Script = GetComponent<Parent_All_Rotation>();
    }
    /// <summary>
    /// 軸にするダイスを中心にダイスを回転させる
    /// </summary>
    /// <param name="storage_obj">軸にするダイス</param>
    /// <param name="para">移動方向パラメータ</param>
    public void Storage_Control_Obj(GameObject storage_obj,int para) {
        //自分が保持しているプレイヤーのポインターを更新する
        (g_player_ver, g_player_side, g_player_high) = g_player_con_Script.Get_Player_Pointer();
        g_con_Obj = storage_obj;
        g_dice_Script = g_con_Obj.GetComponent<Dice_Squares>();
        g_rotate_Script = g_con_Obj.GetComponent<Dice_Rotate>();
        Move(para);
    }

    /// <summary>
    /// サイコロを回転移動させる処理
    /// </summary>
    /// <param name="para"></param>
    private void Move(int para) {
        //回転中の間は回転させない
        if (g_rotate_Script.Get_Rotate_Flag()) {
            return;
        }
        //回転の中心にしているサイコロの親を取得
        g_con_Obj_Parent = g_con_Obj.transform.parent.gameObject;
        //親のスクリプトを取得
        g_parent_Script = g_con_Obj_Parent.GetComponent<Parent_Dice>();
        //与えられたパラメータに応じて、回転の軸にするダイスを取得
        switch (para) {
            //縦プラス方向
            case g_ver_plus_Para:
                g_next_con_Obj = g_parent_Script.Get_Center_Ver_Plus(g_player_ver, g_player_side, g_player_high);
                break;
            //縦マイナス方向
            case g_ver_minus_Para:
                g_next_con_Obj = g_parent_Script.Get_Center_Ver_Minus(g_player_ver, g_player_side, g_player_high);
                break;
            //横プラス方向
            case g_side_plus_Para:
                g_next_con_Obj = g_parent_Script.Get_Center_Side_Plus(g_player_ver, g_player_side, g_player_high);
                break;
            //横マイナス方向
            case g_side_minus_Para:
                g_next_con_Obj = g_parent_Script.Get_Center_Side_Minus(g_player_ver, g_player_side, g_player_high);
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
