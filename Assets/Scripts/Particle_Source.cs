using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Source : MonoBehaviour {
    [SerializeField]
    private GameObject g_docking_particle;

    /// <summary>
    /// 回転の中心
    /// </summary>
    private Vector3 g_rotate_Point = Vector3.zero;
    /// <summary>
    /// 回転の軸
    /// </summary>
    private Vector3 g_rotate_Axis = Vector3.zero;
    /// <summary>
    /// サイコロの回転させる角度
    /// </summary>
    private float g_rotation_Amount = -90;
    /// <summary>
    /// サイコロのサイズ
    /// </summary>
    private float g_dice_Size;
    private bool g_is_start = false;

    public void Docking_Particle_Play(GameObject docking_dice, int particle_pointer) {
        if (!g_is_start) {
            return;
        }
        //回転の中心を初期化
        g_rotate_Point = Vector3.zero;
        //パーティクル生成
        GameObject docking_particle = Instantiate(g_docking_particle);
        //サイズを求める
        g_dice_Size = docking_dice.transform.localScale.x / 1;
        switch (particle_pointer) {
            case 0:
                g_rotate_Point = docking_dice.transform.position + new Vector3(0, 0, g_dice_Size);
                docking_particle.transform.Rotate(new Vector3(0, 0, 90));
                break;
            case 1:
                g_rotate_Point = docking_dice.transform.position + new Vector3(0, 0, -g_dice_Size);
                docking_particle.transform.Rotate(new Vector3(0, 0, 90));
                break;
            case 2:
                g_rotate_Point = docking_dice.transform.position + new Vector3(g_dice_Size, 0, 0);
                docking_particle.transform.Rotate(new Vector3(0, 90, 0));
                break;
            case 3:
                g_rotate_Point = docking_dice.transform.position + new Vector3(-g_dice_Size, 0, 0);
                docking_particle.transform.Rotate(new Vector3(0, 90, 0));
                break;
            case 4:
                g_rotate_Point = docking_dice.transform.position + new Vector3(0, g_dice_Size, 0);
                docking_particle.transform.Rotate(new Vector3(90, 0, 0));
                break;
            case 5:
                g_rotate_Point = docking_dice.transform.position + new Vector3(0, -g_dice_Size, 0);
                docking_particle.transform.Rotate(new Vector3(90, 0, 0));
                break;
        }
        //パーティクルの生成位置変更
        docking_particle.transform.position = g_rotate_Point;
    }
    
    public IEnumerator Change_Start_Flag_(bool _flag) {
        g_is_start = _flag;
        yield break;
    }

}
