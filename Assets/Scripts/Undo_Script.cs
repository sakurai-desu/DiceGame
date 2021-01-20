using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Undo_Script : MonoBehaviour {

    private Game_Controller g_game_con_Script;
    private Dice_Squares g_squares_Script;
    
    [SerializeField, Header("デバッグ用")]
    /// <summary>
    /// 一手前のダイスの親オブジェクトを保持する配列
    /// </summary>
    private GameObject[] g_undo_parents;
    private int g_parent_pointer = 0;
    [SerializeField, Header("デバッグ用")]
    /// <summary>
    /// 一手前のダイスを保持する配列
    /// </summary>
    private GameObject[] g_undo_dices;
    private int g_dice_pointer = 0;
    [SerializeField, Header("デバッグ用")]
    /// <summary>
    /// 親が保持しているダイスをの個数を格納する配列
    /// </summary>
    private int[] g_undo_dice_counters;
    private int g_count_pointer = 0;
    [SerializeField, Header("デバッグ用")]
    /// <summary>
    /// 保持しているダイスが格納されている・縦・横・高さを格納する配列
    /// </summary>
    private int[] g_dice_pointers;
    private int g_point_pointer = 0;
    [SerializeField, Header("デバッグ用")]
    /// <summary>
    /// 保持しているダイスが格納されているマス目を格納する配列
    /// </summary>
    private int[] g_dice_squares;
    private int g_squares_pointer = 0;

    private GameObject[] g_work_children;

    private int g_work_ver = 0;
    private int g_work_side = 0;
    private int g_work_high = 0;

    private void Start() {
        g_game_con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        Array_Reset();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            Keep_Info();
        }
    }

    private void Array_Reset() {
        g_undo_parents = new GameObject[0];
        g_undo_dices = new GameObject[0];
        g_undo_dice_counters = new int[0];
        g_dice_pointers = new int[0];
        g_parent_pointer = 0;
        g_dice_pointer = 0;
        g_count_pointer = 0;
        g_point_pointer = 0;
    }

    private void Keep_Info() {
        Array_Reset();
        g_undo_parents = GameObject.FindGameObjectsWithTag("Dice_Parent");
        for (int i = 0; i < g_undo_parents.Length; i++) {
            g_work_children = g_undo_parents[i].GetComponent<Parent_Dice>().Get_Children();
            Storage_Dice_Counter(g_work_children.Length);
            Storage_Dices();
        }
    }

    /// <summary>
    /// ダイスを配列に格納する処理
    /// </summary>
    /// <param name="_storage_dice">格納するダイス</param>
    private void Storage_Dices() {
        //現在保持している子オブジェクトの数分繰り返す
        for (int i = 0; i < g_work_children.Length; i++) {
            //ダイスを格納する配列のサイズを増やす
            Array.Resize(ref g_undo_dices, g_undo_dices.Length + 1);
            //保持中のダイスを配列に格納
            g_undo_dices[g_dice_pointer] = g_work_children[i];
            //ダイスのスクリプトを取得
            g_squares_Script = g_undo_dices[g_dice_pointer].GetComponent<Dice_Squares>();
            //指標を進める
            g_dice_pointer++;
            //取得したスクリプトから・縦・横・高さの３つの指標を取得
            (g_work_ver, g_work_side, g_work_high) = g_squares_Script.Get_Dice_Pointer();
            //取得した指標を配列に格納
            Storage_Dice_Pointer(g_work_ver, g_work_side, g_work_high);
        }
    }

    /// <summary>
    /// 親の下にあるダイスの個数を配列に格納する処理
    /// </summary>
    /// <param name="_storage_count">ダイスの個数</param>
    private void Storage_Dice_Counter(int _storage_count) {
        //配列のサイズを増やす
        Array.Resize(ref g_undo_dice_counters, g_undo_dice_counters.Length + 1);
        //引数を配列に格納
        g_undo_dice_counters[g_count_pointer] = _storage_count;
        //指標を１つ進める
        g_count_pointer++;
    }

    /// <summary>
    /// ダイスの指標を配列に格納する処理
    /// </summary>
    /// <param name="_ver">縦の指標</param>
    /// <param name="_side">横の指標</param>
    /// <param name="_high">高さの指標</param>
    private void Storage_Dice_Pointer(int _ver, int _side, int _high) {
        //配列のサイズを・縦・横・高さの3つ分増やす
        Array.Resize(ref g_dice_pointers, g_dice_pointers.Length + 3);
        //縦格納
        g_dice_pointers[g_point_pointer + 0] = _ver;
        //横格納
        g_dice_pointers[g_point_pointer + 1] = _side;
        //高さ格納
        g_dice_pointers[g_point_pointer + 2] = _high;
        //指標を３つ進める
        g_point_pointer += 3;
    }
}
