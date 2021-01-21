using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScoreJsonScript : MonoBehaviour
{
    private Score g_stageScore=null;
    /// <summary>
    /// ステージごとのスコアを保存するクラスの親
    /// </summary>
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
    /// ストリーミングアセット内の.jsonフォルダの数を数えるスクリプト
    /// </summary>
    private JsonArray g_jsonArrayScript;

    private void Awake() 
    {
        DontDestroyOnLoad(this.gameObject);
        g_jsonArrayScript = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
    }
    void Start()
    {

        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.streamingAssetsPath + "/Score/Score.json");
        datastr = reader.ReadToEnd();
        reader.Close();
        g_stageScore = JsonUtility.FromJson<Score>(datastr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
