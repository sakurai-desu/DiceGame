using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Controller : MonoBehaviour
{
    private Input_Date g_json_Script;
    private Stage_Obj_Pool g_pool_Script;

    /// <summary>
    /// 横：X軸のポジション
    /// </summary>
    private int g_s_pos = 0;
    /// <summary>
    /// 縦：Z軸のポジション
    /// </summary>
    private int g_v_pos = 0;
    /// <summary>
    /// 高さ：Y軸のポジション
    /// </summary>
    private int g_h_pos = 0;

    /// <summary>
    /// 生成・移動したオブジェクトを格納する配列
    /// </summary>
    private GameObject[,,] g_blocks_Array;
    /// <summary>
    /// ポインタに対応した座標を格納する配列
    /// </summary>
    private Vector3[,,] g_blocksPos_Array;
    /// <summary>
    /// オブジェクトの種類を格納する配列
    /// </summary>
    private int[,,] g_blocksType_Array;

    //ブロックを格納する量を決定する変数
    //H_BlockCountは横の要素数、V_BlockCountは奥行の要素数、high_BlockCountは縦の要素数
    private int g_s_BlockCount;
    private int g_v_BlockCount;
    private int g_h_BlockCount;

    //座標等で０を指定するときに使用する
    /// <summary>
    /// 初期化用変数_0
    /// </summary>
    private const int g_originpoint = 0;

    private int[] g_json_dices;

    void Start()
    {
        g_json_Script = this.GetComponent<Input_Date>();
        g_pool_Script = GameObject.Find("Stage_Pool").GetComponent<Stage_Obj_Pool>();
        //配列の要素数をjsonで決めた数へ変更する
        (g_s_BlockCount, g_v_BlockCount, g_h_BlockCount) = g_json_Script.Get_Array_Max();

        //配列の要素数を決定する
        g_blocks_Array = new GameObject[g_s_BlockCount, g_v_BlockCount, g_h_BlockCount];
        g_blocksPos_Array = new Vector3[g_s_BlockCount, g_v_BlockCount, g_h_BlockCount];
        g_blocksType_Array = new int[g_s_BlockCount, g_v_BlockCount, g_h_BlockCount];

        Pos_Array_Reset();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Array_Debug_Log();
        }
    }

    /// <summary>
    /// 3次元配列の要素数を返す処理
    /// </summary>
    /// <returns></returns>
    public (int, int, int) Get_Array_Max() {
        return (g_v_BlockCount, g_s_BlockCount, g_h_BlockCount);
    }

    /// <summary>
    /// 配列の中身を確認するためのデバッグログを流す処理
    /// </summary>
    private void Array_Debug_Log() {
        //高さの回数繰り返す
        for (int high = g_originpoint; high < g_h_BlockCount; high++) {
            //横の回数繰り返す
            for (int side = g_originpoint; side < g_s_BlockCount; side++) {
                //縦の回数分繰り返す
                for (int ver = g_originpoint; ver < g_v_BlockCount; ver++) {
                    if (g_blocksType_Array[ver, side, high] == 100) {
                        Debug.Log("縦：" + ver + "_横：" + side + "_高さ：" + high);
                        Debug.Log("オブジェクト：" + g_blocks_Array[ver, side, high]);
                        Debug.Log("タイプ：" + g_blocksType_Array[ver, side, high]);
                    }
          
                }
            }
        }
    }

    /// <summary>
    /// ポジション配列初期化
    /// </summary>
    private void Pos_Array_Reset() {
        int i = 0;
        //高さの回数繰り返す
        for (int high = g_originpoint; high < g_h_BlockCount; high++) {
            //高さ取得
            g_h_pos = high;
            //ポジション格納
            //横の回数繰り返す
            for (int side = g_originpoint; side < g_s_BlockCount; side++) {
                //横取得
                g_s_pos = side;
                //縦の回数分繰り返す
                for (int ver = g_originpoint; ver < g_v_BlockCount; ver++) {
                    //縦取得
                    g_v_pos = ver;
                    //ポジション格納用配列にポジションを格納
                    g_blocksPos_Array[ver, side, high] = new Vector3(g_s_pos, g_h_pos, g_v_pos);
                    //タイプを保持
                    int type = g_json_Script.g_inputJson.g_blocks[i].g_type;
                    g_json_dices = g_json_Script.g_inputJson.g_blocks[i].g_dices;
                    //配列にタイプを格納
                    Storage_Obj_Type(ver, side, high, type);
                    //タイプに応じたブロックを生成
                    g_pool_Script.Spawn_Block(ver,side,high,type, g_json_dices);
                    //Json用の指標を進める
                    i++;
                }
            }
        }
    }

    /// <summary>
    /// 配列にオブジェクトを格納する処理
    /// </summary>
    /// <param name="ver">縦の指標</param>
    /// <param name="side">横の指標</param>
    /// <param name="high">高さの指標</param>
    public void Storage_Obj(int ver,int side,int high,GameObject storage_Obj) {
        g_blocks_Array[ver, side, high] = storage_Obj;
    }

    /// <summary>
    /// 配列にオブジェクトの種類を格納する処理
    /// </summary>
    /// <param name="ver">縦の指標</param>
    /// <param name="side">横の指標</param>
    /// <param name="high">高さの指標</param>
    public void Storage_Obj_Type(int ver, int side, int high, int obj_Type) {
        g_blocksType_Array[ver, side, high] = obj_Type;
    }

    /// <summary>
    /// 配列の中身を空白にする処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    public void Storage_Reset(int ver, int side, int high) {
        //オブジェクトを空白にする
        g_blocks_Array[ver, side, high] = null;
        //タイプを空白にする
        g_blocksType_Array[ver, side, high] = 0;
    }

    /// <summary>
    /// 与えられた指標に格納されているポジションを返す処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <returns>ポジション</returns>
    public Vector3 Get_Pos(int ver,int side,int high) {
        return g_blocksPos_Array[ver, side, high];
    }

    /// <summary>
    /// 与えられた指標に格納されているオブジェクトを返す処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <returns>オブジェクト</returns>
    public GameObject Get_Obj(int ver, int side, int high) {
        return g_blocks_Array[ver, side, high];
    }

    /// <summary>
    /// 与えられた指標に格納されているオブジェクトの種類を返す処理
    /// </summary>
    /// <param name="ver">縦</param>
    /// <param name="side">横</param>
    /// <param name="high">高さ</param>
    /// <returns>オブジェクトの種類</returns>
    public int Get_Obj_Type(int ver, int side, int high) {
        if (high <= 0) {
            high = 0;
        }
        if (side <= 0) {
            side = 0;
        }
        if (ver <= 0) {
            ver = 0;
        }
        if (ver >= g_v_BlockCount) {
            ver = g_v_BlockCount-1;
        }
        if (side >= g_s_BlockCount) {
            side = g_s_BlockCount-1;
        }
        if (high >= g_h_BlockCount) {
            high = g_h_BlockCount-1;
        }

        return g_blocksType_Array[ver, side, high];
    }
}
