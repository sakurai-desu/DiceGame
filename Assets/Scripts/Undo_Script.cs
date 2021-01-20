using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Undo_Script : MonoBehaviour {

    private Game_Controller g_game_con_Script;
    private Playercontroller g_player_con_Script;
    private Dice_Squares g_squares_Script;
    private Dice_Create g_dice_create_Script;

    private GameObject g_player_obj = null;
    private int g_player_ver = 0;
    private int g_player_side = 0;
    private int g_player_high = 0;
    
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

    /// <summary>
    /// 子オブジェクトを一時的に保持する配列
    /// </summary>
    private GameObject[] g_work_children;
    /// <summary>
    /// ダイスのマス目を一時的に保持する配列
    /// </summary>
    private int[] g_work_squares;
    private int g_work_pointer = 0;

    private int g_work_ver = 0;
    private int g_work_side = 0;
    private int g_work_high = 0;

    private int g_before_ver = 0;
    private int g_before_side = 0;
    private int g_before_high = 0;

    private void Start() {
        g_game_con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_player_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_dice_create_Script = GameObject.Find("Stage_Pool").GetComponent<Dice_Create>();
        Array_Reset();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            Keep_Info();
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            Undo_Play();
        }
    }

    private void Undo_Play() {
        //プレイヤーの移動先取得
        Vector3 _player_pos = g_game_con_Script.Get_Pos(g_player_ver, g_player_side, g_player_high);
        //プレイヤー移動
        g_player_obj.transform.position = _player_pos;
        //プレイヤーの保持する指標変更
        g_player_con_Script.Storage_Player_Pointer(g_player_ver, g_player_side, g_player_high);

        //ダイス配列用の指標
        int _dice_pointer = 0;
        //縦・横・高さ配列用の指標
        int _point_pointer = 0;
        //保持している親の数分繰り返す
        for (int i = 0; i < g_undo_parents.Length; i++) {
            //親オブジェクト取得
            GameObject _work_parent = g_undo_parents[i];
            //親の子オブジェクトの数取得
            int _work_count = g_undo_dice_counters[i];
            
            //子オブジェクトの数分繰り返す
            for (int j = 0; j < _work_count; j++) {
                //子オブジェクト取得
                GameObject _work_dice = g_undo_dices[j + _dice_pointer];
                //子オブジェクトの親を今保持している親に変更
                _work_dice.transform.parent = _work_parent.transform;
                //縦の指標取得
                g_work_ver = g_dice_pointers[_point_pointer];
                //横の指標取得
                g_work_side = g_dice_pointers[_point_pointer + 1];
                //高さの指標取得
                g_work_high = g_dice_pointers[_point_pointer + 2];
                //元に戻す位置を取得
                Vector3 _undo_pos = g_game_con_Script.Get_Pos(g_work_ver, g_work_side, g_work_high);
                //子オブジェクトを移動
                _work_dice.transform.position = _undo_pos;
                Debug.Log("ここでマス目に応じてダイス回転");
                //ダイスのスクリプト取得
                g_squares_Script = _work_dice.GetComponent<Dice_Squares>();
                //ダイスの移動前の指標取得
                (g_before_ver, g_before_side, g_before_high) = g_squares_Script.Get_Dice_Pointer();
                //オブジェクトの種類を取得
                int _type = g_game_con_Script.Get_Obj_Type(g_before_ver, g_before_side, g_before_high);
                //大元の配列からダイスを除去
                g_game_con_Script.Storage_Reset(g_before_ver, g_before_side, g_before_high);

                //移動後の縦・横・高さの指標に変更
                g_squares_Script.Storage_This_Index(g_work_ver, g_work_side, g_work_high);
                //大元の配列にダイス格納
                g_game_con_Script.Storage_Obj(g_work_ver, g_work_side, g_work_high, _work_dice);
                //大元の配列に種類格納
                g_game_con_Script.Storage_Obj_Type(g_work_ver, g_work_side, g_work_high, _type);

                g_work_squares = Get_Dice_Squares();
                g_dice_create_Script.Dice_Squares_Change(_work_dice,g_work_squares);
                g_squares_Script.Storage_Squares(g_work_squares);

                //縦・横・高さ配列の指標を3つ進める
                _point_pointer += 3;
            }
            //ダイス用の配列の指標を取り出した個数分進める
            _dice_pointer += _work_count;
        }
    }

    private void Array_Reset() {
        g_undo_parents = new GameObject[0];
        g_parent_pointer = 0;
        g_undo_dices = new GameObject[0];
        g_dice_pointer = 0;
        g_undo_dice_counters = new int[0];
        g_count_pointer = 0;
        g_dice_pointers = new int[0];
        g_point_pointer = 0;
        g_dice_squares = new int[0];
        g_squares_pointer = 0;
        g_work_pointer = 0;
    }

    private void Keep_Info() {
        g_player_obj = GameObject.FindWithTag("Player").gameObject;
        (g_player_ver, g_player_side, g_player_high) = g_player_con_Script.Get_Player_Pointer();
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
            g_work_squares = g_squares_Script.Get_Dice_Squares();
            Storage_Dice_Squares(g_work_squares);
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

    /// <summary>
    /// ダイスのマス目を配列に格納する処理
    /// </summary>
    /// <param name="_squares">マス目</param>
    private void Storage_Dice_Squares(int[] _squares) {
        Array.Resize(ref g_dice_squares, g_dice_squares.Length + 6);
        for (int i = 0; i < _squares.Length; i++) {
            g_dice_squares[g_squares_pointer] = _squares[i];
            g_squares_pointer++;
        }
    }

    private int[] Get_Dice_Squares() {
        int[] _keep_squares=new int[6];
        for (int i = 0; i < _keep_squares.Length; i++) {
            _keep_squares[i] = g_dice_squares[g_work_pointer];
            g_work_pointer++;
        }
        return _keep_squares;
    }
}
