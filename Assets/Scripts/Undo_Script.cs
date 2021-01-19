using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Undo_Script : MonoBehaviour {

    private Game_Controller g_game_con_Script;

    private int g_ver_max = 0;
    private int g_side_max = 0;
    private int g_high_max = 0;

    [SerializeField]
    /// <summary>
    /// 一手前のダイスの親オブジェクトを保持する配列
    /// </summary>
    private GameObject[] g_undo_parents;
    private int g_parent_pointer = 0;
    /// <summary>
    /// 一手前のダイスを保持する配列
    /// </summary>
    private GameObject[] g_undo_dices;
    private int g_dice_pointer = 0;
    /// <summary>
    /// 親が保持しているダイスをの個数を格納する配列
    /// </summary>
    private int[] g_undo_dice_counters;
    private int g_count_pointer = 0;
    /// <summary>
    /// 保持しているダイスが格納されている・縦・横・高さを格納する配列
    /// </summary>
    private int[] g_dice_pointers;
    private int g_point_pointer = 0;

    private void Start() {
        g_game_con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        Array_Reset();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            Dice_Get();
        }
    }

    private void Array_Reset() {
        g_undo_parents = new GameObject[0];
        g_undo_dices = new GameObject[0];
        g_undo_dice_counters = new int[0];
        g_dice_pointers = new int[0];
    }

    private void Dice_Get() {
        g_undo_parents = GameObject.FindGameObjectsWithTag("Dice_Parent");
        //(g_ver_max, g_side_max, g_high_max) = g_game_con_Script.Get_Array_Max();
        //for (int _ver = 0; _ver < g_ver_max; _ver++) {
        //    for (int _side = 0; _side < g_side_max; _side++) {
        //        for (int _high = 0; _high < g_high_max; _high++) {
        //            int _type = g_game_con_Script.Get_Obj_Type(_ver, _side, _high);
        //            if (_type >= 100) {
        //                Debug.Log("_縦_" + _ver + "_横_" + _side + "_高さ_" +_high);
        //            }
        //        }
        //    }
        //}
    }

    /// <summary>
    /// ダイスの親オブジェクトを配列に格納する処理
    /// </summary>
    /// <param name="_storage_parent">格納する親オブジェクト</param>
    private void Storage_Parent(GameObject _storage_parent) {
        Array.Resize(ref g_undo_parents, g_undo_parents.Length + 1);
        g_undo_parents[g_parent_pointer] = _storage_parent;
        g_parent_pointer++;
    }

    /// <summary>
    /// ダイスを配列に格納する処理
    /// </summary>
    /// <param name="_storage_dice">格納するダイス</param>
    private void Storage_Dices(GameObject _storage_dice) {
        Array.Resize(ref g_undo_dices, g_undo_dices.Length + 1);
        g_undo_dices[g_dice_pointer] = _storage_dice;
        g_dice_pointer++;
    }

    /// <summary>
    /// 親の下にあるダイスの個数を配列に格納する処理
    /// </summary>
    /// <param name="_storage_count">ダイスの個数</param>
    private void Storage_Dice_Counter(int _storage_count) {
        Array.Resize(ref g_undo_dice_counters, g_undo_dice_counters.Length + 1);
        g_undo_dice_counters[g_count_pointer] = _storage_count;
        g_count_pointer++;
    }

    /// <summary>
    /// ダイスの指標を配列に格納する処理
    /// </summary>
    /// <param name="_ver">縦の指標</param>
    /// <param name="_side">横の指標</param>
    /// <param name="_high">高さの指標</param>
    private void Storage_Dice_Pointer(int _ver, int _side, int _high) {
        Array.Resize(ref g_dice_pointers, g_dice_pointers.Length + 1);
        g_dice_pointers[g_point_pointer + 0] = _ver;
        g_dice_pointers[g_point_pointer + 1] = _side;
        g_dice_pointers[g_point_pointer + 2] = _high;
        g_point_pointer += 3;
    }


}
