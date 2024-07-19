using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chargeindicatorscript : MonoBehaviour
{
    public playerscript playerscript;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerscript.cannonChargeValue >= playerscript.cannonChargeLimit)
        {
            Color currentColor = spriteRenderer.color;
            currentColor.a = 1f; // Change this value to set full opacity
            spriteRenderer.color = currentColor;
        }

        else
        {
            // Set the alpha (transparency) of the sprite to a lower value
            Color currentColor = spriteRenderer.color;
            currentColor.a = 0f; // Change this value to adjust transparency
            spriteRenderer.color = currentColor;
        }
    }
}
