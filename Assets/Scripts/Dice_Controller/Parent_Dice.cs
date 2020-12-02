using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Parent_Dice : MonoBehaviour {
    private Transform g_parent_transform;
    [SerializeField]
    private GameObject[] g_child_Array;
    private int g_child_pointer;

    private GameObject g_con_Dice;

    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_min_Count = 100;

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
        //子オブジェクトを配列に格納
        Storage_Children();
    }
    /// <summary>
    /// 現在の子オブジェクトを返す処理
    /// </summary>
    /// <returns></returns>
    public GameObject[] Get_Children() {
        //子オブジェクトを配列に格納
        Storage_Children();
        //子オブジェクトを返す
        return g_child_Array;
    }
    /// <summary>
    /// 現在の子オブジェクトの個数を返す処理
    /// </summary>
    /// <returns></returns>
    public int Get_Children_Count() {
        //子オブジェクトの個数を返す
        return g_child_Array.Length;
    }

    /// <summary>
    /// 自分の子オブジェクトをすべて配列に入れなおす
    /// </summary>
    private void Storage_Children() {
        //配列初期化
        g_child_Array = new GameObject[g_zero_Count];
        //指標初期化
        g_child_pointer = g_zero_Count;
        //子オブジェクトを全て配列に格納する
        foreach (Transform g_child_transform in this.transform) {
            Array.Resize(ref g_child_Array, g_child_Array.Length + 1);
            //配列に格納
            g_child_Array[g_child_pointer] = g_child_transform.gameObject;
            //指標＋1
            g_child_pointer++;
        }
    }
    /// <summary>
    /// 回転の軸にするオブジェクトを返す処理（縦プラス方向）
    /// </summary>
    /// <param name="player_ver">プレイヤー：縦</param>
    /// <param name="player_side">プレイヤー：横</param>
    /// <param name="player_high">プレイヤー：高さ</param>
    /// <returns></returns>
    public GameObject Get_Center_Ver_Plus(int player_ver, int player_side, int player_high) {
        //子オブジェクトを配列に格納
        Storage_Children();
        //最大値初期化
        int max = g_zero_Count;
        //子オブジェクトの個数分繰り返す
        for (int child_pointer = g_zero_Count; child_pointer < g_child_Array.Length; child_pointer++) {
            //ダイスの現在の指標取得
            (g_dice_ver, g_dice_side, g_dice_high) =
                g_child_Array[child_pointer].GetComponent<Dice_Squares>().Get_Dice_Pointer();
            //ダイスの縦の指標が保持している最大値以上＆
            //ダイスの横の指標がプレイヤーの横の指標と同じ＆
            //ダイスの縦の指標がプレイヤーの縦の指標と同じ
            if (max <= g_dice_ver && g_dice_side == player_side && g_dice_high == player_high) {
                //最大値を更新
                max = g_dice_ver;
                //軸のダイスを更新
                g_con_Dice = g_child_Array[child_pointer];
            }
        }
        //軸にするダイスを返す
        return g_con_Dice;
    }
    /// <summary>
    /// 回転の軸にするオブジェクトを返す処理（縦マイナス方向）
    /// </summary>
    /// <param name="player_ver">プレイヤー：縦</param>
    /// <param name="player_side">プレイヤー：横</param>
    /// <param name="player_high">プレイヤー：高さ</param>
    /// <returns></returns>
    public GameObject Get_Center_Ver_Minus(int player_ver, int player_side, int player_high) {
        //子オブジェクトを配列に格納
        Storage_Children();
        //最小値初期化
        int min = g_min_Count;
        //子オブジェクトの個数分繰り返す
        for (int child_pointer = g_zero_Count; child_pointer < g_child_Array.Length; child_pointer++) {
            //ダイスの現在の指標取得
            (g_dice_ver, g_dice_side, g_dice_high) =
                g_child_Array[child_pointer].GetComponent<Dice_Squares>().Get_Dice_Pointer();
            //ダイスの縦の指標が保持している最小値以下＆
            //ダイスの横の指標がプレイヤーの横の指標と同じ＆
            //ダイスの縦の指標がプレイヤーの縦の指標と同じ
            if (min >= g_dice_ver && g_dice_side == player_side && g_dice_high == player_high) {
            //最小値を更新
            min = g_dice_ver;
                //軸のダイスを更新
                g_con_Dice = g_child_Array[child_pointer];
            }
        }
        //軸にするダイスを返す
        return g_con_Dice;
    }
    /// <summary>
    /// 回転の軸にするオブジェクトを返す処理（横プラス方向）
    /// </summary>
    /// <param name="player_ver">プレイヤー：縦</param>
    /// <param name="player_side">プレイヤー：横</param>
    /// <param name="player_high">プレイヤー：高さ</param>
    /// <returns></returns>
    public GameObject Get_Center_Side_Plus(int player_ver, int player_side, int player_high) {
        //子オブジェクトを配列に格納
        Storage_Children();
        //最大値初期化
        int max = g_zero_Count;
        //子オブジェクトの個数分繰り返す
        for (int child_pointer = g_zero_Count; child_pointer < g_child_Array.Length; child_pointer++) {
            //ダイスの現在の指標取得
            (g_dice_ver, g_dice_side, g_dice_high) =
                g_child_Array[child_pointer].GetComponent<Dice_Squares>().Get_Dice_Pointer();
            //ダイスの横の指標が保持している最大値以上＆
            //ダイスの縦の指標がプレイヤーの縦の指標と同じ＆
            //ダイスの縦の指標がプレイヤーの縦の指標と同じ
            if (max <= g_dice_side && g_dice_ver == player_ver && g_dice_high == player_high) {
                //最大値を更新
                max = g_dice_side;
                //軸のダイスを更新
                g_con_Dice = g_child_Array[child_pointer];
            }
        }
        //軸にするダイスを返す
        return g_con_Dice;
    }
    /// <summary>
    /// 回転の軸にするオブジェクトを返す処理（横マイナス方向）
    /// </summary>
    /// <param name="player_ver">プレイヤー：縦</param>
    /// <param name="player_side">プレイヤー：横</param>
    /// <param name="player_high">プレイヤー：高さ</param>
    /// <returns></returns>
    public GameObject Get_Center_Side_Minus(int player_ver, int player_side, int player_high) {
        //子オブジェクトを配列に格納
        Storage_Children();
        //最小値初期化
        int min = 100;
        //子オブジェクトの個数分繰り返す
        for (int child_pointer = g_zero_Count; child_pointer < g_child_Array.Length; child_pointer++) {
            //ダイスの現在の指標取得
            (g_dice_ver, g_dice_side, g_dice_high) =
                g_child_Array[child_pointer].GetComponent<Dice_Squares>().Get_Dice_Pointer();
            //ダイスの横の指標が保持している最小値以下＆
            //ダイスの縦の指標がプレイヤーの縦の指標と同じ＆
            //ダイスの縦の指標がプレイヤーの縦の指標と同じ
            if (min >= g_dice_side && g_dice_ver == player_ver && g_dice_high == player_high) {
                //最小値を更新
                min = g_dice_side;
                //軸のダイスを更新
                g_con_Dice = g_child_Array[child_pointer];
            }
        }
        //軸にするダイスを返す
        return g_con_Dice;
    }
}
