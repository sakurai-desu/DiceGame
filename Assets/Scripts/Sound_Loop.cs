using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Loop : MonoBehaviour
{
    private AudioSource g_audiosource = null;
    /// <summary>
    /// ループ用タイマー
    /// </summary>
    private float g_timer = 0;
    /// <summary>
    /// BGMの開始時間
    /// </summary>
    [SerializeField]
    private float g_start_time = 0;
    /// <summary>
    /// BGMの終了時間
    /// </summary>
    [SerializeField]
    private float g_end_time = 0;

    private void Start()
    {
        g_audiosource = this.GetComponent<AudioSource>();
        //BGMを開始
        g_audiosource.Play();
    }

    private void Update()
    {
        //タイマー
        g_timer += Time.deltaTime;
        //タイマーが終了時間に達したとき
        if (g_timer >= g_end_time) {
            //BGM停止
            g_audiosource.Stop();
            //BGMの時間を初期化
            g_audiosource.time = g_start_time;
            //BGMを再生
            g_audiosource.Play();
            //タイマーリセット
            g_timer = 0;
        }
    }
}
