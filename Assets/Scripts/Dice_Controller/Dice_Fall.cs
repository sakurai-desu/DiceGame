using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Fall : MonoBehaviour {

    private Game_Controller g_game_Con_Script;
    private Dice_Squares g_child_Script;

    private GameObject[] g_work_dices;

    private int g_dice_ver;
    private int g_dice_side;
    private int g_dice_high;

    private int g_fall_counter;
    private int g_min_counter;

    private bool g_delete_flag = true;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
    }

    /// <summary>
    /// ダイスを落下させる処理
    /// </summary>
    /// <param name="children_dices"></param>
    public void All_Dice_Fall(GameObject[] children_dices) {
        g_work_dices = children_dices;
        Fall_Check();
        Fall();
    }

    /// <summary>
    /// オブジェクトを一段下に落とす処理
    /// </summary>
    private void Fall_Check() {
        g_delete_flag = true;
        g_min_counter = 100;
        for (int i = 0; i < g_work_dices.Length; i++) {
            g_fall_counter = 0;
            //ダイスのスクリプト取得
            g_child_Script = g_work_dices[i].GetComponent<Dice_Squares>();
            //ダイスの現在位置取得
            (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
            for (int pointer = g_dice_high - 1; pointer >= 0; pointer--) {
                int type = g_game_Con_Script.Get_Obj_Type(g_dice_ver, g_dice_side, pointer);
                if (type>=100) {
                    GameObject dice_parent = g_work_dices[i].transform.parent.gameObject;
                    GameObject next_dice= g_game_Con_Script.Get_Obj(g_dice_ver, g_dice_side, pointer);
                    GameObject next_parent = next_dice.transform.parent.gameObject;
                    if (dice_parent != next_parent) {
                        g_delete_flag = false;
                        break;
                    }
                }
                else if (type != 0) {
                    g_delete_flag = false;
                    break;
                }
                g_fall_counter++;
                //if (type == 50||type==100) {
                //    break;
                //}
            }
            if (g_fall_counter < g_min_counter) {
                g_min_counter = g_fall_counter;
            }
        }
    }

    /// <summary>
    /// ダイスを落とす処理
    /// </summary>
    private void Fall() {
        //床のないところに落ちたダイスを削除する
        if (g_delete_flag) {
            for (int i = 0; i < g_work_dices.Length; i++) {
                //ダイスのスクリプト取得
                g_child_Script = g_work_dices[i].GetComponent<Dice_Squares>();
                //ダイスの現在位置取得
                (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
                //元の位置を空にする
                g_game_Con_Script.Storage_Reset(g_dice_ver, g_dice_side, g_dice_high);
                Destroy(g_work_dices[i]);
            }
            //処理終了
            return;
        }

        for (int i = 0; i < g_work_dices.Length; i++) {
            //ダイスのスクリプト取得
            g_child_Script = g_work_dices[i].GetComponent<Dice_Squares>();
            //ダイスの現在位置取得
            (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
            int fall_high = g_dice_high - g_min_counter;
            //元の位置を空にする
            g_game_Con_Script.Storage_Reset(g_dice_ver, g_dice_side, g_dice_high);
            //ダイス格納
            g_game_Con_Script.Storage_Obj(g_dice_ver, g_dice_side, fall_high, g_work_dices[i]);
            //ダイスのタイプ格納
            g_game_Con_Script.Storage_Obj_Type(g_dice_ver, g_dice_side, fall_high, 100);
            //格納先のポジション取得
            Vector3 dice_pos = g_game_Con_Script.Get_Pos(g_dice_ver, g_dice_side, fall_high);
            //ダイス移動
            g_work_dices[i].transform.position = dice_pos;
            //子オブジェクトが保持している指標を更新する
            g_child_Script.Storage_This_Index(g_dice_ver, g_dice_side, fall_high);
            g_child_Script.All_Check();
        }
    }
}
