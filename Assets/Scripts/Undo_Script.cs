using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Undo_Script : MonoBehaviour
{
    private Game_Controller g_game_con_Script;
    private Playercontroller g_player_con_Script;
    private Dice_Squares g_dice_squares_Script;

    [SerializeField]
    private int g_undo_player_ver;
    [SerializeField]
    private int g_undo_player_side;
    [SerializeField]
    private int g_undo_player_high;

    private int g_undo_rotate_direction;

    [SerializeField]
    private GameObject[] g_undo_dices;
    [SerializeField]
    private int[] g_undo_dice_pointers;
    private int g_pointer_count=0;

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
        g_game_con_Script = this.GetComponent<Game_Controller>();
        g_player_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B)) {
    //        Play_Undo();
    //    }
    //}

    private void Play_Undo() {
        GameObject player_obj = GameObject.FindGameObjectWithTag("Player");
        player_obj.transform.position = g_game_con_Script.Get_Pos(g_undo_player_ver, g_undo_player_side, g_undo_player_high);
        g_pointer_count = 0;
        for (int pointer = 0; pointer < g_undo_dices.Length; pointer++) {
            (int back_ver, int back_side, int back_high) = (g_undo_dice_pointers[g_pointer_count], g_undo_dice_pointers[g_pointer_count + 1], g_undo_dice_pointers[g_pointer_count + 2]);

            g_pointer_count = g_pointer_count + 3;
            (int ver, int side, int high) = g_undo_dices[pointer].GetComponent<Dice_Squares>().Get_Dice_Pointer();
            //元々格納されていたオブジェクトを空にする
            g_game_con_Script.Storage_Reset(ver, side, high);
            //子オブジェクトを回転に格納
            g_game_con_Script.Storage_Obj(back_ver, back_side, back_high, g_undo_dices[pointer]);
            //タイプを回転先に格納
            g_game_con_Script.Storage_Obj_Type(back_ver, back_side, back_high, 100);
            g_undo_dices[pointer].transform.position =
                g_game_con_Script.Get_Pos(back_ver, back_side, back_high);
        }
    }

    private void Reset_Keep() {
        g_undo_dice_pointers = new int[0];
        g_pointer_count = 0;
    }

    public void Keep_Dice_Children(GameObject[] dices,int para) {
        Reset_Keep();
        Keep_Player_Pointer();
        g_undo_dices = dices;
        for (int pointer = 0; pointer < g_undo_dices.Length; pointer++) {
            g_dice_squares_Script = g_undo_dices[pointer].GetComponent<Dice_Squares>();
            //縦・横・高さの順番で値を配列に格納
            Array.Resize(ref g_undo_dice_pointers, g_undo_dice_pointers.Length + 3);
            (g_undo_dice_pointers[g_pointer_count], g_undo_dice_pointers[g_pointer_count + 1], 
                g_undo_dice_pointers[g_pointer_count + 2])= g_dice_squares_Script.Get_Dice_Pointer();
            g_pointer_count = g_pointer_count + 3;
        }
    }

    /// <summary>
    /// プレイヤーのいた位置を保持する処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    public void Keep_Player_Pointer() {
        (g_undo_player_ver, g_undo_player_side, g_undo_player_high) = g_player_con_Script.Get_Player_Pointer();
    }
}
