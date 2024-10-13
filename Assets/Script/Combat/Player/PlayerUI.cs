using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    public Slider healBar { get; set; }
    public Slider easeHealBar { get; set; }

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;
        }
        else { Destroy(instance);}
    }
    private void Start()
    {
        healBar = GameObject.Find("HealBarUI").GetComponent<Slider>();
        easeHealBar = GameObject.Find("EaseHealBarUI").GetComponent<Slider>();
    }
}
