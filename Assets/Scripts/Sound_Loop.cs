using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Loop : MonoBehaviour
{
    private AudioSource g_audiosource = null;
    private float g_timer = 0;
    [SerializeField]
    private float g_start_time = 0;
    [SerializeField]
    private float g_end_time = 0;

    private void Start()
    {
        g_audiosource = this.GetComponent<AudioSource>();
        g_audiosource.Play();
    }

    private void Update()
    {
        g_timer += Time.deltaTime;
        if (g_timer >= g_end_time) {
            g_audiosource.Stop();
            g_audiosource.time = g_start_time;
            g_audiosource.Play();
            g_timer = 0;
        }
    }
}
