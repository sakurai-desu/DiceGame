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

    private int[] g_before_Pointers;
    private int g_before;
    private int[] g_after_Pointers;
    private int g_after;

    private int g_get_before;
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

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_json_Script = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        (g_max_Ver, g_max_Side, g_max_High) = g_json_Script.Get_Array_Max();
        Reset_Array();
    }

    public void Reset_Array() {
        g_before_Pointers = new int[g_zero_Count];
        g_after_Pointers = new int[g_zero_Count];
        g_before = 0;
        g_after = 0;
        g_get_before = 0;
        g_get_after = 0;
    }

    public void Retention_Before_Pointer(int ver, int side, int high) {
        Array.Resize(ref g_before_Pointers, g_before_Pointers.Length + 3);
        g_before_Pointers[g_before] = ver;
        g_before_Pointers[g_before + 1] = side;
        g_before_Pointers[g_before + 2] = high;
        g_before = g_before + 3;
    }

    public void Retention_After_Pointer(int ver, int side, int high) {
        Array.Resize(ref g_after_Pointers, g_after_Pointers.Length + 3);
        g_after_Pointers[g_after] = ver;
        g_after_Pointers[g_after + 1] = side;
        g_after_Pointers[g_after + 2] = high;
        g_after = g_after + 3;
    }

    public (int, int, int) Get_Before_Pointer() {
        int ver = g_before_Pointers[g_get_before];
        int side = g_before_Pointers[g_get_before + 1];
        int high = g_before_Pointers[g_get_before + 2];
        g_get_before = g_get_before + 3;
        return (ver, side, high);
    }

    public (int, int, int) Get_After_Pointer() {
        int ver = g_after_Pointers[g_get_after];
        int side = g_after_Pointers[g_get_after + 1];
        int high = g_after_Pointers[g_get_after + 2];
        g_get_after = g_get_after + 3;
        return (ver, side, high);
    }

    public bool Check(int ver, int side, int high) {
        //移動先が配列の範囲外の時
        if (ver < g_zero_Count || g_max_Ver <= ver
            || side < g_zero_Count || g_max_Side <= side
                || high < g_zero_Count || g_max_High <= high) {
            //処理終了
            //回転させない
            return false;
        }
        int type = g_game_Con_Script.Get_Obj_Type(ver, side, high);
        if (type != 0) {
            //処理終了
            //回転させない
            return false;
        }
        return true;
    }

    public bool Center_Obj_Ground_Check(int ver, int side, int high) {
        high--;
        //移動先が配列の範囲外の時
        if (ver < g_zero_Count || g_max_Ver <= ver
            || side < g_zero_Count || g_max_Side <= side
                || high < g_zero_Count || g_max_High <= high) {
            //処理終了
            //回転させない
            return false;
        }
        int type = g_game_Con_Script.Get_Obj_Type(ver, side, high);
        if (type == 0) {
            return false;
        }
        return true;
    }
}
