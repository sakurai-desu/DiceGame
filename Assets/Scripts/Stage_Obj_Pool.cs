using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage_Obj_Pool : MonoBehaviour
{
    private Game_Controller g_game_Con_Script;

    //生成するオブジェクト
    [SerializeField]
    private GameObject g_createBlock;
    //ブロックを入れるためのオブジェクト
    private GameObject g_block_Obj;
    private GameObject g_create_Obj;


    //配列のH_BlockCountの要素を指すポインター
    private int g_iPointer;

    //配列のV_BlockCountの要素を指すポインター
    private int g_jPointer;

    //配列のhigh_BlockCountの要素を指すポインター
    private int g_kPointer;

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

    private Playercontroller g_player_sporn;
    void Start()
    {
        g_game_Con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_player_sporn = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
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
    /// オブジェクトを指定した位置に生成して、配列に格納
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <param name="type">種類</param>
    public void Spawn_Block(int ver, int side, int high,int type,int[] json_dices) {
        //タイプに応じて生成するプレハブを変更する
        switch (type) {
            case 0:
                g_block_Obj = BlockCreator(g_null_Prefab);
                break;
            case 10:
                g_block_Obj = BlockCreator(g_start_Prefab);
                Vector3 player = g_game_Con_Script.Get_Pos(ver,side,high);
                g_player_sporn.PlayerCreator(player);
                break;
            case 20:
                g_block_Obj = BlockCreator(g_goal_Prefab);
                break;
            case 50:
                g_block_Obj = BlockCreator(g_floor_Prefab);
                break;
            case 100:
                g_block_Obj = Dice_Creator(g_dice_Prefab);
                g_block_Obj.GetComponent<Dice_Squares>().Storage_This_Index(ver, side, high);
                break;
        }
        //ポジション取得
        Vector3 g_sporn_Pos = g_game_Con_Script.Get_Pos(ver, side, high);
        //生まれたブロックのポジションを変更する
        g_block_Obj.transform.position = g_sporn_Pos;
        //配列にオブジェクトを格納する
        g_game_Con_Script.Storage_Obj(ver, side, high, g_block_Obj);
    }

    private GameObject Dice_Creator(GameObject dice_obj) {
        GameObject parent_Obj = Instantiate(g_parent_Prefab);
        GameObject g_blockObj;
        //オブジェクトを生成
        g_blockObj = Instantiate(dice_obj);
        //親を決める
        g_blockObj.transform.parent = parent_Obj.transform;
        return g_blockObj;
    }
}
