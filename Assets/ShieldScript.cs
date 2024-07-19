using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public bool ShieldClosed;
    public bool shieldBroken; //for displaying alternative sprite
    private PolygonCollider2D polygonCollider2D;

    public playerscript playerscript;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer detractedSprite; //the sprite of the detracted shield, change to proper sprite structure asap.



    void Start()
    {
        polygonCollider2D = GetComponent<PolygonCollider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerscript.playerCanShoot == true)
        {
            // Toggle the ShieldClosed state
            ShieldClosed = !ShieldClosed;
            shieldBroken = false;
        }


        if (ShieldClosed == false)
        {
            polygonCollider2D.enabled = false;

            // Set the alpha (transparency) of the sprite to a lower value
            Color currentColor = spriteRenderer.color;
            currentColor.a = 0f; // Change this value to adjust transparency
            spriteRenderer.color = currentColor;

            detractedSprite.enabled = true;
        }
        else if (ShieldClosed == true)
        {
            polygonCollider2D.enabled = true;

            // Set the alpha (transparency) of the sprite to full opacity
            Color currentColor = spriteRenderer.color;
            currentColor.a = 1f; // Change this value to set full opacity
            spriteRenderer.color = currentColor;

            detractedSprite.enabled = false;
        }

        if (playerscript.playerCanShoot == false)
        {
            detractedSprite.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( ( collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("shell") ) && playerscript.shieldDamage >= playerscript.shieldDamageLimit)
        {
            ShieldClosed = false;
            shieldBroken = true;
            playerscript.shieldDamage = 0;
            //play breaking sound
        }
    }
}