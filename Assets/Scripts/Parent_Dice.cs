using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parent_Dice : MonoBehaviour
{
    private Transform g_parent_transform;
    [SerializeField]
    private GameObject[] g_child_Array;
    private int g_child_pointer;

    private GameObject g_con_Dice;
    
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;

    private int g_dice_ver;
    private int g_dice_side;
    private int g_dice_high;

    void Start() {
        Storage_Children();
    }

    /// <summary>
    /// 親オブジェクトの中に子オブジェクトを入れる
    /// </summary>
    public void Parent_In_Child(GameObject[] storage_Array) {
        for (int pointer = g_zero_Count; pointer < storage_Array.Length; pointer++) {
            //くっついたオブジェクトを自分の子オブジェクトにする
            storage_Array[pointer].transform.parent = gameObject.transform;
        }
    }

    public GameObject[] Get_Children() {
        //子オブジェクトを配列に格納
        Storage_Children();
        return g_child_Array;
    }

    /// <summary>
    /// 自分の子オブジェクトをすべて配列に入れなおす
    /// </summary>
    private void Storage_Children() {
        //配列初期化
        g_child_Array = new GameObject[0];
        //指標初期化
        g_child_pointer = 0;
        //子オブジェクトを全て配列に格納する
        foreach (Transform g_child_transform in this.transform) {
            Array.Resize(ref g_child_Array, g_child_Array.Length + 1);
            //配列に格納
            g_child_Array[g_child_pointer] = g_child_transform.gameObject;
            //指標＋1
            g_child_pointer++;
        }
    }

    public GameObject Plus_Side(int player_ver, int player_side, int player_high) {
        int max = 0;
        for (int i = 0; i < g_child_Array.Length ; i++) {
            (g_dice_ver, g_dice_side, g_dice_high) =
                g_child_Array[i].GetComponent<Dice_Squares>().Get_Dice_Pointer();
            Debug.Log(g_dice_ver+"_"+ g_dice_side+"_"+ g_dice_high);
            if (max < player_side) {
                max = player_side;
                g_con_Dice = g_child_Array[i];
            }
        }
        return g_con_Dice;
    }
}
