using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Testpool : MonoBehaviour {

    public InputJson g_inputJson;
    [SerializeField]
    private GameObject g_diceObj1;


    [Serializable]
    public class InputJson {
        //読み込むjsonの名前
        public string g_name;
        //ステージの縦の広さ
        public int g_ver;
        //ステージの横の広さ
        public float g_hori;
        //ステージの高さ
        public int g_high;
        //ステージ攻勢を入れる配列
        public Block[] g_block;
    }

    [Serializable]
    public class Block {
        //ブロックのxのポジション
        public int g_x;
        //ブロックのyポジション
        public int g_y;
        //ブロックのzポジション
        public int g_z;
        //ブロックの種類
        public int g_type;

        //さいころのマス目
        public int[] g_dices;
    }
    //生成するオブジェクト
    [SerializeField]
    private GameObject g_createBlock;
    ////生成するオブジェクト
    //[SerializeField]
    //private GameObject createNote2;

    //配列のH_BlockCountの要素を指すポインター
    private int g_iPointer;

    //配列のV_BlockCountの要素を指すポインター
    private int g_jPointer;

    //配列のhigh_BlockCountの要素を指すポインター
    private int g_kPointer;

    //ブロックを格納する量を決定する変数
    //H_BlockCountは横の要素数、V_BlockCountは奥行の要素数、high_BlockCountは縦の要素数
    [SerializeField]
    private int g_s_BlockCount;
    [SerializeField]
    int g_v_BlockCount;
    [SerializeField]
    int g_h_BlockCount;

    //生成したブロックを保存する配列
    [SerializeField]
    private GameObject[,,] g_blocks_Array;

    //ブロックを検索する際に利用する配列
    int[,,] g_blocknum;

    Vector3[,,] g_blocksPos_Array;


    //blocを生成するポジション（X）
    private float g_spos = 0f;
    //blocを生成するポジション（Y）
    private float g_vpos = 0f;
    //blocを生成するポジション（Z）
    private float g_hpos = 0f;

    //テスト用の数値
    [SerializeField]
    private int g_t_hblock;
    [SerializeField]
    private int g_t_vblock;
    [SerializeField]
    private int g_thigh;

    //座標等で０を指定するときに使用する
    private const int g_originpoint = 0;
    //ブロックを入れるためのオブジェクト
    private GameObject g_block_Obj;
    //生成したオブジェクトのスクリプト
    private Changecolor g_stageblock;

    /// <summary>
    /// 呼び出すテキストの名前
    /// </summary>
    private string g_jsonname;
    void Start() {
        g_jsonname = "Test_Stage4";
        //譜面データをResourcesフォルダから取得
        string inputString = Resources.Load<TextAsset>(g_jsonname).ToString();
        //譜面データを取り込む
        g_inputJson = JsonUtility.FromJson<InputJson>(inputString);
        //配列の要素数をjsonで決めた数へ変更する
        g_s_BlockCount = (int)g_inputJson.g_hori;
        g_v_BlockCount = (int)g_inputJson.g_ver;
        g_h_BlockCount = (int)g_inputJson.g_high;
        //配列の要素数を決定する
        g_blocks_Array = new GameObject[g_s_BlockCount, g_v_BlockCount, g_h_BlockCount];
        //生成するブロックを探す
        g_stageblock = GameObject.Find("whiteblock").GetComponent<Changecolor>();
        g_blocksPos_Array = new Vector3[g_s_BlockCount, g_v_BlockCount, g_h_BlockCount];
        //////blockの回数だけ繰り返す
        //for (int i = 0; i < g_inputJson.g_block.Length; i++) {
        //    //生成するポジションをfor分が回るたびにずらす
        //    g_xpos = i;
        //    if (g_inputJson.g_block[i].g_type == 0) {
        //        g_stageblock.g_changecolorflag = true;
        //        SpornBlock_e();
        //    }
        //}
        //for (int i=0; i < g_v_BlockCount; i++) {
        //    g_xpos = i;
        //    g_blocksPos_Array[(int)g_xpos, g_originpoint, g_originpoint] = new Vector3(g_xpos, g_originpoint, g_originpoint);

        //    for (int j = 1; j < g_v_BlockCount; j++) {
        //        g_zpos = j;
        //        g_blocksPos_Array[(int)g_xpos, (int)g_zpos, g_originpoint] = new Vector3(g_xpos, g_originpoint, g_zpos);
        //        for (int k = 1; k < g_high_BlockCount; k++) {
        //            g_ypos = k;
        //            g_blocksPos_Array[(int)g_xpos, (int)g_zpos, (int)g_ypos] = new Vector3(g_xpos, g_zpos, g_ypos);
        //            Debug.Log(g_blocksPos_Array[(int)g_xpos, (int)g_zpos, (int)g_ypos]);
        //        }
        //    }
        //}

        ////blockの回数だけ繰り返す
        for (int k = 0; k < g_h_BlockCount; k++) {
            g_hpos = k;
            //g_blocksPos_Array[(int)g_spos, (int)g_vpos, (int)g_hpos] = new Vector3(g_spos, g_vpos, g_hpos);


            //Debug.Log(g_blocksPos_Array[(int)g_spos, (int)g_vpos, (int)g_hpos]);
            for (int i = 0; i < g_s_BlockCount; i++) {
                //生成するポジションをfor分が回るたびにずらす
                g_spos = i;
                SpornBlock_i();

                g_blocksPos_Array[(int)g_spos, g_originpoint, g_originpoint] = new Vector3(g_spos, g_hpos, g_originpoint);

                for (int j = 1; j < g_v_BlockCount; j++) {
                    g_vpos = j;
                    SpornBlock_j();

                    g_blocksPos_Array[(int)g_spos, (int)g_vpos, g_originpoint] = new Vector3(g_spos, g_hpos, g_vpos);

                }
            }
        }
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Instantiate(g_diceObj1);
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log(g_inputJson.g_block[0].g_type);
        }

    }
    /// <summary>
    /// ブロックを生成する
    /// </summary>
    /// <returns></returns>
    private GameObject BlockCreator() {
        GameObject g_blockObj;
        //オブジェクトを生成
        g_blockObj = Instantiate(g_createBlock);
        //親を決める
        g_blockObj.transform.parent = gameObject.transform;
        //ノーツオブジェクトを返す
        return g_blockObj;
    }

    ///// <summary>
    ///// 配列要素H_BlockCountを格納する
    ///// </summary>
    //void SpornBlock_e() {
    //    //オブジェクトを生成して
    //    g_block_Obj = BlockCreator();
    //    //生まれたブロックのポジションを変更する
    //    g_block_Obj.transform.position = new Vector3(g_inputJson.g_block[(int)g_xpos].g_x, g_inputJson.g_block[(int)g_xpos].g_y, g_inputJson.g_block[(int)g_xpos].g_z);
    //    //ポインターをiの数値と同じにする
    //    g_iPointer = g_inputJson.g_block[(int)g_xpos].g_x;
    //    g_jPointer = g_inputJson.g_block[(int)g_xpos].g_y;
    //    g_kPointer = g_inputJson.g_block[(int)g_xpos].g_z;
    //    //配列にブロックを格納する（[iPointer,0,0]でi列の先頭を検索することができる）
    //    g_blocks_Array[g_iPointer, 0, 0] = g_block_Obj;
    //}

    /// <summary>
    /// 配列要素H_BlockCountを格納する
    /// </summary>
    void SpornBlock_i() {
        //オブジェクトを生成して
        g_block_Obj = BlockCreator();
        //生まれたブロックのポジションを変更する
        g_block_Obj.transform.position = new Vector3(g_spos, g_hpos, g_originpoint);
        //ポインターをiの数値と同じにする
        g_iPointer = (int)g_spos;
        //配列にブロックを格納する（[iPointer,0,0]でi列の先頭を検索することができる）
        g_blocks_Array[g_iPointer, 0, 0] = g_block_Obj;
    }

    /// <summary>
    /// 配列要素V_BlockCountを格納する
    /// </summary>
    void SpornBlock_j() {
        g_block_Obj = BlockCreator();
        g_block_Obj.transform.position = new Vector3(g_spos, g_hpos, g_vpos);

        //ポインターをiの数値と同じにする
        g_iPointer = (int)g_spos;

        //ポインターをiの数値と同じにする
        g_jPointer = (int)g_vpos;
        g_blocks_Array[g_iPointer, g_jPointer, g_originpoint] = g_block_Obj;
    }

}


