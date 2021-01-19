using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Se_Source : MonoBehaviour
{
    private AudioSource g_audiosource;

    //効果音 0～1 = サイコロの効果音、2～5 = システム効果音
    [SerializeField]
    private AudioClip[] g_system_se;

    void Start()
    {
        DontDestroyOnLoad(this);
        g_audiosource = this.GetComponent<AudioSource>();
        g_audiosource.mute=true;
    }

    void Update()
    {
        
    }
    /// <summary>
    /// SEを再生
    /// </summary>
    public void Se_Play(int soundNum) {
        g_audiosource.mute = false;
        g_audiosource.Stop();
        g_audiosource.clip = g_system_se[soundNum];
        g_audiosource.Play();
    }
}
