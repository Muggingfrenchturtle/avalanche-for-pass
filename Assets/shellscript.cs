using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellscript : MonoBehaviour
{
    private Rigidbody2D rb2D; // Declare a variable for the Rigidbody2D component
    private CircleCollider2D circleCollider2D;
    public float ProjectileSpeed;
    public float bounciness;
    //public float speedToDeduct;
    public logicscript logicscript;
    public playerscript playerscript;


    public AudioSource tap;


    // Start is called before the first frame update
    void Start()
    {
        logicscript = GameObject.FindGameObjectWithTag("logicobject").GetComponent<logicscript>();
        playerscript = GameObject.FindGameObjectWithTag("player").GetComponent<playerscript>();

        /*switch (logicscript.shellsOnScreen)
        {
            case 1: //first shell
                {
                    speedToDeduct = 0;
                    ProjectileSpeed -= speedToDeduct;
                    break;
                }
            case 2://second shell
                {
                    speedToDeduct = 1;
                    ProjectileSpeed -= speedToDeduct;
                    break;
                }
            case 3:
                {
                    speedToDeduct = 2;
                    ProjectileSpeed -= speedToDeduct;
                    break;
                }

            case 4:
                {
                    ProjectileSpeed -= 3;
                    break;
                }
        }// code for slower shells the more shells onscreen */


        rb2D = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component of the object
        rb2D.velocity = Vector2.up * ProjectileSpeed; // Add velocity going north (upwards)

        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.sharedMaterial = new PhysicsMaterial2D() { bounciness = bounciness };

        

    }

    // Update is called once per frame
    void Update()
    {
        // Keep rigidbody velocity constant by reassigning it
        rb2D.velocity = rb2D.velocity.normalized * ProjectileSpeed;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("deadzone"))
        {
            Destroy(gameObject);
            --logicscript.shellsOnScreen;

           
            
        }

        /*if (logicscript.shellsOnScreen <= 1)
        {
            ProjectileSpeed += speedToDeduct;

        }//it goes nuts in update() code for slower shells the more shells onscreen */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        tap.Play();
    }



}