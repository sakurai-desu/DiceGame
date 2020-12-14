using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Se_Source : MonoBehaviour
{
    private AudioSource g_audiosource;
    [SerializeField]
    private AudioClip g_dice_rotate_se;
    [SerializeField]
    private AudioClip g_dice_docking_se;

    void Start()
    {
        g_audiosource = this.GetComponent<AudioSource>();
        g_audiosource.mute=true;
    }

    void Update()
    {
        
    }
    /// <summary>
    /// 回転用SEを再生
    /// </summary>
    public void Dice_Rotate_Se_Play() {
        g_audiosource.mute = false;
        g_audiosource.Stop();
        g_audiosource.clip = g_dice_rotate_se;
        g_audiosource.Play();
    }
    /// <summary>
    /// 合体用SEを再生
    /// </summary>
    public void Dice_Docking_Se_Play() {
        g_audiosource.Stop();
        g_audiosource.clip = g_dice_docking_se;
        g_audiosource.Play();
    }
}
