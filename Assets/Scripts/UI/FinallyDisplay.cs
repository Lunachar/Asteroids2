using System;
using System.Collections;
using System.Collections.Generic;
using Asteroids2;
using UnityEngine;
using UnityEngine.UI;

public class FinallyDisplay : MonoBehaviour
{
    public static GameObject TextObject;
    public static string textField;
    //public static Text TextPropertie;
    void Start()
    {
        TextObject = gameObject;
        //TextPropertie = TextObject.GetComponent<Text>();
        textField = TextObject.GetComponent<Text>().text;
    
    }

}
