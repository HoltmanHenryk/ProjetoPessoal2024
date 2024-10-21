using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;


public class fade_on_out : MonoBehaviour
{

    public CanvasGroup canvas_group;
    public bool fadein = false;
    public bool fadeout = false;

    public float time_to_fade;
    void Update(){

        if(fadein) {
            if(canvas_group.alpha < 1) {
                canvas_group.alpha += time_to_fade * Time.deltaTime;
                if(canvas_group.alpha >= 1) {
                    fadein = false;
                }
            }

        }

        if (fadeout) {
            if(canvas_group.alpha >0){
                canvas_group.alpha -= time_to_fade * Time.deltaTime;
                if(canvas_group.alpha == 0) {
                    fadeout = false;
                }
            }
        }
        
    }

    public void Fade_in(){
        fadein = true;
    }

    public void Fade_out() {
        fadeout = true;
    }
}
