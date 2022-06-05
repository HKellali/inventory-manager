using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{

    public static ItemList Instance { get; private set;}
    public Sprite[] sprites;
    
    void Awake()
    {
        Instance = this;
    }

    
}
