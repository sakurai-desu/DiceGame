using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPote : MonoBehaviour
{
    private float g_rota_x;
    private float g_rota_y;

    private int g_camera_rote_num;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        #region コントローラーのカメラ操作  
        if (Input.GetButton("R")) {
            g_rota_y =0.3f;
            transform.Rotate( 0,g_rota_y, 0);
        }
       else if (Input.GetButton("L")) {
            g_rota_y =-0.3f;
            transform.Rotate(0,g_rota_y, 0);
        }
        else{
            transform.Rotate(0, 0, 0);
        }
        #endregion
    }
    public int CameraRote() {
        float g_rotate = transform.localEulerAngles.y;
        Debug.Log(g_rotate);
        if (g_rotate > 0&& g_rotate < 90) {
            g_camera_rote_num = 0;
            return g_camera_rote_num;
        } else if (g_rotate > 90 && g_rotate < 180) {
            g_camera_rote_num = 1;
            return g_camera_rote_num;
        }if (g_rotate > 180&& g_rotate > 295) {
            g_camera_rote_num = 1;
            return g_camera_rote_num;
        } else if (g_rotate > 295) {
            g_camera_rote_num = 0;
            return g_camera_rote_num;
        }
       

        return g_camera_rote_num;
    }
}
