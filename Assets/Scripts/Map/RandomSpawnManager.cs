using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnManager : MonoBehaviour
{
    public List<GameObject> lands;
    private List<Vector2> spawnedPos;
    public int spawnCount;

    void Start()
    {
        spawnedPos = new List<Vector2>();
        for (int i = 0; i < spawnCount; i++)
        {

            if(spawnedPos.Count != 0)
            {
                Instantiate(lands[Random.Range(0, lands.Count)], new Vector2(Random.Range(-4, 4) + 0.5f, Random.Range(-4, 4) + 0.5f), Quaternion.Euler(0, 0, 0));
            }
            else
            {
                Instantiate(lands[Random.Range(0, lands.Count)], new Vector2(Random.Range(-4, 4) + 0.5f, Random.Range(-4, 4) + 0.5f), Quaternion.Euler(0, 0, 0));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
