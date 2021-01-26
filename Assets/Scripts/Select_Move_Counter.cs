using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class Select_Move_Counter : MonoBehaviour {
    [SerializeField]
    private TextMeshProUGUI g_trouble_text;

    private JsonArray g_json_Script;

    [SerializeField]
    public InputJson g_inputJson;

    [Serializable]
    public class InputJson {
        //ステージの手数
        public int g_trouble;
    }

    /// <summary>
    /// 呼び出すテキストの名前
    /// </summary>
    private string g_json_name = "";

    private string[] g_json_names;
    [SerializeField]
    private int[] g_stage_move_counts = new int[0];

    private void Start() {
        g_json_Script = GameObject.Find("Stageinformation").GetComponent<JsonArray>();
        g_json_names = g_json_Script.g_json_stage;
        for (int i = 0; i < g_json_names.Length; i++) {
            //ステージの手数を入れる配列のサイズを増やす
            Array.Resize(ref g_stage_move_counts, g_stage_move_counts.Length + 1);
            //取得したJsonの名前用配列から名前取得
            g_json_name = g_json_names[i];

            string datastr = "";
            StreamReader reader;
            reader = new StreamReader(Application.streamingAssetsPath + "/" + g_json_name);
            datastr = reader.ReadToEnd();
            reader.Close();
            //ステージデータを取り込む
            g_inputJson = JsonUtility.FromJson<InputJson>(datastr);
            //Jsonの中の手数を配列に保持
            g_stage_move_counts[i] = g_inputJson.g_trouble;
        }
    }

    /// <summary>
    /// 手数のテキストを変更する
    /// </summary>
    /// <param name="_ver">縦の指標</param>
    /// <param name="_side">横の指標</param>
    public void Move_Count_Text_Change(int _ver, int _side) {
        //選択したステージが一段目の時
        if (_ver > 0) {
            _side = (_ver * 4) + _side;
        }
        else {
            _side++;
        }
        int _pointer = _side;
        //テキスト変更する
        g_trouble_text.text = g_stage_move_counts[_pointer].ToString("D2");
    }
}