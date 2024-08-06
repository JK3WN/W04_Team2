using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Sea - Level")]
    public GameObject[] lands;
    public int seaLevel;

    [Header("Sea - Wind")]
    public bool isWind;
    public Vector2 windDIrection;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        lands = GameObject.FindGameObjectsWithTag("Land");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(seaLevel > 1)
            {
                seaLevel--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (seaLevel < 3)
            {
                seaLevel++;
            }
        }

        SeaLevel();
        WindDirection();
    }


    void SeaLevel()
    {
        if (seaLevel == 1)
        {
            for (int i = 0; i < lands.Length; i++)
            {
                switch (lands[i].GetComponent<Lands>().landType)
                {
                    case 1:
                        lands[i].SetActive(true);
                        break;
                    case 2:
                        lands[i].SetActive(true);
                        break;
                }
            }
        }
        else if (seaLevel == 2)
        {
            for (int i = 0; i < lands.Length; i++)
            {
                switch (lands[i].GetComponent<Lands>().landType)
                {
                    case 1:
                        lands[i].SetActive(false);
                        break;
                    case 2:
                        lands[i].SetActive(true);
                        break;
                }
            }
        }
        else
        {
            for (int i = 0; i < lands.Length; i++)
            {
                switch (lands[i].GetComponent<Lands>().landType)
                {
                    case 1:
                        lands[i].SetActive(false);
                        break;
                    case 2:
                        lands[i].SetActive(false);
                        break;
                }
            }
        }
    }

    void WindDirection()
    {
        if (isWind)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            windDIrection = new Vector2(horizontal, vertical);
        }
    }
}
