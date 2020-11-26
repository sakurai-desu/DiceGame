using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Create : MonoBehaviour {

    private int[] g_default_dices = { 1, 4, 2, 3, 6, 5 };
    //private int[] g_work_dices = new int[6];
    //private int[] g_json_dices = new int[6];
    [SerializeField]
    private int[] g_work_dices;
    [SerializeField]
    private int[] g_work_Array;
    [SerializeField]
    private int[] g_json_dices;
    //private int[] g_json_dices = { 4, 5, 6, 2, 3, 1 };

    [SerializeField]
    /// <summary>
    /// 回転させるオブジェクト
    /// </summary>
    private GameObject g_dice_Obj;
    /// <summary>
    /// 回転の中心
    /// </summary>
    private Vector3 g_rotate_Point = Vector3.zero;
    /// <summary>
    /// 回転の軸
    /// </summary>
    private Vector3 g_rotate_Axis = Vector3.zero;
    /// <summary>
    /// 回転させる最大値
    /// </summary>
    private float g_rotation_Max = 90;
    /// <summary>
    /// サイコロのサイズ
    /// </summary>
    private float g_dice_Size;

    [SerializeField]
    private int g_ver_count;
    [SerializeField]
    private int g_side_count;
    [SerializeField]
    private int g_high_count;

    private int g_ver_check=0;
    private int g_side_check=1;
    private int g_high_check=2;

    private bool g_check_flag = false;

    void Start() {
        g_work_dices = new int[6];
        g_work_Array = new int[6];
        g_json_dices = new int[6];
        Reset_Array();
    }

    void Update() {
        //if (Input.GetKeyDown(KeyCode.Q)) {
        //    //サイズを求める
        //    g_dice_Size = g_dice_Obj.transform.localScale.x;
        //    g_check_flag = false;
        //    while (true) {
        //        Ver_Check();
        //        if (g_check_flag) {
        //            break;
        //        }
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.W)) {
        //    Ver_Check();
        //}
        //if (Input.GetKeyDown(KeyCode.E)) {
        //    Side_Check();
        //}
        //if (Input.GetKeyDown(KeyCode.R)) {
        //    High_Check();
        //}
    }

    public void Dice_Squares_Change(GameObject dice_obj,int[] json_dices) {
        Get_Json_Dice(json_dices);
        Reset_Array();
        g_dice_Obj = dice_obj;
        //サイズを求める
        g_dice_Size = g_dice_Obj.transform.localScale.x;
        g_check_flag = false;
        for (int i = 0; i < 4; i++) {
            Reset_Array();
            Ver_Check();
            if (g_check_flag) {
                g_check_flag = false;
                break;
            }
        }
    }

    private void Check_Point_Change() {
        //int work = g_ver_check;
        //g_ver_check = g_side_check;
        //g_side_check = g_high_check;
        //g_high_check = work;
        //g_ver_check = 2;
        //g_side_check = 1;
        //g_high_check = 2;
    }

    private void Dice_Rotate() {
        //回転の中心を決める
        g_rotate_Point = g_dice_Obj.transform.position;
        //軸と中心を元に回転させる
        g_dice_Obj.transform.RotateAround(g_rotate_Point, g_rotate_Axis, g_rotation_Max);
    }

    private void Ver_Check() {
        g_ver_count = 0;
        //回転の軸を決める
        g_rotate_Axis = new Vector3(-1, 0, 0);
        for (int i = 0; i < 4; i++) {
            Ver_Squeares_Change();
            g_ver_count++;
            Dice_Rotate();
            if (g_ver_count > 3) {
                Debug.Log("一致していない");
                Side_Check();
                break;
            }
            if (g_work_dices[g_ver_check] == g_json_dices[g_ver_check]) {
                Debug.Log("一致した");
                Squares_All_Check();
                break;
            }
        }
    }
    private void Side_Check() {
        g_side_count = 0;
        //回転の軸を決める
        g_rotate_Axis = new Vector3(0, 0, -1);
        for (int i = 0; i < 4; i++) {
            Side_Squeares_Change();
            g_side_count++;
            Dice_Rotate();
            if (g_side_count > 3) {
                Debug.Log("一致していない");
                High_Check();
                break;
            }
            if (g_work_dices[g_side_check] == g_json_dices[g_side_check]) {
                Debug.Log("一致した");
                Squares_All_Check();
                break;
            }
        }
    }
    private void High_Check() {
        g_high_count = 0;
        //回転の軸を決める
        g_rotate_Axis = new Vector3(0, 1, 0);
        for (int i = 0; i < 4; i++) {
            High_Squeares_Change();
            g_high_count++;
            Dice_Rotate();
            if (g_high_count > 3) {
                Debug.Log("一致していない");
                break;
            }
            if (g_work_dices[g_high_check] == g_json_dices[g_high_check]) {
                Debug.Log("一致した");
                Squares_All_Check();
                break;
            }
        }
    }

    /// <summary>
    /// 入れ替え中のマス目とJsonのマス目を比較してすべてが一致しているか判別する
    /// </summary>
    private void Squares_All_Check() {
        for (int i = 0; i < g_default_dices.Length; i++) {
            if (g_work_dices[i] != g_json_dices[i]) {
                Debug.Log("全てが一致しきっていない");
                Check_Point_Change();
                break;
            }
            if (i >= g_default_dices.Length - 1) {
                Debug.Log("全てが一致しました");
                g_check_flag = true;
            }
        }
    }

    private void Ver_Squeares_Change() {
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_work_dices[2] = g_work_Array[0];
        g_work_dices[4] = g_work_Array[2];
        g_work_dices[5] = g_work_Array[4];
        g_work_dices[0] = g_work_Array[5];
    }
    private void Side_Squeares_Change() {
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_work_dices[0] = g_work_Array[3];
        g_work_dices[3] = g_work_Array[4];
        g_work_dices[4] = g_work_Array[1];
        g_work_dices[1] = g_work_Array[0];
    }
    private void High_Squeares_Change() {
        //現在のマス目を保持
        Retention_Squares();
        //マス目入れ替え
        g_work_dices[1] = g_work_Array[2];
        g_work_dices[2] = g_work_Array[3];
        g_work_dices[3] = g_work_Array[5];
        g_work_dices[5] = g_work_Array[1];
    }

    /// <summary>
    /// 現在のマス目を保持する処理
    /// </summary>
    private void Retention_Squares() {
        //マス目の数の分繰り返す
        for (int pointer = 0; pointer < g_work_dices.Length; pointer++) {
            //保持用配列にマス目の値を保持する
            g_work_Array[pointer] = g_work_dices[pointer];
        }
    }
    private void Reset_Array() {
        //マス目の数の分繰り返す
        for (int pointer = 0; pointer < g_default_dices.Length; pointer++) {
            //保持用配列にマス目の値を保持する
            g_work_dices[pointer] = g_default_dices[pointer];
        }
    }
    private void Get_Json_Dice(int[] json_dices) {
        //マス目の数の分繰り返す
        for (int pointer = 0; pointer < g_json_dices.Length; pointer++) {
            //保持用配列にマス目の値を保持する
            g_json_dices[pointer] = json_dices[pointer];
        }
    }

}
