using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice_Fall : MonoBehaviour {
    private Playercontroller g_player_con_Script;
    private Game_Controller g_game_Con_Script;
    private Se_Source g_se_source_Script;

    private Dice_Squares g_child_Script;
    /// <summary>
    /// 落下検索するオブジェクトを格納する配列
    /// </summary>
    private GameObject[] g_work_dices;
    
    /// <summary>
    /// ダイスの現在位置：縦
    /// </summary>
    private int g_dice_ver;
    /// <summary>
    /// ダイスの現在位置：横
    /// </summary>
    private int g_dice_side;
    /// <summary>
    /// ダイスの現在位置：高さ
    /// </summary>
    private int g_dice_high;
    /// <summary>
    /// 検索位置の移動回数をカウントする変数
    /// </summary>
    private int g_fall_counter;
    /// <summary>
    /// 一番回数が少ない値を格納する変数
    /// </summary>
    private int g_min_counter;
    /// <summary>
    /// 親オブジェクトが同じか判別するフラグ/True：同じ/False：同じではない
    /// </summary>
    private bool g_same_parent_flag = false;

    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_Count = 0;
    /// <summary>
    /// 最小値の初期化用変数
    /// </summary>
    private const int g_min_Start = 100;

    private GameObject g_parent_Obj;
    private GameObject g_center_dice;

    /// <summary>
    /// 移動先の座標
    /// </summary>
    private Vector3 g_end_Pos;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_count = 0;
    /// <summary>
    /// 移動速度の最大値
    /// </summary>
    private const float g_max_speed = 25;
    /// <summary>
    /// 移動速度
    /// </summary>
    private float g_move_speed = 8;
    /// <summary>
    /// 移動速度を加算するための変数
    /// </summary>
    private float g_plus_speed = 0;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_player_con_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_se_source_Script = GameObject.Find("SEList").GetComponent<Se_Source>();
    }

    private void Get_Parent() {
        g_parent_Obj = g_center_dice.transform.parent.gameObject;
    }

    /// <summary>
    /// ダイスを落下させる処理
    /// </summary>
    /// <param name="children_dices"></param>
    public void All_Dice_Fall(GameObject[] children_dices, GameObject center_dice) {
        //移動の中心にするダイス保持
        g_center_dice = center_dice;
        //落下させるダイスを保持
        g_work_dices = children_dices;
        //落下回数を調べる
        Fall_Check();
        //ダイスを落下させる
        Fall();
    }

    /// <summary>
    /// ダイスをどれだけ落下させるか調べる処理
    /// </summary>
    private void Fall_Check() {
        //最小値初期化
        g_min_counter = g_min_Start;

        //ダイスの個数分だけ処理を繰り返す
        for (int dice_count = g_zero_Count; dice_count < g_work_dices.Length; dice_count++) {
            g_fall_counter = g_zero_Count;
            //ダイスのスクリプト取得
            g_child_Script = g_work_dices[dice_count].GetComponent<Dice_Squares>();
            //ダイスの現在位置取得
            (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
            
            //落下先が埋まるか、配列の範囲外になるまで繰り返す
            for (int pointer = g_dice_high - 1; pointer >= g_zero_Count; pointer--) {
                //同じ親フラグ初期化
                g_same_parent_flag = false;
                //検索先に埋まっているオブジェクトのタイプ取得
                int type = g_game_Con_Script.Get_Obj_Type(g_dice_ver, g_dice_side, pointer);
                //検索先がダイスの時、ダイスが検索用ダイスと同じ階層にあるか調べる
                if (type >= 100) {
                    //検索用ダイスの親オブジェクトを取得
                    GameObject dice_parent = g_work_dices[dice_count].transform.parent.gameObject;
                    //検索先のダイス取得
                    GameObject next_dice = g_game_Con_Script.Get_Obj(g_dice_ver, g_dice_side, pointer);
                    //検索先のダイスの親オブジェクトを取得
                    GameObject next_parent = next_dice.transform.parent.gameObject;
                    //ダイスが検索用ダイスと同じ階層にあるか調べる
                    if (dice_parent == next_parent) {
                        //同じならフラグON
                        g_same_parent_flag = true;
                    }
                }
                //移動先オブジェクトが空白ではないなら（床があるなら）
                if (type != 0&&!g_same_parent_flag) {
                    //処理終了
                    break;
                }
                //検索先の移動回数＋1
                g_fall_counter++;
            }

            //移動回数が最小値より小さいなら
            if (g_fall_counter < g_min_counter) {
                //最小値上書き
                g_min_counter = g_fall_counter;
            }
        }
    }
    /// <summary>
    /// ダイスを落とす処理
    /// </summary>
    private void Fall() {
        //落下する必要がないとき処理終了
        if (g_min_counter==g_zero_Count) {
            //処理終了
            return;
        }
        StartCoroutine(Move());
        ////ダイスを配列から削除
        //Dice_Storage_Reset();
        ////ダイスを配列の移動先に格納
        //Dice_Storage();
    }

    /// <summary>
    /// 現在格納されているダイスを配列から削除する
    /// </summary>
    private void Dice_Storage_Reset() {
        //ダイスの個数分だけ処理を繰り返す
        for (int dice_count = g_zero_Count; dice_count < g_work_dices.Length; dice_count++) {
            //ダイスのスクリプト取得
            g_child_Script = g_work_dices[dice_count].GetComponent<Dice_Squares>();
            //ダイスの現在位置取得
            (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
            //元の位置を空にする
            g_game_Con_Script.Storage_Reset(g_dice_ver, g_dice_side, g_dice_high);
        }
    }
    /// <summary>
    /// 操作中のダイスを移動先に格納する
    /// </summary>
    private void Dice_Storage() {
        //ダイスの個数分だけ処理を繰り返す
        for (int dice_count = g_zero_Count; dice_count < g_work_dices.Length; dice_count++) {
            //ダイスのスクリプト取得
            g_child_Script = g_work_dices[dice_count].GetComponent<Dice_Squares>();
            //ダイスの現在位置取得
            (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
            //現在の位置―移動回数をして移動先の高さを計算
            int fall_high = g_dice_high - g_min_counter;
            //ダイス格納
            g_game_Con_Script.Storage_Obj(g_dice_ver, g_dice_side, fall_high, g_work_dices[dice_count]);
            //ダイスのタイプ格納
            g_game_Con_Script.Storage_Obj_Type(g_dice_ver, g_dice_side, fall_high, 100);
            //格納先のポジション取得
            Vector3 dice_pos = g_game_Con_Script.Get_Pos(g_dice_ver, g_dice_side, fall_high);
            //ダイス移動
            g_work_dices[dice_count].transform.position = dice_pos;
            //子オブジェクトが保持している指標を更新する
            g_child_Script.Storage_This_Index(g_dice_ver, g_dice_side, fall_high);
            //全方位を調べて、くっつくダイスがあるか調べる
            g_child_Script.All_Check();
        }
    }
    /// <summary>
    /// 現在保持しているダイスを削除する処理
    /// </summary>
    private void Delete_Dice() {
        //ダイスの個数分だけ処理を繰り返す
        for (int dice_count = g_zero_Count; dice_count < g_work_dices.Length; dice_count++) {
            //ダイスのスクリプト取得
            g_child_Script = g_work_dices[dice_count].GetComponent<Dice_Squares>();
            //ダイスの現在位置取得
            (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
            //元の位置を空にする
            g_game_Con_Script.Storage_Reset(g_dice_ver, g_dice_side, g_dice_high);
            //ダイス削除
            Destroy(g_work_dices[dice_count]);
        }
    }
    /// <summary>
    /// ダイスをゆっくり落下させる処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator Move() {
        //移動中状態にする
        g_player_con_Script.MoveFlag_True();
        //速度初期化
        g_move_speed = g_zero_count;
        //速度加算変数初期化
        g_plus_speed = g_zero_count;

        //中心のダイスのスクリプト取得
        g_child_Script = g_center_dice.GetComponent<Dice_Squares>();
        //中心のダイスの現在位置取得
        (g_dice_ver, g_dice_side, g_dice_high) = g_child_Script.Get_Dice_Pointer();
        //現在の位置―移動回数をして移動先の高さを計算
        int fall_high = g_dice_high - g_min_counter;
        //中心のダイスの現在のポジション取得
        Vector3 center_Pos = g_game_Con_Script.Get_Pos(g_dice_ver, g_dice_side, g_dice_high);
        //中心のダイスの移動後のポジション取得
        Vector3 end_Pos = g_game_Con_Script.Get_Pos(g_dice_ver, g_dice_side, fall_high);
        //動かしたい親を取得
        Get_Parent();
        //動かしたい親オブジェクトの移動先を計算して格納
        //移動前から移動先の移動量を計算し、親の移動先を計算する
        Vector3 parent_end_Pos = g_parent_Obj.transform.position -(center_Pos - end_Pos);

        //親オブジェクトの位置が移動先につくまで繰り返す
        while (g_center_dice.transform.position != end_Pos) {
            //移動速度上げる
            if (g_move_speed < g_max_speed) {
                g_move_speed = g_move_speed + g_plus_speed;
                g_plus_speed = g_plus_speed + 2.5f;
            }

            //親オブジェクトを移動先に向かって移動
            g_parent_Obj.transform.position = Vector3.MoveTowards(g_parent_Obj.transform.position,
              parent_end_Pos, g_move_speed * Time.deltaTime);
            yield return null;
        }
        //移動中状態を解除する
        g_player_con_Script.MoveFlag_False();
        //移動先への移動終了
        g_center_dice.transform.position = end_Pos;
        //移動SE再生
        g_se_source_Script.Se_Play(0);
        //ダイスを配列から削除
        Dice_Storage_Reset();
        //ダイスを配列の移動先に格納
        Dice_Storage();
        //処理終了
        yield break;
    }
}
