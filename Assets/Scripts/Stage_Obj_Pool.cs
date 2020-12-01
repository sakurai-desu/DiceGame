using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Obj_Pool : MonoBehaviour {
    private Game_Controller g_game_Con_Script;
    private Dice_Create g_dice_create_Script;
    private Playercontroller g_player_sporn;

    //ブロックを入れるためのオブジェクト
    private GameObject g_block_Obj;

    [SerializeField]
    private GameObject g_parent_Prefab;

    [SerializeField]
    private GameObject g_null_Prefab;
    [SerializeField]
    private GameObject g_dice_Prefab;
    [SerializeField]
    private GameObject g_floor_Prefab;
    [SerializeField]
    private GameObject g_start_Prefab;
    [SerializeField]
    private GameObject g_goal_Prefab;

    /// <summary>
    /// 空白パラメータ
    /// </summary>
    private const int g_null_para = 0;
    /// <summary>
    /// スタートパラメータ
    /// </summary>
    private const int g_start_para = 10;
    /// <summary>
    /// ゴールパラメータ
    /// </summary>
    private const int g_goal_para = 20;
    /// <summary>
    /// 床パラメータ
    /// </summary>
    private const int g_ground_para = 50;
    /// <summary>
    /// ダイスパラメータ
    /// </summary>
    private const int g_dice_para = 100;

    void Start() {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_player_sporn = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_dice_create_Script = this.GetComponent<Dice_Create>();
    }


    /// <summary>
    /// オブジェクトを指定した位置に生成して、配列に格納
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <param name="type">種類</param>
    public void Spawn_Block(int ver, int side, int high, int type, int[] json_dices) {
        //タイプに応じて生成するプレハブを変更する
        switch (type) {
            //空白
            case g_null_para:
                g_block_Obj = BlockCreator(g_null_Prefab);
                break;
            //スタート
            case g_start_para:
                g_block_Obj = BlockCreator(g_start_Prefab);
                Vector3 player_pos = g_game_Con_Script.Get_Pos(ver, side, high);
                GameObject player = g_player_sporn.PlayerCreator(player_pos);
                g_player_sporn.GetComponent<Player_Direction>().Start_Direction(player);
                break;
            //ゴール
            case g_goal_para:
                g_block_Obj = BlockCreator(g_goal_Prefab);
                break;
            //床
            case g_ground_para:
                g_block_Obj = BlockCreator(g_floor_Prefab);
                break;
            //ダイス
            case g_dice_para:
                g_block_Obj = Dice_Creator(g_dice_Prefab);
                //生成した位置をダイスが保持
                g_block_Obj.GetComponent<Dice_Squares>().Storage_This_Index(ver, side, high);
                //生成段階のマス目をダイスが保持
                g_block_Obj.GetComponent<Dice_Squares>().Storage_Squares(json_dices);
                //生成時のマス目になるようにダイスを回転させる
                g_dice_create_Script.Dice_Squares_Change(g_block_Obj, json_dices);
                break;
        }
        //ポジション取得
        Vector3 g_sporn_Pos = g_game_Con_Script.Get_Pos(ver, side, high);
        //生まれたブロックのポジションを変更する
        g_block_Obj.transform.position = g_sporn_Pos;
        //配列にオブジェクトを格納する
        g_game_Con_Script.Storage_Obj(ver, side, high, g_block_Obj);
    }

    /// <summary>
    /// ブロックを生成する
    /// </summary>
    /// <returns></returns>
    private GameObject BlockCreator(GameObject diceobj) {
        GameObject g_blockObj;
        //オブジェクトを生成
        g_blockObj = Instantiate(diceobj);
        //親を決める
        g_blockObj.transform.parent = gameObject.transform;
        //ノーツオブジェクトを返す
        return g_blockObj;
    }

    /// <summary>
    /// ダイスプレハブ生成処理
    /// </summary>
    /// <param name="dice_obj">ダイスのプレハブ</param>
    /// <returns></returns>
    private GameObject Dice_Creator(GameObject dice_obj) {
        //ダイスの親オブジェクト生成
        GameObject parent_Obj = Instantiate(g_parent_Prefab);
        //ダイス生成
        GameObject g_blockObj = Instantiate(dice_obj);
        //ダイスの親を決める
        g_blockObj.transform.parent = parent_Obj.transform;
        //ダイスを返す
        return g_blockObj;
    }
}
