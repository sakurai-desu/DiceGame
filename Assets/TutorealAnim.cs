using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class TutorealAnim : MonoBehaviour
{
    Animator g_tuto_anim;
    float g_destroytimer;
    bool g_destroyflag;
    // Start is called before the first frame update
    void Start()
    {
        g_tuto_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) {
            g_destroyflag = true;
            g_tuto_anim.SetBool("EndFlag", true);
        }
        if (g_destroyflag) {
            g_destroytimer+=Time.deltaTime;
        }
        if (g_destroytimer > 4) {
            Destroy(this.gameObject);
        }
    }
}
