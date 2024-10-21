using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class js_trigger : MonoBehaviour
{
    [Header("----------------")]
    [Header("Logica")]

    public GameObject text_toggle;

    public GameObject player;

    public GameObject remover;

    [Header("----------------")]
    [Header("HUD")]

    public static Color custom_red;
    public static Color custom_green;

    public Text dinheiro_text;
    public Text removedor_text;
    public Text notas_fisc_text;

    void Start (){
        custom_red = new Color(0.72f, 0.23f, 0.23f);
        custom_green = new Color(0.27f, 0.63f, 0.21f);
    }

    

    // Start is called before the first frame update
    void OnTriggerEnter(Collider other){
        text_toggle.SetActive(true);

        if(gameObject.tag == "JS") {
            js_logic();
            //player.GetComponent<PlayerMovement>().play_hand_anim();
        }

        if(gameObject.tag == "dinheiro") {
            sonegar_logic();
        }

        if(gameObject.tag == "triturador") {
            triturador_logic();
        }

        if(gameObject.tag == "removedor") {
            removedor_logic();
        }

        if(gameObject.tag == "exit") {
            win_logic();
        }

    }

    void OnTriggerExit(Collider other) {
        text_toggle.SetActive(false);
    }


    void js_logic() {
        Debug.Log("js_logic");

        if(player.GetComponent<PlayerMovement>().get_money_state()){
            Debug.Log("tem dinheiro para repor o computador");
            remover.SetActive(false);
            notas_fisc_text.color = custom_green;
            //notas_fisc_text.text = "Sim";

            player.GetComponent<PlayerMovement>().set_js_removed_state(true);
        }
        else {
            Debug.Log("não tem dinheiro para repor o computador");
        }
    }

    void sonegar_logic() { // pegar dinheiro
        Debug.Log("sonegar_logic");
        remover.SetActive(false);
        player.GetComponent<PlayerMovement>().set_money_state(true);
        dinheiro_text.color = custom_green;
        //dinheiro_text.text = "Sim";

        
    }

    void triturador_logic() {
        Debug.Log("triturador_logic");

        if(player.GetComponent<PlayerMovement>().get_js_removed_state()){ // passa se o js foi deletado
            if(player.GetComponent<PlayerMovement>().get_money_state()) { //passa se tem dinheiro
                /*logica deletar dinheiro*/
                Debug.Log("dinheiro sera deletado");
                player.GetComponent<PlayerMovement>().set_money_destroyed_state(true);
                player.GetComponent<PlayerMovement>().set_money_state(false);
                Debug.Log("dinhero destruido e removido do player");
                dinheiro_text.color = custom_red;
                //dinheiro_text.text = "Não";

                notas_fisc_text.color = custom_red;
                //notas_fisc_text.text = "Não";
            }
            else { // else não tem dinheiro
                Debug.Log("não tem dinheiro");
            }
        }
        else{ // else não deletou js
            Debug.Log("JS tem que ser removido antes");
        }
    }

    void removedor_logic() {
        Debug.Log("removedor_logic");

        if(player.GetComponent<PlayerMovement>().get_money_destroyed_state()) { // passa se dinheiro destruido

            if(player.GetComponent<PlayerMovement>().get_js_removed_state()) { //passa se js removido
                /* logica removedor */
                Debug.Log("removido");
                player.GetComponent<PlayerMovement>().set_ifpr_removed_state(true);
                removedor_text.color = custom_green;
                //removedor_text.text = "Sim";
            }

        }

    }

    void win_logic() {
        Debug.Log("win_logic");

        if(player.GetComponent<PlayerMovement>().get_ifpr_removed_state()) {
            if(player.GetComponent<PlayerMovement>().get_money_destroyed_state()) {
                if(player.GetComponent<PlayerMovement>().get_js_removed_state()) {
                    /*se passou todas essas condiçoes pode sair*/

                    SceneManager.LoadScene("win");

                }
            }
        }
    }


}
