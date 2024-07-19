using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscript : MonoBehaviour
{
    public Rigidbody2D rigidbooty;
    public float projectileSpeed;
    public playerscript playerscript;

    // Start is called before the first frame update
    void Start()
    {
        rigidbooty = GetComponent<Rigidbody2D>();
        rigidbooty.velocity = Vector2.up * projectileSpeed;

        playerscript = GameObject.FindGameObjectWithTag("player").GetComponent<playerscript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.CompareTag("shield"))
        {
            //add to shieldbreak
        }*/

        Destroy(gameObject);
        --playerscript.bulletsOnScreen;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("boulder"))
        {
            Destroy(gameObject);
            --playerscript.bulletsOnScreen;
        }
    }
}
