using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeout_controller : MonoBehaviour
{

    fade_on_out fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<fade_on_out>();

        fade.Fade_out();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
