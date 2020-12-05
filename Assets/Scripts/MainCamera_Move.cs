using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Move : MonoBehaviour
{
    [SerializeField]
    private GameObject[] g_camera_Array;

    //配列内のどのカメラが選択されているかを判断するための数値
    public int g_camera_pointer;

    private GameObject g_this_Obj;

    Playermove g_play_move;

    //ボタンが押されているかどうかを判断するフラグ
    public bool g_button_push_flag;

    void Start()
    {
        g_this_Obj = this.gameObject;
        g_camera_pointer = 0;
        g_this_Obj.transform.parent = g_camera_Array[g_camera_pointer].transform;
        Local_PosAndRotation_Reset();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            GetPlayer();
            g_button_push_flag = true;
            Change_Right_Camra_Pos();
            CameraNum();
        } 
        if (Input.GetKeyDown(KeyCode.Q)) {
            GetPlayer();
            g_button_push_flag = true;
            Change_Left_Camra_Pos();
            CameraNum();
        } 
    }

    private void Change_Left_Camra_Pos() {
        g_camera_pointer++;
        if (g_camera_pointer>=g_camera_Array.Length) {
            g_camera_pointer = 0;
        }
        g_this_Obj.transform.parent = g_camera_Array[g_camera_pointer].transform;
        Local_PosAndRotation_Reset();
    }

    private void Change_Right_Camra_Pos() {
        g_camera_pointer--;
        if (g_camera_pointer < 0) {
            g_camera_pointer = g_camera_Array.Length-1;
        }
        g_this_Obj.transform.parent = g_camera_Array[g_camera_pointer].transform;
        Local_PosAndRotation_Reset();
    }

    private void Local_PosAndRotation_Reset() {
        g_this_Obj.transform.localPosition= new Vector3(0, 0, 0);
        g_this_Obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    /// <summary>
    /// プレイヤーを取得
    /// </summary>
    private void GetPlayer() {
        g_play_move = GameObject.Find("Player_Parent(Clone)").GetComponent<Playermove>();
    }
    /// <summary>
    /// プレイヤー内の数値変更
    /// </summary>
    void CameraNum() {
        g_play_move.g_camera_num = g_camera_pointer;
    }
}
