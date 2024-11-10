using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDataEffect 
{
    [Header("Player Effect Sword")]
    [SerializeField] private GameObject Slash1;
    [SerializeField] private GameObject Thursh1;
    [SerializeField] private GameObject Slash2;
    [SerializeField] private GameObject AbilitySword1;
    public GameObject G_Slash1 { get; private set; }
    public GameObject G_Thursh1 { get; private set; }
    public GameObject G_Slash2 { get; private set; }
    public GameObject G_AbilityQ { get; private set; }

    public void InitilizedVfx() 
    {
        G_Slash1 = Slash1;
        G_Thursh1= Thursh1;
        G_Slash2 = Slash2;
        G_AbilityQ = AbilitySword1;
    }
}
