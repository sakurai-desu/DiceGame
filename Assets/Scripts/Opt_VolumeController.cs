using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opt_VolumeController : MonoBehaviour
{
    //音量設定オプションで取り扱うパーツ
    public Slider[] g_targetSlider = new Slider[2];
    //後々TextMeshProに変更
    public Text[] g_targetvalueText = new Text[2];
    //カーソルの割当変数
    //この仕様の場合、0はBGM、1はSEにあたる
    private int g_select_cursor = 0;
    //カーソル割当変数の制限値の設定。最大値を入力する。
    private int g_select_limit = 1;
    //各設定値の配列管理
    public float[] g_select_value;
    //共通の制限値
    private float g_max_value = 100;
    private float g_min_value = 0;
    private bool g_limit_sw = false;

    //項目選択時の引数 - 縦
    private float g_select_versw = 0;
    private bool g_count_sw = false;
    //項目選択時の引数 - 横
    public float g_select_horsw = 0;
    private bool g_conti_sw = false;

    //値変更時の間隔の設定
    public float g_move_value = 0;

    //効果音
    [SerializeField]
    AudioSource g_cursorSE=null;

    //操作対象の配列リスト
    [SerializeField]
    AudioSource[] g_bgmList=null;
    [SerializeField]
    AudioSource[] g_seList=null;
    
    [SerializeField]
    GameObject[] g_target_Parent=null;

    /*private*/[SerializeField] float[] g_target_yPos=null;

    // Start is called before the first frame update
    void Start()
    {
        //カーソル移動位置の設定
        for (int parentcount = 0; parentcount <= g_target_Parent.Length - 1; parentcount++)
        {
            g_target_yPos[parentcount] = g_target_Parent[parentcount].transform.position.y;
            print(g_target_yPos[parentcount]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        g_select_value = new float[2];
        //設定項目の値を設定
        g_select_value[0] = g_targetSlider[0].value;
        g_select_value[1] = g_targetSlider[1].value;

        //上下キーの制御
        g_select_versw = Input.GetAxisRaw("Vertical");
        //左右キーの制御
        g_select_horsw = Input.GetAxisRaw("Horizontal");


        //■処理の順序
        //上下キーでカーソル変数を変動
        if (g_select_versw >= 1 && g_count_sw == false)
        {
            if (g_select_cursor != 0)
            {
                g_select_cursor = g_select_cursor - 1;
                g_count_sw = true;
                print("上入力完了");
            }
            else
            {
                g_select_cursor = 0;
                print("これ以上、上に進めません");
            }
        }
        else if (g_select_versw <= -1 && g_count_sw == false)
        {
            if (g_select_cursor != g_select_limit)
            {
                g_select_cursor = g_select_cursor + 1;
                g_count_sw = true;
                print("下入力完了");
            }
            else
            {
                g_select_cursor = g_select_limit;
                print("これ以上、下に進めません");
            }
        }
        else if (g_select_versw == 0)
        {
            g_count_sw = false;
        }

        //左右キーで値変更処理(ここで増加か減少かを決定させる)
        if (g_select_horsw >= 1 && g_conti_sw == false)
        {
            g_conti_sw = true;
            g_cursorSE.Play();
            g_targetSlider[g_select_cursor].value = g_select_value[g_select_cursor] + g_move_value;
            print("右入力完了");
            ValueLimit();
        }
        else if (g_select_horsw <= -1 && g_conti_sw == false)
        {
            g_conti_sw = true;
            g_cursorSE.Play();
            g_targetSlider[g_select_cursor].value = g_select_value[g_select_cursor] - g_move_value;
            print("左入力完了");
            ValueLimit();
        }
        else if (g_select_horsw == 0)
        {
            g_conti_sw = false;
            g_targetvalueText[g_select_cursor].text = string.Format(g_select_value[g_select_cursor] + "%");
            AudioVolumeControl();
        }


        //switchでカーソル判定してから値変更処理を実行
    }

    private void ValueLimit()
    {
        if(g_select_horsw != 0)
        {
            if (g_select_value[g_select_cursor] > g_max_value)
            {
                g_select_value[g_select_cursor] = g_max_value;
                g_limit_sw = true;
                print("これ以上指定の方向に進めません。");
            }
            else if (g_select_value[g_select_cursor] < g_min_value)
            {
                g_select_value[g_select_cursor] = g_min_value;
                g_limit_sw = true;
                print("これ以上指定の方向に進めません。");
            }
            else
            {
                g_limit_sw = false;
            }
        }
    }

    private void AudioVolumeControl()
    {
        //音量の最小、最大値を制御する「ValueLimit」で何も制限がかからなかった場合に実行。
        //配列のBGMList,SEListからAudioSource.volumeの値を取得し、音量を変更する
        //select_cursorの値によってBGM,SEのどちらかを消すかを判定する
        //計算方式「AudioSource.volume / (select_value[select_cursor] * 0.01)」
        if(g_limit_sw == false)
        {
            if (g_select_cursor == 0)
            {
                for (int listcount = 0; listcount <= g_bgmList.Length - 1; listcount++)
                {
                    g_bgmList[listcount].volume = g_select_value[g_select_cursor] / 100;
                    print("bgm volume changed no." + listcount);
                }
            }
            else if (g_select_cursor == 1)
            {
                for (int listcount = 0; listcount <= g_seList.Length - 1; listcount++)
                {
                    g_seList[listcount].volume = g_select_value[g_select_cursor] / 100;
                    print("se volume changed no." + listcount);
                }
            }
        }
    }
}
