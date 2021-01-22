using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ScoreJsonScript : MonoBehaviour
{
    /// <summary>
    /// ステージごとのスコアを保存するクラスの親
    /// </summary>
    [Serializable]
    public class Score {
        /// <summary>
        ///　ステージ数
        /// </summary>
        public int g_stageNum;
        /// <summary>
        /// ステージごとのスコアを入れる配列
        /// </summary>
        public StageScore[] g_stageInfo;
    }
    /// <summary>
    /// ステージごとのスコア
    /// </summary>
    [Serializable]
    public class StageScore {
        /// <summary>
        /// 評価
        /// </summary>
        public int g_evaluation;
        /// <summary>
        /// 手数
        /// </summary>
        public int g_trouble;
    }

    /// <summary>
    /// スコアクラスを使用するための変数
    /// </summary>
    private Score g_stageScore=null;
    /// <summary>
    /// ストリーミングアセット内の.jsonフォルダの数を数えるスクリプト
    /// </summary>
    private JsonArray g_jsonArrayScript;

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
        g_jsonArrayScript = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
    }
    void Start() {
        //スコアを保存するためのjsonを取得する
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.streamingAssetsPath + "/Score/Score.json");
        datastr = reader.ReadToEnd();
        reader.Close();
        g_stageScore = JsonUtility.FromJson<Score>(datastr);

        //ステージの数を取得して入れる
        g_stageScore.g_stageNum = g_jsonArrayScript.g_stage_array_num;
        Debug.Log(g_stageScore.g_stageNum+"ステージ数");
        //ステージごとの評価を入れる配列を増やす
        g_stageScore.g_stageInfo= new StageScore[g_stageScore.g_stageNum];
        Debug.Log(g_stageScore.g_stageInfo.Length);
    }

    /// <summary>
    /// jsonの中身を変更する
    /// </summary>
    /// <param name="stageNum">ステージ番号</param>
    /// <param name="evaluation">評価</param>
    /// <param name="trouble">手数</param>
    public void ChangeInfo(int stageNum,int evaluation,int trouble) 
    {
        //g_stageScore.g_stageInfo[stageNum].g_evaluation = evaluation;
        //g_stageScore.g_stageInfo[stageNum].g_trouble = trouble;
    }
}
