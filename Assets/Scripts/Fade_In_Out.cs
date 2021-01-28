using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_In_Out : MonoBehaviour
{
    /// <summary>
    /// 色を変えたいオブジェクト
    /// </summary>
    private Image g_fade_image;
    /// <summary>
    /// フェードイン後の色（透明）
    /// </summary>
    private Color g_fade_in_color = new Color(0, 0, 0, 0);
    /// <summary>
    /// フェードアウト後の色（真っ黒）
    /// </summary>
    private Color g_fade_out_color = new Color(0, 0, 0, 1);
    /// <summary>
    /// 色を変更してから次に変更するまでの待ち時間
    /// </summary>
    private float g_wait_time=0.01f;
    /// <summary>
    /// 透明度を変更する際の加算量
    /// </summary>
    private float g_plus_value = 0.06f;

    private void Awake()
    {
        g_fade_image = this.GetComponent<Image>();
    }
    
    /// <summary>
    /// フェードアウト後に与えられた処理をする処理
    /// </summary>
    /// <param name="_move_method">フェードアウト後に実行したい処理</param>
    public void Start_Fade_Out(IEnumerator _move_method)
    {
        StartCoroutine(Fade_Out(_move_method));
    }

    /// <summary>
    /// フェードアウト処理
    /// </summary>
    /// <param name="_move_method">フェードアウト後に実行したい処理</param>
    /// <returns></returns>
    private IEnumerator Fade_Out(IEnumerator _move_method)
    {
        //イメージを透明な状態からスタートする
        g_fade_image.color = g_fade_in_color;
        //透明度
        float _fade_plus = g_fade_in_color.a;
        //イメージが真っ黒になるまで続ける
        while (_fade_plus < g_fade_out_color.a)
        {
            //色変更
            g_fade_image.color = new Color(0, 0, 0, _fade_plus);
            //少しづつ黒く
            _fade_plus += g_plus_value;
            //一定時間待つ
            yield return new WaitForSeconds(g_wait_time);
        }
        //色を真っ黒にする
        g_fade_image.color = g_fade_out_color;
        //与えられた処理を実行
        StartCoroutine(_move_method);
        yield break;
    }

    /// <summary>
    /// フェードイン後に与えられた処理をする処理
    /// </summary>
    /// <param name="_move_method">フェードイン後に実行したい処理</param>
    public void Start_Fade_In(IEnumerator _start_method)
    {
        StartCoroutine(Fade_In(_start_method));
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <param name="_start_method">フェードイン後に実行した処理</param>
    /// <returns></returns>
    private IEnumerator Fade_In(IEnumerator _start_method)
    {
        //イメージを真っ黒な状態からスタート
        g_fade_image.color = g_fade_out_color;
        //透明度
        float _fade_plus = g_fade_out_color.a;
        //透明になるまで繰り返す
        while (_fade_plus > g_fade_in_color.a)
        {
            //色変更
            g_fade_image.color = new Color(0, 0, 0, _fade_plus);
            //少しづつ透明に
            _fade_plus -= g_plus_value;
            //一定時間待つ
            yield return new WaitForSeconds(g_wait_time);
        }
        //イメージを透明にする
        g_fade_image.color = g_fade_in_color;
        //与えられた処理を実行
        StartCoroutine(_start_method);
        yield break;
    }

    /// <summary>
    /// ゲームスタート時のフェードイン処理
    /// </summary>
    public void GameStart_Fade_In() {
        StartCoroutine(GameStart());
    }

    /// <summary>
    /// フェードイン処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator GameStart() {
        //イメージを真っ黒な状態からスタート
        g_fade_image.color = g_fade_out_color;
        //透明度
        float _fade_plus = g_fade_out_color.a;
        //透明になるまで繰り返す
        while (_fade_plus > g_fade_in_color.a) {
            //色変更
            g_fade_image.color = new Color(0, 0, 0, _fade_plus);
            //少しづつ透明に
            _fade_plus -= g_plus_value;
            //一定時間待つ
            yield return new WaitForSeconds(g_wait_time);
        }
        //イメージを透明にする
        g_fade_image.color = g_fade_in_color;

        yield break;
    }

}
