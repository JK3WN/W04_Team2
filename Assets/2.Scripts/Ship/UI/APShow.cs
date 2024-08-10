using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APShow : MonoBehaviour
{
    public GameObject[] APSlot;
    public int currentAP = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < currentAP && i < APSlot.Length; i++)
        {
            APSlot[i].SetActive(true);
        }
        for(int i=currentAP; i< APSlot.Length; i++)
        {
            APSlot[i].SetActive(false);
        }
    }
}
