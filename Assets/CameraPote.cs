using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPote : MonoBehaviour
{
    private float g_rota_y;

    private int g_camera_rote_num;

    Vector3 g_camera_rotate_euler;

    bool g_push_y_flag;
    bool g_push_r_flag;
    bool g_push_q_flag;
    bool g_push_e_flag;
    // Start is called before the first frame update
    void Start()
    {
        g_camera_rotate_euler = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update() {
        #region コントローラーのカメラ操作 
        //カメラを回転させるモードにする
        if (Input.GetButtonDown("X")) {
            g_push_y_flag = true;
        } 
        //話したらカメラを戻す
        else if(Input.GetButtonUp("X")&&g_push_y_flag) {
            transform.localEulerAngles = g_camera_rotate_euler;
            g_push_y_flag = false;
        }
        //カメラを回転するモードで止める
        if (Input.GetButtonDown("R")) {
            g_push_r_flag = true;
        } else if(Input.GetButtonUp("R")&&g_push_r_flag) {
            g_push_r_flag = false;
        }
        if (g_push_y_flag) {

            if (Input.GetAxis("CameraX") !=0&&g_push_r_flag==false) {
                float g_axis = Input.GetAxis("CameraX");
                g_rota_y = g_axis;
                Debug.Log(g_axis);
                transform.Rotate(0, g_rota_y, 0);
            }
               
        }
            if (Input.GetKey(KeyCode.Q)) {

                g_rota_y = 1;
                transform.Rotate(0, g_rota_y, 0);
            } else if (Input.GetKey(KeyCode.E)) {

                g_rota_y = -1;
                transform.Rotate(0, g_rota_y, 0);
            }

        #endregion
    }
}
