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

    private PlayerXbox g_player_con_Script;
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
        if (Input.GetKeyDown(KeyCode.E)||Input.GetButtonDown("R")) {
            g_button_push_flag = true;
            Change_Right_Camra_Pos();
            CameraNum();
        } 
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetButtonDown("L")) {
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
    /// プレイヤー内の数値変更
    /// </summary>
    void CameraNum() {
        g_player_con_Script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerXbox>();
        g_player_con_Script.Change_CameraNum(g_camera_pointer);

    }
}
