using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Ship"))
        {
            Debug.Log("ÆÄ±«µÊ");
            GetComponent<CreateDirt>().hp--;
        }

        if (GetComponent<CreateDirt>().hp == 0)
        {
            Destroy(gameObject);
        }
        

    }
}
