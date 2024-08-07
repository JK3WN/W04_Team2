using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int ActionPoints;

    [Header("Sea - Level")]
    public GameObject[] lands;
    public GameObject[] reefs;
    public List<GameObject> landsList;
    public int seaLevel;

    [Header("Sea - Wind")]
    public bool isWind;
    public Vector2 windDIrection;
    public GameObject[] ships;
    public List<Vector3> shipsTransform;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        FindLands();
        FindShips();
        
    }

    void Update()
    {
        isWind = false;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(seaLevel > 1)
            {
                seaLevel--;
                ActionPoints--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (seaLevel < 3)
            {
                seaLevel++;
                ActionPoints--;
            }
        }

        SeaLevel();
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            ActionPoints--;
            WindDirection();
            WindMove();
        }
        ToFirstPosition();

    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void FindLands()
    {
        lands = GameObject.FindGameObjectsWithTag("Land");
        reefs = GameObject.FindGameObjectsWithTag("Reef");

        for (int i = 0; i < lands.Length; i++)
        {
            landsList.Add(lands[i]);
        }
        for (int i = 0; i < reefs.Length; i++)
        {
            landsList.Add(reefs[i]);
        }
    }
    void SeaLevel()
    {
        if (seaLevel == 1)
        {
            for (int i = 0; i < landsList.Count; i++)
            {
                switch (landsList[i].GetComponent<Lands>().landType)
                {
                    case 1:
                        if (!landsList[i].GetComponent<SpriteRenderer>().enabled)
                        {
                            landsList[i].GetComponent<SpriteRenderer>().enabled = !landsList[i].GetComponent<SpriteRenderer>().enabled;
                            landsList[i].GetComponent<BoxCollider2D>().enabled = !landsList[i].GetComponent<BoxCollider2D>().enabled;
                        }
                        break;
                    case 2:
                        if (!landsList[i].GetComponent<SpriteRenderer>().enabled)
                        {
                            landsList[i].GetComponent<SpriteRenderer>().enabled = !landsList[i].GetComponent<SpriteRenderer>().enabled;
                            landsList[i].GetComponent<BoxCollider2D>().enabled = !landsList[i].GetComponent<BoxCollider2D>().enabled;
                        }
                        break;
                }
            }
        }
        else if (seaLevel == 2)
        {
            for (int i = 0; i < landsList.Count; i++)
            {
                switch (landsList[i].GetComponent<Lands>().landType)
                {
                    case 1:
                        if (landsList[i].GetComponent<SpriteRenderer>().enabled)
                        {
                            landsList[i].GetComponent<SpriteRenderer>().enabled = !landsList[i].GetComponent<SpriteRenderer>().enabled;
                            landsList[i].GetComponent<BoxCollider2D>().enabled = !landsList[i].GetComponent<BoxCollider2D>().enabled;
                        }
                        break;
                    case 2:
                        if (!landsList[i].GetComponent<SpriteRenderer>().enabled)
                        {
                            landsList[i].GetComponent<SpriteRenderer>().enabled = !landsList[i].GetComponent<SpriteRenderer>().enabled;
                            landsList[i].GetComponent<BoxCollider2D>().enabled = !landsList[i].GetComponent<BoxCollider2D>().enabled;
                        }
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < landsList.Count; i++)
            {
                switch (landsList[i].GetComponent<Lands>().landType)
                {
                    case 1:
                        if (landsList[i].GetComponent<SpriteRenderer>().enabled)
                        {
                            landsList[i].GetComponent<SpriteRenderer>().enabled = !landsList[i].GetComponent<SpriteRenderer>().enabled;
                            landsList[i].GetComponent<BoxCollider2D>().enabled = !landsList[i].GetComponent<BoxCollider2D>().enabled;
                        }
                        break;
                    case 2:
                        if (landsList[i].GetComponent<SpriteRenderer>().enabled)
                        {
                            landsList[i].GetComponent<SpriteRenderer>().enabled = !landsList[i].GetComponent<SpriteRenderer>().enabled;
                            landsList[i].GetComponent<BoxCollider2D>().enabled = !landsList[i].GetComponent<BoxCollider2D>().enabled;
                        }
                        break;
                }
            }
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------

    void FindShips()
    {
        ships = GameObject.FindGameObjectsWithTag("Ship");
        for (int i = 0; i < ships.Length; i++)
        {
            shipsTransform.Add(ships[i].transform.position);
        }
    }
    void WindDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
            
        windDIrection = new Vector2(horizontal, vertical);
    }
    void WindMove()
    {
        for (int i = 0; i < ships.Length; i++)
        {
            ships[i].GetComponent<ShipWindMove>().Move();
        }
    }
    void ToFirstPosition()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < ships.Length; i++)
            {
                ships[i].transform.position = shipsTransform[i];
            }

        }
    }
}
