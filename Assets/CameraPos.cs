using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    //jsonを保持しているスクリプト
    Input_Date g_input;

    //自分がどのカメラかどうかを判断する数値
    [SerializeField]
    private int g_camera_num;

    //横の幅
    [SerializeField]
    private int g_side_pos;
    //高さの幅
    [SerializeField]
    private int g_high_pos;
    //縦の幅
    [SerializeField]
    private int g_var_pos;

    //どのカメラかを判断するのに使う数値
    const int g_rghitcamera=0;
    const int g_leftcamera=1;
    const int g_rightbackcamera=2;
    const int g_leftbackcamera=3;


    int g_change_hpos_num;
    int g_change_Vpos_num;
    int g_change_Spos_num;
    // Start is called before the first frame update
    void Start()
    {
        //jsonを読み込んでいるスクリプトを保存しているスクリプト
        g_input = GameObject.Find("Game_Controller").GetComponent<Input_Date>();
        //ステージの広さを代入
        g_side_pos = g_input.g_inputJson.g_hori;
        g_high_pos = g_input.g_inputJson.g_high;
        g_var_pos = g_input.g_inputJson.g_ver;
        #region ステージのサイズ判定
        //ステージの高さがどれぐらいかを検知
        if (g_high_pos >= 6 && g_high_pos <= 9) {
            g_change_hpos_num = 1;
        } else if (g_high_pos > 9) {

            g_change_hpos_num = 2;
        }
        //ステージの幅がどれぐらいかを検知
        if (g_side_pos >= 6 && g_side_pos <= 9) {
            g_change_Spos_num = 3;
        } else if (g_side_pos > 9) {

            g_change_Spos_num = 4;
        }
        //ステージの奥行がどれぐらいかを検知
        if (g_var_pos >= 6 && g_var_pos <= 9) {
            g_change_Vpos_num = 5;
        } else if (g_var_pos > 9) {

            g_change_Vpos_num = 6;
        }
        #endregion
        //自信のオブジェクトがどこのポジションのカメラ化を判断する
        switch (g_camera_num) {
            case g_rghitcamera:
                CameraSet(g_side_pos, g_high_pos, -g_var_pos+1);
                break;
            case g_leftcamera:
                CameraSet(0, g_high_pos,-g_var_pos+1);
                break;
            case g_rightbackcamera:
                CameraSet(0, g_high_pos, g_var_pos + g_change_Vpos_num-1);
                break;
            case g_leftbackcamera:
                CameraSet(g_side_pos, g_high_pos, g_var_pos + g_change_Vpos_num-1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CameraSet(int sidepos, int highpos, int varpos) {
        gameObject.transform.position = new Vector3(sidepos, highpos, varpos);
    }
}
