using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDirt : MonoBehaviour
{
    //�� ��ũ��Ʈ�� �� �����տ� ������� ��
    public GameObject dirt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject dirtInstance = GameObject.Instantiate(dirt, transform);
    }
}
