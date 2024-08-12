using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APShow : MonoBehaviour
{
    public Sprite[] APSlot;
    public int currentAP = 2;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = APSlot[currentAP];
    }
}
