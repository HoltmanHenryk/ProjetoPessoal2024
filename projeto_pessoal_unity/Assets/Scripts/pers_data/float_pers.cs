using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class float_pers : ScriptableObject
{
    [SerializeField]
    private float _value;

    public float Value {
        get {return _value;}
        set {_value = value;}
    }
}
