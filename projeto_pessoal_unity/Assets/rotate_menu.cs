using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class rotate_menu : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * (speed * Time.deltaTime));
    }

    public void set_speed_multiplier(float speed_mult) {
        speed *= speed_mult;
    }

    public void set_speed_value(float speed_value){
        speed = speed_value;
    }
}
