using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boulderscript : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float fallSpeed;
    public int boulderhealth;
    public SpriteRenderer spriteRenderer;
    public logicscript logicscript;
    public int boulderHitScore = 10;
    public float BreakingAnimationTimer;
    public BoxCollider2D BoxCollider2D;

    public bool breakPreperationsHasPlayed;

    public persistentscript persistentscript;

    public brickscript brickscript;

    public AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        logicscript = GameObject.FindGameObjectWithTag("logicobject").GetComponent<logicscript>();
        persistentscript = GameObject.FindGameObjectWithTag("persistence").GetComponent<persistentscript>();
        brickscript = GameObject.FindGameObjectWithTag("brick").GetComponent<brickscript>();


        BoxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component of the object


        /* if (persistentscript.gameType == 2)
         {
             boulderhealth = Random.Range(1, 4);//random value choosing boulderhealth (setting max to 3 makes it that red boulders dont spawn)
         }
         else 
         { */

        //}

        
        rb2D.velocity = Vector2.down * fallSpeed; //apply velocity
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (boulderhealth)
        {
            case 1:
                {
                    spriteRenderer.color = Color.yellow; // Change color to yellow
                    fallSpeed = 1.7f;
                    break;
                }

            case 2:
                {
                    spriteRenderer.color = new Color(1f, 0.5f, 0f); // Change color to orange
                    fallSpeed = 1.4f;
                    break;
                }

            default:
                {
                    spriteRenderer.color = Color.red; // Change color to red
                    fallSpeed = 1f;
                    break;
                }
        }

        if (boulderhealth <= 0 && breakPreperationsHasPlayed == false) 
        {

            breakPreperationsHasPlayed = true;

            

            Debug.Log("bloulder dying running");

            breakPreperations();
            StartCoroutine(breakBoulder());





        }
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            hitBoulder();
            logicscript.score += boulderHitScore * logicscript.scoreMultiplier;
            logicscript.stageScore += boulderHitScore * logicscript.scoreMultiplier;
        }

        if (collision.gameObject.CompareTag("deadzone"))
        {
            logicscript.campDamage();
            Destroy(gameObject);
            --logicscript.bricksLeft;
        }

        if (collision.gameObject.CompareTag("playerturret"))
        {
            logicscript.gameover();
        }
    }

    public void linkbouldertype(int boulderTypeMessege)
    {
        boulderhealth = boulderTypeMessege;
    }

    public void hitBoulder()
    {
        --boulderhealth;

        rb2D.velocity = Vector2.zero;//reset velocity
        rb2D.velocity = Vector2.down * fallSpeed; //apply velocity
    }


    IEnumerator breakBoulder() //delay that gives time for sound effects and animations before destroying the gameobject
    {
        
        yield return new WaitForSeconds(BreakingAnimationTimer);
       
        Destroy(gameObject);
    }

    public void breakPreperations()
    {
        audioSource.Play(); // it couldnt play because it was in update, so its plays and replays itself every frame. nvm, it still dosent play. idk why.
        BoxCollider2D.enabled = false;

        spriteRenderer.enabled = false;

        --logicscript.bricksLeft; //it messes things up if placed in update() or above waitforseconds

        
    }


}
