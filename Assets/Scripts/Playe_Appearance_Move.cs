using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playe_Appearance_Move : MonoBehaviour
{
    [SerializeField]
    private GameObject g_player_obj;

    [SerializeField]
    private Vector3 g_end_Pos;

    [SerializeField]
    private float g_move_speed;

    void Start() {

    }

    /// <summary>
    /// 移動先を変更し移動する
    /// </summary>
    /// <param name="end_Pos"></param>
    private void Player_Move(Vector3 end_Pos) {
        g_end_Pos = end_Pos;
        Player_Smooth_Move();
    }

    /// <summary>
    /// プレイヤーをなめらかに動かす処理
    /// </summary>
    /// <returns></returns>
    IEnumerator Player_Smooth_Move() {
        while (g_player_obj.transform.position != g_end_Pos) {
            g_player_obj.transform.position = Vector3.MoveTowards(g_player_obj.transform.position,
              g_end_Pos, g_move_speed * Time.deltaTime);
            yield return null;
        }
        yield break;
    }
}
