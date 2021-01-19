using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parent_All_Rotation : MonoBehaviour {

    private Game_Controller g_game_Con_Script;
    private Dice_Squares g_child_Script;
    private Dice_Rotate g_rotate_Script;
    private Parent_Dice g_parent_Script;
    private Move_Check g_check_Script;
    private Dice_Fall g_dice_fall_Script;

    /// <summary>
    /// 回転計算用オブジェクト配列
    /// </summary>
    private GameObject[,,] g_work_Objs_Array;
    private GameObject[] g_work_children;

    private GameObject g_center_Dice;
    private GameObject g_parent_center_Dice;

    private int g_array_max = 9;
    private int g_work_senter = 4;

    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_one_Count = 1;

    //回転の軸にするオブジェクトの指標
    /// <summary>
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

    /// <summary>
    /// 縦のプラス方向のパラメータ
    /// </summary>
    private const int g_ver_plus_Para = 31;
    /// <summary>
    /// 縦のマイナス方向のパラメータ
    /// </summary>
    private const int g_ver_minus_Para = 33;
    /// <summary>
    /// 横のプラス方向のパラメータ
    /// </summary>
    private const int g_side_plus_Para = 30;
    /// <summary>
    /// 横のマイナス方向のパラメータ
    /// </summary>
    private const int g_side_minus_Para = 32;
    /// <summary>
    /// 現在のパラメータを保持する変数
    /// </summary>
    private int g_now_para;
    /// <summary>
    /// 回転させるかを判別するフラグ/True：回転できる/False：回転できない
    /// </summary>
    private bool g_is_rotate = true;
    /// <summary>
    /// 床があるか判別するフラグ/True：床がある/False：床がない
    /// </summary>
    private bool g_is_ground = true;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_check_Script = this.GetComponent<Move_Check>();
        g_dice_fall_Script = this.GetComponent<Dice_Fall>();
        //配列を生成
        g_work_Objs_Array = new GameObject[g_array_max, g_array_max, g_array_max];
    }

    public void All_Rotation(GameObject center_obj, int para) {
        //配列初期化
        Reset_Objs_Array();
        //回転の軸にするオブジェクトを保持
        g_center_Dice = center_obj;
        //回転の軸になるオブジェクトと同じ子オブジェクトを取得
        Get_Children();
        //g_undo_Script.Keep_Dice_Children(g_work_children,para);
        //現在のパラメータ保持
        g_now_para = para;
        //現在のパラメータに応じた処理をする
        switch (para) {
            //縦プラス方向
            case g_ver_plus_Para:
                Plus_Ver_Rotate();
                break;
            //縦マイナス方向
            case g_ver_minus_Para:
                Minus_Ver_Rotate();
                break;
            //横プラス方向
            case g_side_plus_Para:
                Plus_Side_Rotate();
                break;
            //横マイナス方向
            case g_side_minus_Para:
                Minus_Side_Rotate();
                break;
        }
        //g_is_ground=g_dice_fall_Script.Rotate_Check(g_work_children);
        //回転できる状態なら
        if (g_is_rotate && g_is_ground) {
            //回転の軸にするダイスの回転用スクリプト取得
            g_rotate_Script = g_center_Dice.GetComponent<Dice_Rotate>();
            //ダイスを回転させる
            g_rotate_Script.This_Rotate(g_now_para);
        }
    }

    /// <summary>
    /// 回転計算用3次元配列の初期化
    /// </summary>
    private void Reset_Objs_Array() {
        g_check_Script.Reset_Array();
        //回転フラグ初期化
        g_is_rotate = true;
        //床フラグ初期化
        g_is_ground = false;
        for (int ver = g_zero_Count; ver < g_array_max; ver++) {
            for (int side = g_zero_Count; side < g_array_max; side++) {
                for (int high = g_zero_Count; high < g_array_max; high++) {
                    g_work_Objs_Array[ver, side, high] = null;
                }
            }
        }
    }

    /// <summary>
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

            //移動前の位置を配列に格納して保持
            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            //移動後の位置を配列に格納して保持
            g_check_Script.Retention_After_Pointer(g_next_ver + 1, g_child_Side, g_next_high);
            //移動先が埋まっていないか調べる
            g_is_rotate = g_check_Script.Check(g_next_ver + 1, g_child_Side, g_next_high);
            if (!g_is_ground) {
                g_is_ground = g_check_Script.Obj_Ground_Check(g_next_ver + 1, g_child_Side, g_next_high, g_work_children[child_pointer]);
            }

            //移動先が埋まっていたら
            if (!g_is_rotate) {
                //処理中断
                break;
            }
        }
    }
    /// <summary>
    /// 縦マイナス方向の回転処理
    /// </summary>
    /// <param name="center_obj"></param>
    private void Minus_Ver_Rotate() {
        //取得した子オブジェクトの個数分繰り返す
        for (int child_pointer = 0; child_pointer < g_work_children.Length; child_pointer++) {
            //子オブジェクトのスクリプト取得
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();
            //子オブジェクトの指標を取得
            (g_child_Ver, g_child_Side, g_child_High) = g_child_Script.Get_Dice_Pointer();
            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);
            //回転量計算
            (g_next_ver, g_next_high) = Minus_Rotate(g_next_ver, g_next_high);
            //回転先の指標計算
            Rotate_Pointer_Calc();

            //移動前の位置を配列に格納して保持
            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            //移動後の位置を配列に格納して保持
            g_check_Script.Retention_After_Pointer(g_next_ver - 1, g_child_Side, g_next_high);
            //移動先が埋まっていないか調べる
            g_is_rotate = g_check_Script.Check(g_next_ver - 1, g_child_Side, g_next_high);
           
            if (!g_is_ground) {
                g_is_ground = g_check_Script.Obj_Ground_Check(g_next_ver - 1, g_child_Side, g_next_high, g_work_children[child_pointer]);
            }
            //移動先が埋まっていないか調べる
            if (!g_is_rotate ) {
                //処理中断
                break;
            }
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
            //移動前の位置を配列に格納して保持
            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            //移動後の位置を配列に格納して保持
            g_check_Script.Retention_After_Pointer(g_child_Ver, g_next_side + 1, g_next_high);
            //移動先が埋まっていないか調べる
            g_is_rotate = g_check_Script.Check(g_child_Ver, g_next_side + 1, g_next_high);
            if (!g_is_ground) {
            g_is_ground = g_check_Script.Obj_Ground_Check(g_child_Ver, g_next_side + 1, g_next_high, g_work_children[child_pointer]);
            }
            //移動先が埋まっていないか調べる
            if (!g_is_rotate ) {
                //処理中断
                break;
            }
        }
    }
    /// <summary>
    /// 横マイナス方向の回転処理
    /// </summary>
    /// <param name="center_obj"></param>
    private void Minus_Side_Rotate() {
        //取得した子オブジェクトの個数分繰り返す
        for (int child_pointer = 0; child_pointer < g_work_children.Length; child_pointer++) {
            //子オブジェクトのスクリプト取得
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();
            //子オブジェクトの指標を取得
            (g_child_Ver, g_child_Side, g_child_High) = g_child_Script.Get_Dice_Pointer();
            //配列に子オブジェクトを格納
            Storage_Work_Array(child_pointer);
            //回転量計算
            (g_next_side, g_next_high) = Minus_Rotate(g_next_side, g_next_high);
            //回転先の指標計算
            Rotate_Pointer_Calc();
            //移動前の位置を配列に格納して保持
            g_check_Script.Retention_Before_Pointer(g_child_Ver, g_child_Side, g_child_High);
            //移動後の位置を配列に格納して保持
            g_check_Script.Retention_After_Pointer(g_child_Ver, g_next_side - 1, g_next_high);
            //移動先が埋まっていないか調べる
            g_is_rotate = g_check_Script.Check(g_child_Ver, g_next_side - 1, g_next_high);
            if (!g_is_ground) {
                g_is_ground = g_check_Script.Obj_Ground_Check(g_child_Ver, g_next_side - 1, g_next_high, g_work_children[child_pointer]);

            }
            //移動先が埋まっていないか調べる
            if (!g_is_rotate ) {
                //処理中断
                break;
            }
        }
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
        //動かすダイスの個数分繰り返す
        for (int child_pointer = g_zero_Count; child_pointer < g_work_children.Length; child_pointer++) {
            //ダイスのスクリプト取得
            g_child_Script = g_work_children[child_pointer].GetComponent<Dice_Squares>();
            //移動前の位置を取得
            (g_child_Ver, g_child_Side, g_child_High) = g_check_Script.Get_Before_Pointer();
            //移動先の位置を取得
            (g_next_ver, g_next_side, g_next_high) = g_check_Script.Get_After_Pointer();

            //元々格納されていたオブジェクトを空にする
            g_game_Con_Script.Storage_Reset(g_child_Ver, g_child_Side, g_child_High);
            //子オブジェクトを回転に格納
            g_game_Con_Script.Storage_Obj(g_next_ver, g_next_side, g_next_high, g_work_children[child_pointer]);
            //タイプを回転先に格納
            g_game_Con_Script.Storage_Obj_Type(g_next_ver, g_next_side, g_next_high, 100);
            //移動先のポジション取得
            Vector3 pos = g_game_Con_Script.Get_Pos(g_next_ver, g_next_side, g_next_high);
            //ポジション移動
            g_work_children[child_pointer].transform.position = pos;
            //子オブジェクトが保持している指標を更新する
            g_child_Script.Storage_This_Index(g_next_ver, g_next_side, g_next_high);
            //サイコロのマス目を変更
            g_child_Script.Change_Squares(g_now_para);
            //ダイスの全方向を調べて、くっつくダイスがあるか調べる
            g_child_Script.All_Check();
        }
        //ダイス落下処理
        g_dice_fall_Script.All_Dice_Fall(g_work_children, g_center_Dice);
    }

    /// <summary>
    /// 現在の位置からの回転量を計算する処理
    /// </summary>
    /// <param name="now_pointer_ver_or_side">縦または横</param>
    /// <param name="now_pointer_high">高さ</param>
    /// <returns>縦または横、高さを返す</returns>
    private (int, int) Plus_Rotate(int now_pointer_ver_or_side, int now_pointer_high) {
        int high = g_array_max-g_one_Count - now_pointer_ver_or_side;
        int ver_or_side = now_pointer_high;

        int move_high = high - now_pointer_high;
        int move_ver_or_side = ver_or_side - now_pointer_ver_or_side;
        return (move_ver_or_side, move_high);
    }
    /// <summary>
    /// 現在の位置からの回転量を計算する処理
    /// </summary>
    /// <param name="now_pointer_ver_or_side">縦または横</param>
    /// <param name="now_pointer_high">高さ</param>
    /// <returns>縦または横、高さを返す</returns>
    private (int, int) Minus_Rotate(int now_pointer_ver_or_side, int now_pointer_high) {
        int high = now_pointer_ver_or_side;
        int ver_or_side = g_array_max - g_one_Count - now_pointer_high;

        int move_high = high - now_pointer_high;
        int move_ver_or_side = ver_or_side - now_pointer_ver_or_side;
        return (move_ver_or_side, move_high);
    }
}
