using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatEventSubscriber : MonoBehaviour
{
    public TurnManager turnManager;

    public int Weight = 1;

    // Start is called before the first frame update
    void Start()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        if (turnManager != null) turnManager.WeightStart += OnWeightStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        if (turnManager != null)  turnManager.WeightStart -= OnWeightStart;
    }


    private void OnWeightStart(int CallWeight)
    {
        if (Weight != CallWeight) return;
        // YJK, 자기 weight와 동일한 event가 들어올 때만 진행
        Debug.Log(this);
    }
}
