using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Undo_Script : MonoBehaviour
{
    private Game_Controller g_game_con_Script;
    private Playercontroller g_player_con_Script;
    private Dice_Squares g_dice_squares_Script;
    private Dice_Rotate g_dice_rotate_Script;
    private Dice_Create g_dice_create_Script;

    private const int g_zero_count = 0;

    [SerializeField]
    private int g_undo_player_ver;
    [SerializeField]
    private int g_undo_player_side;
    [SerializeField]
    private int g_undo_player_high;
    /// <summary>
    /// プレイヤーの向いている方向を保持する変数
    /// </summary>
    private int g_undo_rotate_direction;
    /// <summary>
    /// 保持したいダイスを格納する配列
    /// </summary>
    [SerializeField]
    private GameObject[] g_undo_dices;
    /// <summary>
    /// ダイスの縦・横・高さを順番に格納していく配列
    /// </summary>
    [SerializeField]
    private int[] g_undo_dice_pointers;
    /// <summary>
    /// ダイスの位置を配列から取り出す際に使用するポインタ
    /// </summary>
    private int g_pointer_count=0;

    /// <summary>
    /// 合体したか判別するフラグ
    /// </summary>
    private bool g_is_docking;
    /// <summary>
    /// 一手戻すことが可能か判別するフラグ
    /// </summary>
    private bool g_is_undo;

    [SerializeField]
    private GameObject g_not_undo_dice_parent;
    [SerializeField]
    private GameObject[] g_undo_dice_parents;
    [SerializeField]
    private int g_undo_parent_pointer;
    [SerializeField]
    private GameObject[] g_undo_dice_children;
    [SerializeField]
    private int g_undo_children_pointer;
    [SerializeField]
    /// <summary>
    /// 保持しているダイスのどこまでのダイスがどの親かを保持する配列
    /// </summary>
    private int[] g_keep_dices_pointers;

    /// <summary>
    /// 縦のプラス方向のパラメータ
    /// </summary>
    private const int g_ver_plus_Para = 31;
    /// <summary>
    /// 縦のマイナス方向のパラメータ
    /// </summary>
    private const int g_ver_minus_Para = 33;
    /// <summary>
    /// 横のプラス方向のパラメータ
    /// </summary>
    private const int g_side_plus_Para = 30;
    /// <summary>
    /// 横のマイナス方向のパラメータ
    /// </summary>
    private const int g_side_minus_Para = 32;

    void Start()
    {
        g_is_undo = true;
        g_game_con_Script = this.GetComponent<Game_Controller>();
        g_player_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_dice_create_Script = GameObject.Find("Stage_Pool").GetComponent<Dice_Create>();
        Keep_Player_Pointer();
        Reset_Keep_Value();
    }

    //void Update() {

    //    if (Input.GetKeyDown(KeyCode.B)) {
    //        Play_Undo();
    //    }
    //}

    private void Reset_Keep_Value() {
        //保持しているプレイヤーの位置を初期化
        (g_undo_player_ver, g_undo_player_side, g_undo_player_high) = (g_zero_count, g_zero_count, g_zero_count);
        //保持している移動した方向を初期化
        g_undo_rotate_direction = g_zero_count;
        //保持していたダイス配列を初期化
        g_undo_dices = new GameObject[g_zero_count];
        //ダイスの位置を格納する配列を初期化
        g_undo_dice_pointers = new int[g_zero_count];
        //ダイスの位置を配列から取り出す変数初期化
        g_pointer_count = g_zero_count;
        g_undo_children_pointer = g_zero_count;
        g_undo_dice_children = new GameObject[g_zero_count];
        g_undo_parent_pointer = g_zero_count;
        g_undo_dice_parents = new GameObject[g_zero_count];
        g_keep_dices_pointers = new int[g_zero_count];
    }

    private void Play_Undo() {
        if (g_player_con_Script.Get_MoveFlag()) {
            return;
        }
        if (!g_is_undo) {
            return;
        }
        g_is_undo = false;
        //操作中にする
        g_player_con_Script.MoveFlag_True();
        //プレイヤーオブジェクト取得
        GameObject player_obj = GameObject.FindGameObjectWithTag("Player");
        //プレイヤーを一手前のポジションに変更する
        player_obj.transform.position = g_game_con_Script.Get_Pos(g_undo_player_ver, g_undo_player_side, g_undo_player_high);
        //プレイヤーが保持するポインターを更新する
        g_player_con_Script.Storage_Player_Pointer(g_undo_player_ver, g_undo_player_side, g_undo_player_high);

        //ポインター初期化
        g_pointer_count = 0;
        //一手前に動かしたダイスの個数分繰り返す
        for (int pointer = 0; pointer < g_undo_dices.Length; pointer++) {
            //配列に保持していた一手前のポインターを取得
            (int back_ver, int back_side, int back_high) = (g_undo_dice_pointers[g_pointer_count], g_undo_dice_pointers[g_pointer_count + 1], g_undo_dice_pointers[g_pointer_count + 2]);
            //保持用配列のポインターを3つ進める
            g_pointer_count = g_pointer_count + 3;
            //戻したいダイスのスクリプトを取得
            g_dice_squares_Script = g_undo_dices[pointer].GetComponent<Dice_Squares>();
            //ダイスの現在のポインター取得
            (int now_ver, int now_side, int now_high) = g_dice_squares_Script.Get_Dice_Pointer();
            //元々格納されていたオブジェクトを空にする
            g_game_con_Script.Storage_Reset(now_ver, now_side, now_high);
            //ダイスのオブジェクトを移動先に格納
            g_game_con_Script.Storage_Obj(back_ver, back_side, back_high, g_undo_dices[pointer]);
            //ダイスのタイプを移動先に格納
            g_game_con_Script.Storage_Obj_Type(back_ver, back_side, back_high, 100);
            //ダイスを一手前のポジションに変更する
            g_undo_dices[pointer].transform.position =
                g_game_con_Script.Get_Pos(back_ver, back_side, back_high);
            //子オブジェクトが保持している指標を更新する
            g_dice_squares_Script.Storage_This_Index(back_ver, back_side, back_high);
            g_dice_squares_Script.Change_Squares(g_undo_rotate_direction);
            g_dice_create_Script.Dice_Direction_Rotate(g_undo_dices[pointer],g_undo_rotate_direction);
        }
        Parent_And_Children_Undo();
        //操作中を解除する
        g_player_con_Script.MoveFlag_False();
    }

    /// <summary>
    /// 転がしたダイスたちと元居た位置を保持する処理
    /// </summary>
    /// <param name="dices">転がしたダイスたち</param>
    /// <param name="para">向いていた方向</param>
    public void Keep_Dice_Children(GameObject[] dices,int para) {
        //保持している変数を初期化
        Reset_Keep_Value();
        //ダイスを押した方向を保持
        Keep_Dice_Direction(para);
        //一手前のプレイヤーのポインターを保持
        Keep_Player_Pointer();
        //転がしたダイスたちを保持
        g_undo_dices = dices;
        for (int pointer = 0; pointer < g_undo_dices.Length; pointer++) {
            //保持するダイスのスクリプト取得
            g_dice_squares_Script = g_undo_dices[pointer].GetComponent<Dice_Squares>();
            //ダイスのポインターを保持する配列を縦・横・高さの3つ分増やす
            Array.Resize(ref g_undo_dice_pointers, g_undo_dice_pointers.Length + 3);
            //縦・横・高さの順番で値を配列に格納
            (g_undo_dice_pointers[g_pointer_count], g_undo_dice_pointers[g_pointer_count + 1], 
                g_undo_dice_pointers[g_pointer_count + 2])= g_dice_squares_Script.Get_Dice_Pointer();
            //保持用配列のポインターを3つ進める
            g_pointer_count = g_pointer_count + 3;
        }
    }
    /// <summary>
    /// ダイスの親と子を保持する処理
    /// </summary>
    /// <param name="children_dices">くっつけられた側のダイスたち</param>
    /// <param name="parent">くっつけた側の親オブジェ</param>
    /// <param name="docking_flag">合体したか判別するフラグ</param>
    public void Keep_Dice_Parent_And_Children(GameObject[] children_dices,GameObject parent,bool docking_flag) {
        //合体したか判別するフラグを保持
        g_is_docking = docking_flag;
        //動かした側の親オブジェクト保持
        g_not_undo_dice_parent = parent;
        //くっつけられたダイスたちを保持
        //g_undo_dice_children = children_dices;
        for (int pointer = g_zero_count;pointer<children_dices.Length;pointer++) {
            Array.Resize(ref g_undo_dice_children, g_undo_dice_children.Length + 1);
            g_undo_dice_children[g_undo_children_pointer] = children_dices[pointer];
            g_undo_children_pointer++;
        }
        Array.Resize(ref g_keep_dices_pointers, g_keep_dices_pointers.Length + 1);
        g_keep_dices_pointers[g_undo_parent_pointer] = g_undo_children_pointer-1;
        Array.Resize(ref g_undo_dice_parents, g_undo_dice_parents.Length + 1);
        //くっつけられたダイスの親オブジェクト取得
        g_undo_dice_parents[g_undo_parent_pointer] = g_undo_dice_children[g_undo_children_pointer-1].transform.parent.gameObject;
        g_undo_parent_pointer++;
    }

    private void Parent_And_Children_Undo() {
        //保持した際にダイスが合体していた時
        if (g_is_docking) {
            int parents_pointer = 0;
            for (int pointer = 0; pointer < g_undo_dice_children.Length; pointer++) {
                GameObject dice = g_undo_dice_children[pointer];
                //余った親にくっつけられた側のオブジェクトたちを入れる
                //g_undo_dice_parents[pointer].GetComponent<Parent_Dice>().Parent_In_Child(g_undo_dice_children);
                g_undo_dice_parents[parents_pointer].GetComponent<Parent_Dice>().Parent_In_OneDice(dice);
                if (g_keep_dices_pointers[parents_pointer]<= pointer) {
                    parents_pointer++;
                }
            }
            //くっつけた側の親が子オブジェを取得し直す
            g_not_undo_dice_parent.GetComponent<Parent_Dice>().Storage_Children();
        }
    }

    /// <summary>
    /// プレイヤーのいた位置を保持する処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    private void Keep_Player_Pointer() {
        (g_undo_player_ver, g_undo_player_side, g_undo_player_high) = g_player_con_Script.Get_Player_Pointer();
    }

    private void Keep_Dice_Direction(int para) {
        switch (para) {
            case g_ver_plus_Para:
                g_undo_rotate_direction = g_ver_minus_Para;
                break;
            case g_ver_minus_Para:
                g_undo_rotate_direction = g_ver_plus_Para;
                break;
            case g_side_plus_Para:
                g_undo_rotate_direction = g_side_minus_Para;
                break;
            case g_side_minus_Para:
                g_undo_rotate_direction = g_side_plus_Para;
                break;
        }
    }

    public void Undo_Flag_On() {
        g_is_undo = true;
    }
}
