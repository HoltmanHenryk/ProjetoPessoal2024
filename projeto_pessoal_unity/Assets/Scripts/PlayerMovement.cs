using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Movement")]
    public CharacterController controller;

    private Animator player_anim;

    public Transform groundCheck;

    public LayerMask groundMask;

    public float speed = 15;
    private float gravity = -9.81f;

    public float random_trolle = 0.0f;

    
    public float groundDistance = 0.4f;
    

    public Vector3 velocity;

    public bool isGrounded;

    public GameObject player_camera;

    public bool in_move_camera_area;
    private InputAction camera_move_action;
    private InputAction touch_postition_action;
    private PlayerInput player_input;
    public bool is_android;
    private Vector2 camera_delta;
    private Vector2 touch_position;
    public RectTransform move_camera_area;

    public float mouseSens = 100.0f;
    public float xRotation = 0.0f;


    


    public Vector2 mouse_pos;
   
    public float horizontal;
    public float vertical;

    public float sanity_tickrate = 2.0f;

    [Header("----------------")]
    [Header("HUD")]
    public Text sanity_text;

    public GameObject enable_touch;

    public Text fps;

    [Header("----------------")]
    [Header("Objectives")]

    public bool shoud_tick_sanity = true;
    
    public int sanity = 100;

    

    public bool tem_dinheiro = false;

    public bool money_destroyed = false;

    public bool js_removed = false;

    public bool if_removed = false;

    public bool can_exit = false;

    
    

    

     private FMOD.Studio.EventInstance fs;


    private void Awake(){
        player_input = GetComponent<PlayerInput>();
        camera_move_action = player_input.actions["CameraMove"];
        touch_postition_action = player_input.actions["TouchPosition"];
    }
    
    // Start is called before the first frame update
    void Start()
    {
        

        if(Application.platform == RuntimePlatform.Android) {
            enable_touch.SetActive(true);
            is_android = true;
            in_move_camera_area = false;
        } else{
            Cursor.lockState = CursorLockMode.Locked;
            in_move_camera_area = true;
        }

        
        
        if(shoud_tick_sanity){
            Invoke("tick_sanity", 2.0f);
        }

        player_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    
    
    
    void Update()
    {
        // estimativa do fps
        fps.text = (Mathf.RoundToInt(1/ Time.smoothDeltaTime)).ToString();

        
        random_trolle = Random.Range(-1.5f, 1.5f);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2;
        }
        
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("Vertical");

        player_anim.SetFloat("vertical", vertical);
        player_anim.SetFloat("horizontal", horizontal);
        //player_anim.SetBool("hand_swing", true);

        Vector3 move = transform.right * horizontal + transform.forward * vertical ;

        if(sanity < 50) {


            move += Vector3.back * random_trolle;

            if(sanity < 40) {
                move += Vector3.right * random_trolle * 1.5f;
            }

            if(sanity < 30) {
                move *= -1;
            }

            if(sanity < -100){
                
            }
        }


        controller.Move(move * speed * Time.deltaTime);

        /*
        if(Input.GetButton("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }
        */

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.F)) {
            player_anim.SetBool("bool_swing", true);
            //player_anim.SetBool("bool_swing", false);
            player_anim.SetTrigger("hand_swing");

        }


//camera

      
        if(is_android){
            touch_position = touch_postition_action.ReadValue<Vector2>();
            //in_move_camera_area = RectTransformUtility.RectangleContainsScreenPoint(move_camera_area, touch_position); // true if touch is inside rect

            if(RectTransformUtility.RectangleContainsScreenPoint(move_camera_area, touch_position)){
                in_move_camera_area = true;
            }
            else{
                in_move_camera_area = false;
            }
        }
       

        if(in_move_camera_area){
            camera_delta = camera_move_action.ReadValue<Vector2>();
            float position_x = camera_delta.x * mouseSens * Time.deltaTime;
            float position_y = camera_delta.y * mouseSens * Time.deltaTime;


            xRotation -= position_y;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
            player_camera.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);

            transform.Rotate(Vector3.up * position_x);
        }


    /*
        // camera movement

        float mouseX = mouse_pos.x * mouseSens;
        float mouseY = mouse_pos.y * mouseSens;

        if(is_touch) {
            //mouseX = Mathf.RoundToInt(mouseX);
            //mouseY = Mathf.RoundToInt(mouseY);
        }

        xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

            player_camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

           transform.Rotate(Vector3.up * mouseX);

           */



    }



    /*

    metodo bosta refazer essa merda depois

    OOP fudendo tudo compilação ep 47
    
    */

    public bool get_money_state() {return tem_dinheiro;}

    public void set_money_state(bool state) {tem_dinheiro = state;}

    public bool get_money_destroyed_state() {return money_destroyed;}

    public void set_money_destroyed_state(bool state) {money_destroyed = state;}

    public bool get_js_removed_state() {return js_removed;}

    public void set_js_removed_state(bool state) {js_removed = state;}

    public bool get_ifpr_removed_state() {return if_removed;}

    public void set_ifpr_removed_state(bool state) {if_removed = state;}

    public bool get_exit_condition() {return can_exit;}
    
    public void set_exit_condition(bool state) {can_exit = state;} 

    public int get_sanity() {return sanity;}

    public void modify_sanity(int ammount) {sanity += ammount;}

    public void tick_sanity() {
        sanity_tickrate -= 0.03f;
        sanity--;
        sanity_text.text = sanity.ToString();
        //sanity_text.color = new Color(233, 0, 0); //custom red
        FMODUnity.RuntimeManager.PlayOneShot("event:/tick_tock", transform.position);

        Invoke("tick_sanity", sanity_tickrate);
        }




/*morra unity*/

    private void OnMove(InputValue value){

        horizontal = value.Get<Vector2>().x;
        vertical = value.Get<Vector2>().y;

        /*if(horizontal != 0 || vertical != 0){
        //FMODUnity.RuntimeManager.PlayOneShot("event:/fs", GetComponent<Transform>().position);
        fs = FMODUnity.RuntimeManager.CreateInstance("event:/fs");


            if(!IsPlaying(fs)){
                fs.start();
            }
        }

        

        //fs.start();
        */

    }

    public void OnMousePos(InputValue value){
            mouse_pos = value.Get<Vector2>();
    }


private bool IsPlaying(FMOD.Studio.EventInstance instance) {
	FMOD.Studio.PLAYBACK_STATE state;   
	instance.getPlaybackState(out state);
	return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
}


}