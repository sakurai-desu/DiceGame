using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent_All_Rotation : MonoBehaviour {

    private Game_Controller g_game_Con_Script;
    private Dice_Squares g_child_Script;
<<<<<<< HEAD
    private Dice_Rotate g_rotate_Script;
    private Parent_Dice g_parent_Script;
    private Move_Check g_check_Script;
=======
    private Parent_Dice g_parent_Script;
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d

    /// <summary>
    /// 回転計算用オブジェクト配列
    /// </summary>
    private GameObject[,,] g_work_Objs_Array;
    private GameObject[] g_work_children;

    private GameObject g_center_Dice;
    private GameObject g_parent_center_Dice;

    private int g_array_max = 7;
    private int g_work_senter;

    /// <summary>
<<<<<<< HEAD
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_one_Count = 1;

    //回転の軸にするオブジェクトの指標
    /// <summary>
=======
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
    /// 回転の軸にするオブジェクトの縦の指標
    /// </summary>
    private int g_center_Ver;
    /// <summary>
    /// 回転の軸にするオブジェクトの横の指標
    /// </summary>
    private int g_center_Side;
    /// <summary>
    /// 回転の軸にするオブジェクトの高さの指標
    /// </summary>
    private int g_center_High;
<<<<<<< HEAD
    //オブジェクトの移動前の指標を格納する変数
    /// <summary>
    /// 移動前の指標：縦
    /// </summary>
    private int g_child_Ver;
    /// <summary>
    /// 移動前の指標：横
    /// </summary>
    private int g_child_Side;
    /// <summary>
    /// 移動前の指標：高さ
    /// </summary>
    private int g_child_High;
    //移動先の指標を格納する変数
    /// <summary>
    /// 移動先の指標：縦
    /// </summary>
    private int g_next_ver;
    /// <summary>
    /// 移動先の指標：横
    /// </summary>
    private int g_next_side;
    /// <summary>
    /// 移動先の指標：高さ
    /// </summary>
    private int g_next_high;

    private const int g_ver_plus_Para = 0;
    private const int g_ver_minus_Para = 1;
    private const int g_side_plus_Para = 2;
    private const int g_side_minus_Para = 3;

    private int g_now_para;

    private bool g_rotate_Flag = true;
    private bool g_ground_Flag = true;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_check_Script = this.GetComponent<Move_Check>();
        g_work_Objs_Array = new GameObject[g_array_max, g_array_max, g_array_max];
        g_work_senter = 3;
        //縦横高さを決めた数へ変更する
        //(g_max_Ver, g_max_Side, g_max_High) = g_game_Con_Script.Get_Array_Max();
    }

    public void All_Rotation(GameObject center_obj,int para) {
        //配列初期化
        Reset_Objs_Array();
        //回転の軸にするオブジェクトを保持
        g_center_Dice = center_obj;
        //回転の軸になるオブジェクトと同じ子オブジェクトを取得
        Get_Children();
        //現在のパラメータ保持
        g_now_para = para;
        switch (para) {
            case g_ver_plus_Para:
                Plus_Ver_Rotate();
                break;
            case g_ver_minus_Para:
                Minus_Ver_Rotate();
                break;
            case g_side_plus_Para:
                Plus_Side_Rotate();
                break;
            case g_side_minus_Para:
                Minus_Side_Rotate();
                break;
        }
        if (g_rotate_Flag&&g_ground_Flag) {
            g_rotate_Script = g_center_Dice.GetComponent<Dice_Rotate>();
            //オブジェクトを回転させる
            g_rotate_Script.This_Rotate(g_now_para);
        }
    }

=======
    [SerializeField]
    private int g_child_Ver;
    [SerializeField]
    private int g_child_Side;
    [SerializeField]
    private int g_child_High;

    [SerializeField]
    private int g_next_ver;
    [SerializeField]
    private int g_next_side;
    [SerializeField]
    private int g_next_high;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_work_Objs_Array = new GameObject[g_array_max, g_array_max, g_array_max];
        g_work_senter = 3;
    }
    
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
    /// <summary>
    /// 回転計算用3次元配列の初期化
    /// </summary>
    private void Reset_Objs_Array() {
<<<<<<< HEAD
        g_check_Script.Reset_Array();
        g_rotate_Flag = true;
        g_ground_Flag = true;
=======
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        for (int ver = 0; ver < g_array_max; ver++) {
            for (int side = 0; side < g_array_max; side++) {
                for (int high = 0; high < g_array_max; high++) {
                    g_work_Objs_Array[ver, side, high] = null;
                }
            }
        }
    }

    /// <summary>
<<<<<<< HEAD
    /// 縦プラス方向の回転処理
    /// </summary>
    /// <param name="center_obj"></param>
    private void Plus_Ver_Rotate() {
        //取得した子オブジェクトの個数分繰り返す
        for (int child_pointer = 0; child_pointer < g_work_children.Length; child_pointer++) {
            //子オブジェクトのスクリプト取得
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();
            //子オブジェクトの指標を取得
            (g_child_Ver, g_child_Side, g_child_High) = g_child_Script.Get_Dice_Pointer();
            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);
            //回転量計算
            (g_next_ver, g_next_high) = Plus_Rotate(g_next_ver, g_next_high);
            //回転先の指標計算
            Rotate_Pointer_Calc();

            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            g_check_Script.Retention_After_Pointer(g_next_ver + 1, g_child_Side, g_next_high);

            g_rotate_Flag = g_check_Script.Check(g_next_ver + 1, g_child_Side, g_next_high);
            if (!g_rotate_Flag) {
                break;
            }
            g_ground_Flag = g_check_Script.Center_Obj_Ground_Check(g_center_Ver+1, g_center_Side, g_center_High);
        }
    }
    /// <summary>
    /// 縦マイナス方向の回転処理
    /// </summary>
    /// <param name="center_obj"></param>
    private void Minus_Ver_Rotate() {
=======
    /// 横プラス方向の回転処理
    /// </summary>
    /// <param name="center_obj"></param>
    public void Plus_Side_Rotate(GameObject center_obj) {
        //配列初期化
        Reset_Objs_Array();
        //回転の中心にするオブジェクト取得
        (g_center_Ver, g_center_Side, g_center_High) = center_obj.GetComponent<Dice_Squares>().Get_Dice_Pointer();
        //回転の軸の親オブジェクト取得
        g_parent_center_Dice = center_obj.transform.parent.gameObject;
        //親スクリプト取得
        g_parent_Script = g_parent_center_Dice.GetComponent<Parent_Dice>();
        //親の子オブジェクトを格納した配列を取得
        g_work_children = g_parent_Script.Get_Children();

>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        //取得した子オブジェクトの個数分繰り返す
        for (int child_pointer = 0; child_pointer < g_work_children.Length; child_pointer++) {
            //子オブジェクトのスクリプト取得
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();
            //子オブジェクトの指標を取得
            (g_child_Ver, g_child_Side, g_child_High) = g_child_Script.Get_Dice_Pointer();
<<<<<<< HEAD
            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);
            //回転量計算
            (g_next_ver, g_next_high) = Minus_Rotate(g_next_ver, g_next_high);
            //回転先の指標計算
            Rotate_Pointer_Calc();

            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            g_check_Script.Retention_After_Pointer(g_next_ver - 1, g_child_Side, g_next_high);

            g_rotate_Flag = g_check_Script.Check(g_next_ver - 1, g_child_Side, g_next_high);
            if (!g_rotate_Flag) {
                break;
            }
            g_ground_Flag = g_check_Script.Center_Obj_Ground_Check(g_center_Ver - 1, g_center_Side, g_center_High);
        }
    }

    /// <summary>
    /// 横プラス方向の回転処理
    /// </summary>
    /// <param name="center_obj"></param>
    private void Plus_Side_Rotate() {
        //取得した子オブジェクトの個数分繰り返す
        for (int child_pointer = 0; child_pointer < g_work_children.Length; child_pointer++) {
            //子オブジェクトのスクリプト取得
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();
            //子オブジェクトの指標を取得
            (g_child_Ver, g_child_Side, g_child_High) = g_child_Script.Get_Dice_Pointer();
            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);
            //回転量計算
            (g_next_side, g_next_high) = Plus_Rotate(g_next_side, g_next_high);
            //回転先の指標計算
            Rotate_Pointer_Calc();

            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            g_check_Script.Retention_After_Pointer(g_child_Ver, g_next_side + 1, g_next_high);

            g_rotate_Flag = g_check_Script.Check(g_child_Ver, g_next_side + 1, g_next_high);
            if (!g_rotate_Flag ) {
                break;
            }
            g_ground_Flag = g_check_Script.Center_Obj_Ground_Check(g_center_Ver , g_center_Side+1, g_center_High);
        }
    }
    /// <summary>
    /// 横マイナス方向の回転処理
    /// </summary>
    /// <param name="center_obj"></param>
    private void Minus_Side_Rotate() {
=======
            Debug.Log("もともといた位置_"+g_child_Ver+"_"+ g_child_Side+"_"+ g_child_High);

            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);

            //回転量計算
            (g_next_side, g_next_high) = Plus_Rotate(g_next_side, g_next_high);
            //実際に格納している位置に回転量を足す
            //横の指標計算
            g_next_side = g_next_side + g_child_Side;
            //高さの指標計算
            g_next_high = g_next_high + g_child_High;
            Debug.Log("回転先_"+ g_child_Ver + "_" + g_next_side + "_" + g_next_high);

            //元々格納されていたオブジェクトを空にする
            g_game_Con_Script.Storage_Reset(g_child_Ver, g_child_Side, g_child_High);
            //子オブジェクトを回転に格納
            g_game_Con_Script.Storage_Obj(g_child_Ver, g_next_side + 1, g_next_high, g_work_children[child_pointer]);
            //タイプを回転先に格納
            g_game_Con_Script.Storage_Obj_Type(g_child_Ver, g_next_side + 1, g_next_high, 100);
            //子オブジェクトが保持している指標を更新する
            g_child_Script.Storage_This_Pointer(g_child_Ver, g_next_side + 1, g_next_high);
            //サイコロのマス目を変更
            g_child_Script.Side_Plus_Squears_Change();
        }
    }

    public void Minus_Side_Rotate(GameObject center_obj) {
        //配列初期化
        Reset_Objs_Array();
        //回転の中心にするオブジェクト取得
        (g_center_Ver, g_center_Side, g_center_High) = center_obj.GetComponent<Dice_Squares>().Get_Dice_Pointer();
        //回転の軸の親オブジェクト取得
        g_parent_center_Dice = center_obj.transform.parent.gameObject;
        //親スクリプト取得
        g_parent_Script = g_parent_center_Dice.GetComponent<Parent_Dice>();
        //親の子オブジェクトを格納した配列を取得
        g_work_children = g_parent_Script.Get_Children();

>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
        //取得した子オブジェクトの個数分繰り返す
        for (int child_pointer = 0; child_pointer < g_work_children.Length; child_pointer++) {
            //子オブジェクトのスクリプト取得
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();
            //子オブジェクトの指標を取得
            (g_child_Ver, g_child_Side, g_child_High) = g_child_Script.Get_Dice_Pointer();
<<<<<<< HEAD
            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);
            //回転量計算
            (g_next_side, g_next_high) = Minus_Rotate(g_next_side, g_next_high);
            //回転先の指標計算
            Rotate_Pointer_Calc();

            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            g_check_Script.Retention_After_Pointer(g_child_Ver, g_next_side - 1, g_next_high);

            g_rotate_Flag=g_check_Script.Check(g_child_Ver, g_next_side - 1, g_next_high);
            if (!g_rotate_Flag) {
                break;
            }
        }
        g_ground_Flag = g_check_Script.Center_Obj_Ground_Check(g_center_Ver, g_center_Side - 1, g_center_High);
    }

    /// <summary>
    /// 軸にするオブジェクトと同じ親のオブジェクトを取得
    /// </summary>
    private void Get_Children() {
        //回転の中心にするオブジェクト取得
        (g_center_Ver, g_center_Side, g_center_High) = g_center_Dice.GetComponent<Dice_Squares>().Get_Dice_Pointer();
        //回転の軸の親オブジェクト取得
        g_parent_center_Dice = g_center_Dice.transform.parent.gameObject;
        //親スクリプト取得
        g_parent_Script = g_parent_center_Dice.GetComponent<Parent_Dice>();
        //親の子オブジェクトを格納した配列を取得
        g_work_children = g_parent_Script.Get_Children();
    }
    /// <summary>
=======
            Debug.Log("もともといた位置_" + g_child_Ver + "_" + g_child_Side + "_" + g_child_High);

            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);

            //回転量計算
            (g_next_side, g_next_high) = Minus_Rotate(g_next_side, g_next_high);
            Debug.Log("_" + g_next_side + "_" + g_next_high);
            //実際に格納している位置に回転量を足す
            //横の指標計算
            g_next_side = g_next_side + g_child_Side;
            //高さの指標計算
            g_next_high = g_next_high + g_child_High;
            Debug.Log("回転先_" + g_child_Ver + "_" + g_next_side + "_" + g_next_high);

            //元々格納されていたオブジェクトを空にする
            g_game_Con_Script.Storage_Reset(g_child_Ver, g_child_Side, g_child_High);
            //子オブジェクトを回転に格納
            g_game_Con_Script.Storage_Obj(g_child_Ver, g_next_side - 1, g_next_high, g_work_children[child_pointer]);
            //タイプを回転先に格納
            g_game_Con_Script.Storage_Obj_Type(g_child_Ver, g_next_side - 1, g_next_high, 100);
            //子オブジェクトが保持している指標を更新する
            g_child_Script.Storage_This_Pointer(g_child_Ver, g_next_side - 1, g_next_high);
            //サイコロのマス目を変更
            g_child_Script.Side_Minus_Squears_Change();
        }
    }

    /// <summary>
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
    /// 回転計算用配列にオブジェクトを格納する
    /// </summary>
    /// <param name="child_pointer"></param>
    private void Storage_Work_Array(int child_pointer) {
        //回転計算用配列に格納するための指標を計算
        //子オブジェの現在の位置―回転の軸オブジェの位置＋回転計算配列の中心
        //縦の計算
        g_next_ver = g_child_Ver - g_center_Ver + g_work_senter;
        //横の計算
        g_next_side = g_child_Side - g_center_Side + g_work_senter;
        //高さの計算
        g_next_high = g_child_High - g_center_High + g_work_senter;
        //回転計算用配列に子オブジェクトを格納
        g_work_Objs_Array[g_next_ver, g_next_side, g_next_high] = g_work_children[child_pointer];
    }
<<<<<<< HEAD
    /// <summary>
    /// 移動前の指標に計算した移動量を足して移動先の指標計算
    /// </summary>
    private void Rotate_Pointer_Calc() {
        //実際に格納している位置に回転量を足す
        //縦の指標計算
        g_next_ver = g_next_ver + g_child_Ver;
        //横の指標計算
        g_next_side = g_next_side + g_child_Side;
        //高さの指標計算
        g_next_high = g_next_high + g_child_High;
    }
    /// <summary>
    /// 移動前を空にして移動先にオブジェクトとタイプを格納する処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <param name="pointer">子オブジェクト配列の指標</param>
    public void Reset_And_Storage_Obj() {
        for (int child_pointer = g_zero_Count; child_pointer < g_work_children.Length; child_pointer++) {
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();

            (g_child_Ver, g_child_Side, g_child_High) = g_check_Script.Get_Before_Pointer();
            (g_next_ver, g_next_side, g_next_high) = g_check_Script.Get_After_Pointer();

            //元々格納されていたオブジェクトを空にする
            g_game_Con_Script.Storage_Reset(g_child_Ver, g_child_Side, g_child_High);
            //子オブジェクトを回転に格納
            g_game_Con_Script.Storage_Obj(g_next_ver, g_next_side, g_next_high, g_work_children[child_pointer]);
            //タイプを回転先に格納
            g_game_Con_Script.Storage_Obj_Type(g_next_ver, g_next_side, g_next_high, 100);
            //子オブジェクトが保持している指標を更新する
            g_child_Script.Storage_This_Index(g_next_ver, g_next_side, g_next_high);
            //サイコロのマス目を変更
            g_child_Script.Change_Squares(g_now_para);
            g_child_Script.All_Check();
        }
    }
=======
>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d

    /// <summary>
    /// 現在の位置からの回転量を計算する処理
    /// </summary>
    /// <param name="now_pointer_ver_or_side">縦または横</param>
    /// <param name="now_pointer_high">高さ</param>
    /// <returns>縦または横、高さを返す</returns>
    private (int,int) Plus_Rotate(int now_pointer_ver_or_side, int now_pointer_high) {
        int high =6- now_pointer_ver_or_side;
        int ver_or_side = now_pointer_high;

        int move_high =high- now_pointer_high;
        int move_ver_or_side = ver_or_side- now_pointer_ver_or_side;
        return (move_ver_or_side, move_high);
    }
<<<<<<< HEAD
=======

>>>>>>> 0adec74d705f0e4511a91ce3c9e575709f1c6a8d
    /// <summary>
    /// 現在の位置からの回転量を計算する処理
    /// </summary>
    /// <param name="now_pointer_ver_or_side">縦または横</param>
    /// <param name="now_pointer_high">高さ</param>
    /// <returns>縦または横、高さを返す</returns>
    private (int, int) Minus_Rotate(int now_pointer_ver_or_side, int now_pointer_high) {
        int high =  now_pointer_ver_or_side;
        int ver_or_side = 6-now_pointer_high;

        int move_high = high - now_pointer_high;
        int move_ver_or_side = ver_or_side - now_pointer_ver_or_side;
        return (move_ver_or_side, move_high);
    }
}
