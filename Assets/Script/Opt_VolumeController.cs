using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opt_VolumeController : MonoBehaviour
{
    //音量設定オプションで取り扱うパーツ
    public Slider[] targetSlider = new Slider[2];
    //後々TextMeshProに変更
    public Text[] targetvalueText = new Text[2];
    //カーソルの割当変数
    //この仕様の場合、0はBGM、1はSEにあたる
    private int select_cursor = 0;
    //カーソル割当変数の制限値の設定。最大値を入力する。
    private int select_limit = 1;
    //各設定値の配列管理
    public float[] select_value;
    //共通の制限値
    private float max_value = 100;
    private float min_value = 0;
    private bool limit_sw = false;

    //項目選択時の引数 - 縦
    private float select_versw = 0;
    private bool count_sw = false;
    //項目選択時の引数 - 横
    public float select_horsw = 0;
    private float conti_time = 0;
    private bool conti_sw = false;

    //値変更時の間隔の設定
    public float move_value = 0;

    //効果音
    [SerializeField]
    AudioSource cursorSE;

    //操作対象の配列リスト
    [SerializeField]
    AudioSource[] BGMList;
    [SerializeField]
    AudioSource[] SEList;

    //縦移動用のカーソル
    [SerializeField]
    Image cursorImage;
    [SerializeField]
    GameObject[] target_Parent;

    /*private*/[SerializeField] float[] target_yPos;

    // Start is called before the first frame update
    void Start()
    {
        //カーソル移動位置の設定
        for (int parentcount = 0; parentcount <= target_Parent.Length - 1; parentcount++)
        {
            target_yPos[parentcount] = target_Parent[parentcount].transform.position.y;
            print(target_yPos[parentcount]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        select_value = new float[2];
        //設定項目の値を設定
        select_value[0] = targetSlider[0].value;
        select_value[1] = targetSlider[1].value;

        //上下キーの制御
        select_versw = Input.GetAxisRaw("Vertical");
        //左右キーの制御
        select_horsw = Input.GetAxisRaw("Horizontal");


        //■処理の順序
        //上下キーでカーソル変数を変動
        if (select_versw >= 1 && count_sw == false)
        {
            if (select_cursor != 0)
            {
                select_cursor = select_cursor - 1;
                count_sw = true;
                print("上入力完了");
            }
            else
            {
                select_cursor = 0;
                print("これ以上、上に進めません");
            }
        }
        else if (select_versw <= -1 && count_sw == false)
        {
            if (select_cursor != select_limit)
            {
                select_cursor = select_cursor + 1;
                count_sw = true;
                print("下入力完了");
            }
            else
            {
                select_cursor = select_limit;
                print("これ以上、下に進めません");
            }
        }
        else if (select_versw == 0)
        {
            count_sw = false;
        }

        //左右キーで値変更処理(ここで増加か減少かを決定させる)
        if (select_horsw >= 1 && conti_sw == false)
        {
            conti_sw = true;
            cursorSE.Play();
            targetSlider[select_cursor].value = select_value[select_cursor] + move_value;
            print("右入力完了");
            ValueLimit();
        }
        else if (select_horsw <= -1 && conti_sw == false)
        {
            conti_sw = true;
            cursorSE.Play();
            targetSlider[select_cursor].value = select_value[select_cursor] - move_value;
            print("左入力完了");
            ValueLimit();
        }
        else if (select_horsw == 0)
        {
            conti_sw = false;
            targetvalueText[select_cursor].text = string.Format(select_value[select_cursor] + "%");
            AudioVolumeControl();
        }


        //switchでカーソル判定してから値変更処理を実行
    }

    private void ValueLimit()
    {
        if(select_horsw != 0)
        {
            if (select_value[select_cursor] > max_value)
            {
                select_value[select_cursor] = max_value;
                limit_sw = true;
                print("これ以上指定の方向に進めません。");
            }
            else if (select_value[select_cursor] < min_value)
            {
                select_value[select_cursor] = min_value;
                limit_sw = true;
                print("これ以上指定の方向に進めません。");
            }
            else
            {
                limit_sw = false;
            }
        }
    }

    private void AudioVolumeControl()
    {
        //音量の最小、最大値を制御する「ValueLimit」で何も制限がかからなかった場合に実行。
        //配列のBGMList,SEListからAudioSource.volumeの値を取得し、音量を変更する
        //select_cursorの値によってBGM,SEのどちらかを消すかを判定する
        //計算方式「AudioSource.volume / (select_value[select_cursor] * 0.01)」
        if(limit_sw == false)
        {
            if (select_cursor == 0)
            {
                for (int listcount = 0; listcount <= BGMList.Length - 1; listcount++)
                {
                    BGMList[listcount].volume = select_value[select_cursor] / 100;
                    print("bgm volume changed no." + listcount);
                }
            }
            else if (select_cursor == 1)
            {
                for (int listcount = 0; listcount <= SEList.Length - 1; listcount++)
                {
                    SEList[listcount].volume = select_value[select_cursor] / 100;
                    print("se volume changed no." + listcount);
                }
            }
        }
    }
}
