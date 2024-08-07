using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWindMove : MonoBehaviour
{
    public float weight;
    public float repeat;
    public LayerMask mask;
    public Vector2 windPos;

    public Transform upPos;
    public Transform downPos;
    public Transform leftPos;
    public Transform rightPos;

    private ShipBase ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<ShipBase>();
        weight = ship.weight;

        switch (weight)
        {
            case 1:
                repeat = 4;
                break;
            case 2:
                repeat = 3;
                break;
            case 3:
                repeat = 2;
                break;
            case 4:
                repeat = 1;
                break;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isWind)
        {
            windPos = transform.position;
            for (int i = 0; i < repeat; i++)
            {
                ship.RepositionUI();
            }

        }
    }


    public void Move()
    {
        for(int i = 0; i < repeat; i++)
        {
            if (GameManager.instance.windDIrection.x > 0)
            {
                RaycastHit2D ray = Physics2D.Raycast(rightPos.position, Vector2.right, 0.5f, mask);
                if (ray.collider == null)
                {
                    transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                }
            }
            else if (GameManager.instance.windDIrection.x < 0)
            {
                RaycastHit2D ray = Physics2D.Raycast(leftPos.position, Vector2.left, 0.5f, mask);
                if (ray.collider == null)
                {
                    transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                }
            }
            else if (GameManager.instance.windDIrection.y > 0)
            {
                RaycastHit2D ray = Physics2D.Raycast(upPos.position, Vector2.up, 0.5f, mask);
                if (ray.collider == null)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                }
            }
            else if (GameManager.instance.windDIrection.y < 0)
            {
                RaycastHit2D ray = Physics2D.Raycast(downPos.position, Vector2.down, 0.5f, mask);
                if (ray.collider == null)
                {
                    transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                }
            }

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Reef"))
        {
            Destroy(gameObject);

        }
    }


}
