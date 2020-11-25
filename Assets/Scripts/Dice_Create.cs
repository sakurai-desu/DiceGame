using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Create : MonoBehaviour {

    private int[] g_default_dices = { 1, 4, 2, 3, 6, 5 };
    //private int[] g_work_dices = new int[6];
    //private int[] g_json_dices = new int[6];
    [SerializeField]
    private int[] g_work_dices=new int[6];
    [SerializeField]
    private int[] g_work_Array=new int[6];
    [SerializeField]
    private int[] g_json_dices = { 1, 5, 4, 2, 6, 3 };

    [SerializeField]
    private int g_ver_count;
    private int g_side_count;
    private int g_high_count;

    void Start() {
        g_work_dices = g_default_dices;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Ver_Check();
        }
        if (Input.GetKeyDown(KeyCode.W)) {
            Side_Check();
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            High_Check();
        }
    }

    private void Ver_Check() {
        g_ver_count = 0;
        for (int i = 0; i < 4; i++) {
            Ver_Squeares_Change();
            g_ver_count++;
            if (g_ver_count > 3) {
                Debug.Log("一致していない");
            }
            if (g_work_dices[0] == g_json_dices[0]) {
                Debug.Log("一致した");
                break;
            }

        }
    }
    private void Side_Check() {
        g_side_count = 0;
        for (int i = 0; i < 4; i++) {
            Side_Squeares_Change();
            g_side_count++;
            if (g_side_count > 3) {
                Debug.Log("一致していない");
            }
            if (g_work_dices[1] == g_json_dices[1]) {
                Debug.Log("一致した");
                break;
            }

        }
    }
    private void High_Check() {
        g_high_count = 0;
        for (int i = 0; i < 4; i++) {
            High_Squeares_Change();
            g_high_count++;
            if (g_high_count > 3) {
                Debug.Log("一致していない");
            }
            if (g_work_dices[2] == g_json_dices[2]) {
                Debug.Log("一致した");
                break;
            }

        }
    }

    private void Squares_All_Check() {
        for (int i = 0; i < g_default_dices.Length; i++) {
            if (g_work_dices[i] != g_json_dices[i]) {
                Debug.Log("全てが一致しきっていない");
                break;
            }
            if (i >= g_default_dices.Length - 1) {
                Debug.Log("全てが一致しました");
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

}
