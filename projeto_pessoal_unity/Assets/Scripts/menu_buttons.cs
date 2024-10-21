using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu_buttons : MonoBehaviour
{

    fade_on_out fade;
    
    public GameObject jogar_button;

    public Slider sound_slider;
    public float_pers pers_data;

    public GameObject settings_obj;
    public GameObject model_settings;


    // Start is called before the first frame update
    void Start()
    {
        fade = FindObjectOfType<fade_on_out>();

        sound_slider.value = pers_data.Value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public IEnumerator jogar_fade(){
        fade.Fade_in();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Jogo");

    }

    public IEnumerator config_voltar(){
        
        model_settings.transform.Rotate(10, 10, 10);
        yield return new WaitForSeconds(1);
        settings_obj.SetActive(false);

    }

    public void start_game(){
        
        /*jogar_button.transform.localScale = new Vector3(1000, 1000, 1000);
        SceneManager.LoadScene("Jogo");*/

        StartCoroutine(jogar_fade());

    }

    public void sair() {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void settings_open() {
        settings_obj.SetActive(true);

    }

    public void settings_close() {
        settings_obj.SetActive(false);
    }

    public void on_sound_slider() {
        
        Debug.Log( "[ProjetoPessoal] "+ "Volume slider: " + sound_slider.value);

        
        pers_data.Value = sound_slider.value;
        
        model_settings.GetComponent<rotate_menu>().set_speed_value(pers_data.Value * 100 * 6);

        //Vector3 rot = new(0, 0, sound_slider.value);


       // model_settings.transform.localRotation = Quaternion.Euler(0, 0, -sound_slider.value);


        
    }
}
