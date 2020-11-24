using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Input_Date : MonoBehaviour
{
    public InputJson g_inputJson;

    [Serializable]
    public class InputJson {
        //読み込むjsonの名前
        public string g_stagename;
        //ステージの縦の広さ
        public int g_ver;
        //ステージの横の広さ
        public int g_hori;
        //ステージの高さ
        public int g_high;
        //ステージ攻勢を入れる配列
        public Block[] g_blocks;
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

    [SerializeField]
    /// <summary>
    /// 呼び出すテキストの名前
    /// </summary>
    private string g_jsonname;

    private void Awake() {
        //譜面データをResourcesフォルダから取得
        string inputString = Resources.Load<TextAsset>(g_jsonname).ToString();
        //譜面データを取り込む
        g_inputJson = JsonUtility.FromJson<InputJson>(inputString);

    }

    void Start()
    {

    }

    /// <summary>
    /// ステージの縦横高さを返す処理
    /// </summary>
    /// <returns></returns>
    public (int, int, int) Get_Array_Max() {
        return (g_inputJson.g_ver,g_inputJson.g_hori,g_inputJson.g_high);
    }
}
