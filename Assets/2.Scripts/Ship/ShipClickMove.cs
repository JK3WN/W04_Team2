using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipClickMove : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<Player>().intPosition != new Vector2(0,0))
        {
            transform.position = player.GetComponent<Player>().intPosition;
        }
    }
}
