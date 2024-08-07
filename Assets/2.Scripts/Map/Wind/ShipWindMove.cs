using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipWindMove : MonoBehaviour
{
    public LayerMask mask;

    public Transform upPos;
    private bool canMove;

    private ShipBase ship;

    // Start is called before the first frame update
    void Start()
    {
        ship = GetComponent<ShipBase>();
    }

    public void Move()
    {
        canMove = true;

        // Use the ship's local downward direction
        Vector2 downDirection = transform.TransformDirection(Vector2.down);
        RaycastHit2D ray = Physics2D.Raycast(upPos.position, downDirection, 100f, mask);

        // Draw the ray in the scene view for debugging
        Debug.DrawLine(upPos.position, upPos.position + (Vector3)downDirection * 100f, Color.red, 2f);

        StartCoroutine(MoveToPos(ray));
    }

    IEnumerator MoveToPos(RaycastHit2D rays)
    {
        while (canMove)
        {
            yield return null;
            if (rays.collider != null)
            {
                // Draw the current position to the target position
                Debug.DrawLine(upPos.position, rays.point, Color.green);

                if (rays.collider.CompareTag("Dirt"))
                {
                    if (Vector2.Distance(transform.position, rays.collider.transform.position) < 0.05f)
                    {
                        canMove = false;
                    }
                }
                else if (!rays.collider.CompareTag("Dirt") && Vector2.Distance(upPos.position, rays.point) < 0.05f)
                {
                    canMove = false;
                }

                transform.Translate(Vector2.down * 10 * Time.deltaTime);
                ship.RepositionUI();
            }
        }
    }
}
