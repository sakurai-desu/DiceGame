using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Move_Check : MonoBehaviour {

    private Input_Date g_json_Script;
    private Game_Controller g_game_Con_Script;

    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_one_Count = 1;

    /// <summary>
    /// 移動前の位置を保持する配列
    /// </summary>
    private int[] g_before_Pointers;
    /// <summary>
    /// 移動前を格納するための指標
    /// </summary>
    private int g_before;
    /// <summary>
    /// 移動前を取得するための指標
    /// </summary>
    private int g_get_before;
    /// <summary>
    /// 移動後の位置を保持する配列
    /// </summary>
    private int[] g_after_Pointers;
    /// <summary>
    /// 移動先を格納するための指標
    /// </summary>
    private int g_after;
    /// <summary>
    /// 移動先を取得するための指標
    /// </summary>
    private int g_get_after;

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

    /// <summary>
    /// 親オブジェクトが同じか判別するフラグ/True：同じ/False：同じではない
    /// </summary>
    private bool g_same_parent_flag = false;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        //ステージの配列の最大値を取得
        (g_max_Ver, g_max_Side, g_max_High) = g_json_Script.Get_Array_Max();
        //初期化
        Reset_Array();
    }

    /// <summary>
    /// 配列を変数の初期化
    /// </summary>
    public void Reset_Array() {
        g_before_Pointers = new int[g_zero_Count];
        g_after_Pointers = new int[g_zero_Count];
        g_before = g_zero_Count;
        g_after = g_zero_Count;
        g_get_before = g_zero_Count;
        g_get_after = g_zero_Count;
    }

    /// <summary>
    /// 移動する前の指標を保持する処理
    /// </summary>
    /// <param name="ver"></param>
    /// <param name="side"></param>
    /// <param name="high"></param>
    public void Retention_Before_Pointer(int ver, int side, int high) {
        //配列の要素数を3つ分（縦・横・高さ）サイズを増やす
        Array.Resize(ref g_before_Pointers, g_before_Pointers.Length + 3);
        //移動前の縦の指標を格納
        g_before_Pointers[g_before] = ver;
        //移動前の横の指標を格納
        g_before_Pointers[g_before + 1] = side;
        //移動前の高さの指標を格納
        g_before_Pointers[g_before + 2] = high;
        //指標を進める
        g_before = g_before + 3;
    }
    /// <summary>
    /// 移動した後の指標を保持しておく処理
    /// </summary>
    /// <param name="ver"></param>
    /// <param name="side"></param>
    /// <param name="high"></param>
    public void Retention_After_Pointer(int ver, int side, int high) {
        //配列の要素数を3つ分（縦・横・高さ）サイズを増やす
        Array.Resize(ref g_after_Pointers, g_after_Pointers.Length + 3);
        //移動後の縦の指標を格納
        g_after_Pointers[g_after] = ver;
        //移動後の横の指標を格納
        g_after_Pointers[g_after + 1] = side;
        //移動後の高さの指標を格納
        g_after_Pointers[g_after + 2] = high;
        //指標を進める
        g_after = g_after + 3;
    }

    /// <summary>
    /// 保持していた移動前の指標を返す処理
    /// </summary>
    /// <returns>移動前の縦・横・高さ</returns>
    public (int, int, int) Get_Before_Pointer() {
        //移動前の縦取得
        int ver = g_before_Pointers[g_get_before];
        //移動前の横取得
        int side = g_before_Pointers[g_get_before + 1];
        //移動前の高さ取得
        int high = g_before_Pointers[g_get_before + 2];
        //指標を進める
        g_get_before = g_get_before + 3;
        //縦・横・高さを返す
        return (ver, side, high);
    }
    /// <summary>
    /// 保持していた移動後の指標を返す処理
    /// </summary>
    /// <returns>移動後の縦・横・高さ</returns>
    public (int, int, int) Get_After_Pointer() {
        //移動後の縦取得
        int ver = g_after_Pointers[g_get_after];
        //移動後の横取得
        int side = g_after_Pointers[g_get_after + 1];
        //移動後の高さ取得
        int high = g_after_Pointers[g_get_after + 2];
        //指標を進める
        g_get_after = g_get_after + 3;
        //縦・横・高さを返す
        return (ver, side, high);
    }

    /// <summary>
    /// 回転できるかを調べる処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <returns>True：回転できる/False：回転できない</returns>
    public bool Check(int ver, int side, int high) {
        //移動先が配列の範囲外の時
        if (ver < g_zero_Count || g_max_Ver <= ver
            || side < g_zero_Count || g_max_Side <= side
                || high < g_zero_Count || g_max_High <= high) {
            //処理終了
            //回転させない状態を返す
            return false;
        }
        //検索位置に格納されているオブジェクトのタイプ取得
        int type = g_game_Con_Script.Get_Obj_Type(ver, side, high);
        //移動先が埋まっていた時
        if (type != 0) {
            //処理終了
            //回転させない状態を返す
            return false;
        }
        //回転できる状態を返す
        return true;
    }

    /// <summary>
    /// 与えられた引数の一つ下に床があるか調べる処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <returns>True：床が有る/False：床が無い</returns>
    public bool Obj_Ground_Check(int ver, int side, int high,GameObject dice_obj) {
        //落下先が埋まるか、配列の範囲外になるまで繰り返す
        for (int pointer = high - 1; pointer >= g_zero_Count; pointer--) {
            //同じ親フラグ初期化
            g_same_parent_flag = false;
            //検索先に埋まっているオブジェクトのタイプ取得
            int type = g_game_Con_Script.Get_Obj_Type(ver, side, pointer);
            //検索先がダイスの時、ダイスが検索用ダイスと同じ階層にあるか調べる
            if (type == 100) {
                //検索用ダイスの親オブジェクトを取得
                GameObject dice_parent = dice_obj.transform.parent.gameObject;
                //検索先のダイス取得
                GameObject next_dice = g_game_Con_Script.Get_Obj(ver, side, pointer);
                //検索先のダイスの親オブジェクトを取得
                GameObject next_parent = next_dice.transform.parent.gameObject;
                //ダイスが検索用ダイスと同じ階層にあるか調べる
                if (dice_parent == next_parent) {
                    //同じならフラグON
                    g_same_parent_flag = true;
                }
            }
            //移動先オブジェクトが空白ではないなら（床があるなら）
            if (type != 0 && !g_same_parent_flag) {
                //処理終了
                //床がある状態を返す
                return true;
            }
        }
        return false;
    }
}
