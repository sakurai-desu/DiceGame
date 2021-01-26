using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Appearance_Move : MonoBehaviour {
    private Game_Controller g_game_con_Script;
    private Playercontroller g_player_Script;
    private Player_Animation g_player_anim_Script;
    StageInformation g_information_Script;
    TroubleScr g_trouble_Script;
    ResultScript g_result_Script;
    /// <summary>
    /// プレイヤーのオブジェクト
    /// </summary>
    private GameObject g_player_obj;
    [SerializeField]
    private GameObject g_player_body;

    /// <summary>
    /// 移動先の座標
    /// </summary>
    private Vector3 g_end_Pos;
    /// <summary>
    /// 初期化用変数
    /// </summary>
    private const int g_zero_count = 0;
    /// <summary>
    /// 移動速度の最大値
    /// </summary>
    private const float g_max_speed = 8;
    /// <summary>
    /// 移動速度
    /// </summary>
    private float g_move_speed = 8;
    /// <summary>
    /// 移動速度を加算するための変数
    /// </summary>
    private float g_plus_speed = 0;
    /// <summary>
    /// 移動中か判別するフラグ・True：移動中・False:停止中
    /// </summary>
    private bool g_is_move = false;

    private float g_check_pos_length = 0.5f;

    Folder_Script g_folder_Script;
    //取得した手数を入れるためのもの
    private int g_trouble_num;

    private bool g_one_flag;
    /// <summary>
    /// 増えた評価の数
    /// </summary>
    private const int g_max_trouble_num=10;
    Stage_Clear g_clear_Script;
    void Start() {
        g_trouble_Script = GameObject.Find("TroubleObj").GetComponent<TroubleScr>();
        g_result_Script = GameObject.Find("Stageinformation").GetComponent<ResultScript>();
        g_clear_Script = GameObject.Find("Game_Controller").GetComponent<Stage_Clear>();
        g_folder_Script = GameObject.Find("Stageinformation").GetComponent<Folder_Script>();
        g_information_Script = GameObject.Find("Stageinformation").GetComponent<StageInformation>();
        g_game_con_Script = GameObject.Find("Game_Controller").GetComponent<Game_Controller>();
        g_player_Script = GameObject.Find("Player_Controller").GetComponent<Playercontroller>();
        g_player_anim_Script = this.GetComponent<Player_Animation>();
        g_player_obj = this.gameObject;
    }

    /// <summary>
    /// 移動先を変更し移動する
    /// </summary>
    /// <param name="end_Pos"></param>
    public void Player_Move(Vector3 move_Pos) {
        //移動先を取得
        g_end_Pos = move_Pos;
        //プレイヤーを移動させる
        StartCoroutine(Player_Smooth_Move());
    }

    /// <summary>
    /// プレイヤーをなめらかに動かす処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Player_Smooth_Move() {
        //移動中フラグON
        g_player_Script.MoveFlag_True();
        //速度初期化
        g_move_speed = g_zero_count;
        //速度加算変数初期化
        g_plus_speed = g_zero_count;
        //プレイヤーの位置が移動先につくまで繰り返す
        //while (g_player_body.transform.position != g_end_Pos) {
            while (g_player_body.transform.position.x < g_end_Pos.x - g_check_pos_length ||
                   g_player_body.transform.position.x > g_end_Pos.x + g_check_pos_length ||
                   g_player_body.transform.position.y < g_end_Pos.y - g_check_pos_length ||
                   g_player_body.transform.position.y > g_end_Pos.y + g_check_pos_length ||
                   g_player_body.transform.position.z < g_end_Pos.z - g_check_pos_length ||
                   g_player_body.transform.position.z > g_end_Pos.z + g_check_pos_length) {
                //移動速度上げる
                if (g_move_speed < g_max_speed) {
                g_move_speed = g_move_speed + g_plus_speed;
                g_plus_speed = g_plus_speed + 0.2f;
            }
            //移動先に向かって移動
            g_player_obj.transform.position = Vector3.MoveTowards(g_player_obj.transform.position,
              g_end_Pos, g_move_speed * Time.deltaTime);
            yield return null;
        }
        //移動先への移動終了
        g_player_obj.transform.position = g_end_Pos;
        g_player_Script.MoveFlag_False();
        g_player_anim_Script.Flag_False();
        Goal();
        yield break;
    }

    private void Goal() {
        (int p_ver, int p_side, int p_high) = g_player_Script.Get_Player_Pointer();
        int type = g_game_con_Script.Get_Obj_Type(p_ver, p_side, p_high - 1);
        if (type == 20) {
            if (SceneManager.GetActiveScene().name == "MainScene") {
                g_trouble_num = g_trouble_Script.g_troublenum;
                Debug.Log(g_trouble_num);
                //評価するためのスクリプトを呼び出す
                g_result_Script.Trouble_Eva(g_trouble_num,g_max_trouble_num,g_max_trouble_num*0.5f,g_max_trouble_num*0.1f);
                g_clear_Script.This_Stage_Clear();
            }
           else if (SceneManager.GetActiveScene().name == "TutrialScene") {
                if (g_one_flag == false) {
                    g_information_Script.g_tutorial_num++;
                    g_one_flag = true;
                }
                if (g_information_Script.g_tutorial_num == g_folder_Script.Filenum("T*.json")) {
                    SceneManager.LoadScene("SelectScene");
                } else {

                    g_information_Script.Change_Tutorial("/Tutorial/TStage00"+g_information_Script.g_tutorial_num+".json");
                    SceneManager.LoadScene("TutrialScene");
                }

            }
           
        }
    }
}
