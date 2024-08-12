using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPShow : MonoBehaviour
{
    public int currentHP = 3;
    public Sprite[] HPSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHP > 0) GetComponent<SpriteRenderer>().sprite = HPSlot[currentHP - 1];
    }
}
