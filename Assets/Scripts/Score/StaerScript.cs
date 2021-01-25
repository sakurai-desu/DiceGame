using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージセレクトの評価を管理しているスクリプト
/// </summary>
public class StaerScript : MonoBehaviour
{
    /// <summary>
    /// ステージごとのスコアを入れているスクリプト
    /// </summary>
    private ScoreJsonScript g_scoreJsonScript;
    /// <summary>
    /// 選択中のステージ数などが入っているスクリプト
    /// </summary>
    private StageInformation g_stageInformationScript;
    /// <summary>
    /// 評価を表示するための画像を入れる配列
    /// </summary>
    public Image[] g_images;
    /// <summary>
    /// 評価の段階数(星の数)
    /// </summary>
    private const int g_imagesNum = 3;

    private void Start() 
    {
        g_scoreJsonScript = GameObject.Find("ScoreInformation").GetComponent<ScoreJsonScript>();
        g_stageInformationScript = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
    }

    /// <summary>
    /// 星をセットする数を取得して次の処理へ進めさせるためのメソッド
    /// </summary>
    /// <param name="scorePointer">評価の配列を検索させるためのポインター</param>
    public  void SetStaer(int scorePointer) 
    {
        //配列召喚
        ChangeSter(g_scoreJsonScript.g_stageScore.g_stageInfo[scorePointer].g_evaluation);
    }
    /// <summary>
    /// 表示する評価を変更させるメソッド
    /// </summary>
    /// <param name="evoNum">評価数</param>
    public void ChangeSter(int evoNum) 
    {
        //評価の最大数までfor文を回す
        for (int i = 1; i <= g_imagesNum; i++) 
        {
            //評価がゼロ以上の時
            if (evoNum != 0) {
                if (i <= evoNum) {
                    g_images[i].color = new Color32(255, 255, 255, 255);
                } else {
                    g_images[i].color = new Color32(60, 60, 60, 255);
                }
                Debug.Log(evoNum);
            }
            //評価がゼロの時
            else {
                g_images[i].color = new Color32(60, 60, 60, 255);
            }
        }
    }
}
