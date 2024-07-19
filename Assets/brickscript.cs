using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickscript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int bricktype;
    public GameObject boulder;
    public GameObject extrashell;
    public logicscript logicscript;

    public int editorbricktype; //so you dont need to remember the numbers when creating stages

    public int bouldertype;

    public int normalBrickScoreValue = 20;
    public int boulderBrickScoreValue = 10;
    public int extrashellBrickScoreValue = 1;
    public float pitchChoose;

    

    public float BreakingAnimationTimer;

    public bool isRandomBrick;

    public boulderscript boulderscript;

    public AudioSource AudioSource;

 
    public BoxCollider2D BoxCollider2D;





    // Start is called before the first frame update
    void Start()
    {
        logicscript = GameObject.FindGameObjectWithTag("logicobject").GetComponent<logicscript>();
        

        spriteRenderer = GetComponent<SpriteRenderer>();
        BoxCollider2D = GetComponent<BoxCollider2D>();

        if (isRandomBrick == true)
        {
            bricktype = Random.Range(1, 100);//random chance to become brick type 1-100
            bouldertype = Random.Range(1, 4);
        }

        else if (isRandomBrick == false) 
        {
        switch (editorbricktype)
            {
                default:
                    {
                        bricktype = 65;
                        break;
                    }
                case 2:
                    {
                        bricktype = 95;
                        break; 
                    }
                case 3:
                    {
                        bricktype = 100;
                        break;
                    }
            }
        }

        ++logicscript.bricksLeft;

        switch (bricktype)
        {
            case <= 65:
                {
                    //keep color the same
                    break;
                }

            case <= 95 and > 65:
                {
                    switch (bouldertype)
                    {
                        case 1: 
                            {
                                spriteRenderer.color = Color.yellow;
                                break; 
                            }
                        case 2:
                            {
                                spriteRenderer.color = new Color(1f, 0.5f, 0f);
                                break;
                            }
                        default:
                            {
                                spriteRenderer.color = Color.red;//make red
                                break;
                            }
                    }

                    
                    break;
                }

            case <= 100 and > 95:
                {
                    spriteRenderer.color = Color.blue;//make blue
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        


       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("shell"))
        {
            pitchChoose = Random.Range(0.50f,1.50f);
            AudioSource.pitch = pitchChoose;
            switch (bricktype)
            {
                case <= 65:
                    {
                        
                        AudioSource.Play();
                        BoxCollider2D.enabled = false;//disable collider
                        --logicscript.bricksLeft;

                        makeTransparent(); //temporary; remove and replace once we get a breaking animation.

                        
                        
                        logicscript.score += normalBrickScoreValue * logicscript.scoreMultiplier;
                        logicscript.stageScore += normalBrickScoreValue * logicscript.scoreMultiplier;
                        StartCoroutine(breakBrick());
                        break;
                    }

                case <= 95 and > 65:
                    {
                        AudioSource.Play();
                        BoxCollider2D.enabled = false;
                        

                        makeTransparent(); //temporary; remove once we get a breaking animation.

                        GameObject newBoulder = Instantiate(boulder, transform.position, transform.rotation);
                        boulderscript = newBoulder.GetComponent<boulderscript>();//assign the just spawed boulderscript

                        boulderscript.linkbouldertype(bouldertype); //send bouldertype to boulder

                        //deducting from bricksleft will be done by the boulderscript so that the game only ends when all boulders are gone
                        logicscript.score += boulderBrickScoreValue * logicscript.scoreMultiplier;
                        logicscript.stageScore += boulderBrickScoreValue * logicscript.scoreMultiplier;
                        StartCoroutine(breakBrick());
                        break;
                    }

                case <= 100 and > 95:
                    {
                        AudioSource.Play();
                        BoxCollider2D.enabled = false;
                        --logicscript.bricksLeft;

                        makeTransparent(); //temporary; remove once we get a breaking animation.

                        Instantiate(extrashell, transform.position, transform.rotation);//spawn powerup
                        
                        //--logicscript.bricksLeft;
                        logicscript.score += extrashellBrickScoreValue * logicscript.scoreMultiplier; //score is only 1 because of scoring potential of multiple shells
                        logicscript.stageScore += extrashellBrickScoreValue * logicscript.scoreMultiplier;
                        StartCoroutine(breakBrick());
                        break;
                    }
            }
        }

    }

  

    IEnumerator breakBrick() //delay that gives time for sound effects and animations before destroying the gameobject
    {
        yield return new WaitForSeconds(BreakingAnimationTimer);
        Destroy(gameObject);
    }

    public void makeTransparent()
    {
        Color currentColor = spriteRenderer.color;
        currentColor.a = 0f; // Change this value to adjust transparency
        spriteRenderer.color = currentColor;
    }
}


