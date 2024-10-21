using System.Collections;
using System.Collections.Generic;
using FMOD;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class VCAcontroller : MonoBehaviour
{

    FMOD.Studio.EventInstance volume_event;

    private FMOD.Studio.VCA vca_controller;
    FMOD.Studio.Bus Master;

    public float_pers pers_data;

    void Start() {
        vca_controller = FMODUnity.RuntimeManager.GetVCA("vca:/Master");
    }

    /* void Awake() {
        vca_controller.setVolume(pers_data.Value);
    } */



    // Update is called once per frame
    void Update()
    {
        vca_controller.setVolume(pers_data.Value);
    }
}
