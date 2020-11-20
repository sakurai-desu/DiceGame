using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Dice : MonoBehaviour {
    private Input_Date g_json_Script;
    private Game_Controller g_game_Con_Script;

    private Dice_Squares g_next_dice_Script;

    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_one_Count = 1;

    //配列の要素数を取得する変数
    /// <summary>
    /// 縦の要素数
    /// </summary>
    private int g_max_Ver;
    /// <summary>
    /// 横の要素数
    /// </summary>
    private int g_max_Side;
    /// <summary>
    /// 高さの要素数
    /// </summary>
    private int g_max_High;

    //操作中のサイコロの指標
    /// <summary>
    /// サイコロの縦
    /// </summary>
    private int g_dice_Ver;
    /// <summary>
    /// サイコロの横
    /// </summary>
    private int g_dice_Side;
    /// <summary>
    /// サイコロの高さ
    /// </summary>
    private int g_dice_High;

    /// <summary>
    /// 動かしたオブジェクトの接触面パラメータ
    /// </summary>
    private int g_adhesive_para;
    /// <summary>
    /// 停止しているオブジェクトの接触面パラメータ
    /// </summary>
    private int g_next_adhesive_para;

    #region 接触面パラメータの定数

    /// <summary>
    /// 接触面が縦のプラス方向の時のパラメータ
    /// </summary>
    private const int g_ver_plus_para = 5;
    /// <summary>
    /// 接触面が縦のマイナス方向の時のパラメータ
    /// </summary>
    private const int g_ver_minus_para = 2;
    /// <summary>
    /// 接触面が横のプラス方向の時のパラメータ
    /// </summary>
    private const int g_side_plus_para = 3;
    /// <summary>
    /// 接触面が横のマイナス方向の時のパラメータ
    /// </summary>
    private const int g_side_minus_para = 1;
    /// <summary>
    /// 接触面が高さのプラス方向の時のパラメータ
    /// </summary>
    private const int g_high_plus_para = 0;
    /// <summary>
    /// 接触面が高さのマイナス方向の時のパラメータ
    /// </summary>
    private const int g_high_minus_para = 4;

    #endregion

    /// <summary>
    /// 動かしているサイコロ
    /// </summary>
    private GameObject g_dice_Obj;
    private GameObject g_next_dice_Obj;
    /// <summary>
    /// マス目の合計値
    /// </summary>
    private const int g_max_squares_sum = 7;

    void Start() {
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        //縦横高さの最大値をjsonで決めた数へ変更する
        (g_max_Ver, g_max_Side, g_max_High) = g_json_Script.Get_Array_Max();
    }

    /// <summary>
    /// 全ての方向を調べる
    /// </summary>
    public void Check_All_Direction(int this_ver, int this_side, int this_high, GameObject dice_Obj) {
        //チェックの中心になるオブジェクトの指標を保持
        //縦
        g_dice_Ver = this_ver;
        //横
        g_dice_Side = this_side;
        //高さ
        g_dice_High = this_high;
        //操作中オブジェクトを保持
        g_dice_Obj = dice_Obj;

        //縦のプラス方向を調べる
        Check_Ver_Plus();
        //縦のマイナス方向を調べる
        Check_Ver_Minus();
        //横のプラス方向を調べる
        Check_Side_Plus();
        //横のマイナス方向を調べる
        Check_Side_Minus();
        //高さのプラス方向を調べる
        Check_High_Plus();
        //高さのマイナス方向を調べる
        Check_High_Minus();
    }

    /// <summary>
    /// 縦のプラス方向をチェックする処理
    /// </summary>
    private void Check_Ver_Plus() {
        //チェックする位置が範囲外だった時処理を中断する
        if (g_dice_Ver + g_one_Count >= g_max_Ver) {
            return;
        }
        //格納されているオブジェクトのタイプを調べる
        Check_Type(g_dice_Ver + g_one_Count, g_dice_Side, g_dice_High, g_ver_plus_para);
    }

    /// <summary>
    /// 縦のマイナス方向をチェックする処理
    /// </summary>
    private void Check_Ver_Minus() {
        //チェックする位置が範囲外だった時処理を中断する
        if (g_dice_Ver - g_one_Count < g_zero_Count) {
            return;
        }
        //格納されているオブジェクトのタイプを調べる
        Check_Type(g_dice_Ver - g_one_Count, g_dice_Side, g_dice_High, g_ver_minus_para);
    }

    /// <summary>
    /// 横のプラス方向をチェックする処理
    /// </summary>
    private void Check_Side_Plus() {
        //チェックする位置が範囲外だった時処理を中断する
        if (g_dice_Side + g_one_Count >= g_max_Side) {
            return;
        }
        //格納されているオブジェクトのタイプを調べる
        Check_Type(g_dice_Ver, g_dice_Side + g_one_Count, g_dice_High, g_side_plus_para);
    }

    /// <summary>
    /// 横のマイナス方向をチェックする処理
    /// </summary>
    private void Check_Side_Minus() {
        //チェックする位置が範囲外だった時処理を中断する
        if (g_dice_Side - g_one_Count < g_zero_Count) {
            return;
        }
        //格納されているオブジェクトのタイプを調べる
        Check_Type(g_dice_Ver, g_dice_Side - g_one_Count, g_dice_High, g_side_minus_para);
    }

    /// <summary>
    /// 高さのプラス方向をチェックする処理
    /// </summary>
    private void Check_High_Plus() {
        //チェックする位置が範囲外だった時処理を中断する
        if (g_dice_High + g_one_Count >= g_max_High) {
            return;
        }
        //格納されているオブジェクトのタイプを調べる
        Check_Type(g_dice_Ver, g_dice_Side, g_dice_High + g_one_Count, g_high_plus_para);
    }

    /// <summary>
    /// 高さのマイナス方向をチェックする処理
    /// </summary>
    private void Check_High_Minus() {
        //チェックする位置が範囲外だった時処理を中断する
        if (g_dice_High - g_one_Count < g_zero_Count) {
            return;
        }
        //格納されているオブジェクトのタイプを調べる
        Check_Type(g_dice_Ver, g_dice_Side, g_dice_High - g_one_Count, g_high_minus_para);
    }

    /// <summary>
    /// 指定した位置に格納されているオブジェクトのタイプを取得する
    /// </summary>
    /// <param name="this_ver">縦</param>
    /// <param name="this_side">横</param>
    /// <param name="this_high">高さ</param>
    private void Check_Type(int this_ver, int this_side, int this_high, int change_para) {
        //タイプ取得
        int type = g_game_Con_Script.Get_Obj_Type(this_ver, this_side, this_high);
        //取得したタイプがサイコロだった時
        if (type == 100) {
            Debug.Log("縦：" + this_ver + "_横" + this_side + "_高さ" + this_high + "_ダイス発見");

            //接触された側のオブジェクトを取得
            g_next_dice_Obj = g_game_Con_Script.Get_Obj(this_ver, this_side, this_high);
            //接触された側のスクリプトを取得
            g_next_dice_Script = g_next_dice_Obj.GetComponent<Dice_Squares>();
            //移動側の接触面のパラメータを保持
            g_adhesive_para = change_para;
            //接触面を調べてくっつくか調べる
            Adhesive_Check();
        }
    }

    /// <summary>
    /// 接触したサイコロ同士の接触面を比較し接着するか調べる処理
    /// </summary>
    private void Adhesive_Check() {

        //移動側のサイコロの接触面に対応した処理をする
        switch (g_adhesive_para) {
            //接触面が縦のプラス方向の時
            case g_ver_plus_para:
                //停止側のパラメータを縦のマイナス方向のパラメータにする
                g_next_adhesive_para = g_ver_minus_para;
                break;
            //接触面が縦のマイナス方向の時
            case g_ver_minus_para:
                //停止側のパラメータを縦のプラス方向のパラメータにする
                g_next_adhesive_para = g_ver_plus_para;
                break;
            //接触面が横のプラス方向の時
            case g_side_plus_para:
                //停止側のパラメータを横のマイナス方向のパラメータにする
                g_next_adhesive_para = g_side_minus_para;
                break;
            //接触面が横のマイナス方向の時
            case g_side_minus_para:
                //停止側のパラメータを横のプラス方向のパラメータにする
                g_next_adhesive_para = g_side_plus_para;
                break;
            //接触面が高さのプラス方向の時
            case g_high_plus_para:
                //停止側のパラメータを高さのマイナス方向のパラメータにする
                g_next_adhesive_para = g_high_minus_para;
                break;
            //接触面が高さのマイナス方向の時
            case g_high_minus_para:
                //停止側のパラメータを高さのプラス方向のパラメータにする
                g_next_adhesive_para = g_high_plus_para;
                break;
        }

        //接触した側の接触面の数値を取得
        int g_dice_squares = g_dice_Obj.GetComponent<Dice_Squares>().Get_Array_Squares(g_adhesive_para);
        //接触された側の接触面の数値を取得
        int g_next_dice_squares = g_next_dice_Script.Get_Array_Squares(g_next_adhesive_para);
        //互いの数値の合計が7の時くっつく
        if (g_dice_squares + g_next_dice_squares == g_max_squares_sum) {
            Debug.Log("くっつきます");
            GameObject parent_Obj = g_dice_Obj.gameObject.transform.root.gameObject;
            //g_next_dice_Obj.transform.parent = parent_Obj.transform;
            Parent_Dice next_parent_Script = g_next_dice_Obj.transform.parent.GetComponent<Parent_Dice>();
            GameObject[] children = next_parent_Script.Get_Children();
            Debug.Log(children[0]);
            parent_Obj.GetComponent<Parent_Dice>().Parent_In_Child(children);
        }
    }
}
