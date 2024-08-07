using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CreateDirt : MonoBehaviour
{
    //이 스크립트는 섬 프리팹에 집어넣을 것
    public GameObject dirt;
    public int hp = 1;

    public TurnManager turnManager;

    // Start is called before the first frame update
    void Start()
    {
        hp = 1;
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        if (turnManager != null) turnManager.WeightStart += OnWeightStart;
    }

    private void OnDisable()
    {
        if (turnManager != null) turnManager.WeightStart -= OnWeightStart;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnWeightStart(int weight)
    {
        Invoke("DeathCheck", 0.1f);
    }

    private void OnDestroy()
    {
        GameObject dirtInstance = GameObject.Instantiate(dirt, transform.position, Quaternion.identity);
    }

    public virtual void DeathCheck()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
