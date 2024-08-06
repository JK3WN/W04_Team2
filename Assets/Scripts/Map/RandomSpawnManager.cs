using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnManager : MonoBehaviour
{
    public List<GameObject> lands;
    public List<Vector2> spawnedPos;

    public GameObject[] spawnedFirst;

    private bool canSpawn;
    private int spawnedCount;
    public int MaxspawnCount;


    void Start()
    {
        spawnedCount = 0;
        
        spawnedPos = new List<Vector2>();

        spawnedFirst = GameObject.FindGameObjectsWithTag("Land");

        for (int i = 0; i < spawnedFirst.Length; i++)
        {
            spawnedPos.Add(spawnedFirst[i].transform.position);
        }

        while(spawnedCount < MaxspawnCount)
        {
            Vector2 SpawnPosVector = new Vector2 (Random.Range(-4, 4) + 0.5f, Random.Range(-4, 4) + 0.5f);
            if(spawnedPos.Count != 0)
            {
                canSpawn = true;
                for (int j = 0; j < spawnedPos.Count; j++)
                {
                    if (Vector2.Distance(spawnedPos[j], SpawnPosVector) < 1)
                    {
                        canSpawn = false;
                    }
                }
                if (canSpawn)
                {
                    Instantiate(lands[Random.Range(0, lands.Count)], SpawnPosVector, Quaternion.Euler(0, 0, 0));
                    spawnedCount++;
                    spawnedPos.Add(SpawnPosVector);
                }
            }
            else
            {
                Instantiate(lands[Random.Range(0, lands.Count)], SpawnPosVector, Quaternion.Euler(0, 0, 0));
                spawnedCount++;
                spawnedPos.Add(SpawnPosVector);
            }
        }
    }

}
