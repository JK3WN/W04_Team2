using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer spriteRenderer;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];

        InvokeRepeating("ChangeSprite", 0f, 0.2f);
    }

    private void ChangeSprite()
    {
        if (index >= 3) Destroy(gameObject);
        else
        {
            spriteRenderer.sprite = sprites[index];
            index++;
        }
    }
}
