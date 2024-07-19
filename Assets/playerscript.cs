using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Member;

public class playerscript : MonoBehaviour
{
    public float MoveSpeed;
    public float ShellAmount;
    public bool hasShell;

    public float shieldDamage;
    public float shieldDamageLimit;
    public float cannonChargeValue;
    public float cannonChargeLimit;
    public float offsetY = -4.1f;
    public float maxX = 0.486f;
    public float minX = -0.486f;

    public bool playerCanMove;
    public bool playerCanShoot; //prevents player from shooting when stage complete is running


    public float lastGunFired = 2;

    public float bulletsOnScreen;
    public float bulletLimit;

    public ShieldScript ShieldScript;
    public machinegunscript machinegunscript1;
    public machinegunscript machinegunscript2;
    public logicscript logicscript;
    public GameObject shell;

    public AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePositionScreen = Input.mousePosition;

        // Convert the mouse position to world coordinates
        Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePositionScreen.x, mousePositionScreen.y, transform.position.z));



        // Update the GameObject's position with the mouse's x-axis position and locked y-axis
        if (playerCanMove == true)
        {
            transform.position = new Vector3(mousePositionWorld.x, offsetY, transform.position.z);
        }


        if (mousePositionWorld.x < maxX && mousePositionWorld.x > minX)
        {
            playerCanMove = true;
        }
        else if ((mousePositionWorld.x > maxX || mousePositionWorld.x < minX))
        {
            playerCanMove = false;
        }//limit player's x movement

        if (transform.position.x > maxX) //when tank gets past the limit, reset its transform to expected place.
        {
            transform.position = new Vector2 (maxX, transform.position.y);
        }
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }//prevent tank from phasing through wall when moving the mouse fast


       


        if (Input.GetKeyDown(KeyCode.Space) && ShellAmount > 0 && playerCanShoot == true)
        {
            audioSources[0].Play(); 
        }


            if (Input.GetKey(KeyCode.Space) && ShellAmount > 0 && playerCanShoot == true)
        {
            Debug.Log("cannon code 1 running");
            cannonChargeValue += Time.deltaTime;


            

            //rest of the code is down below

            //fire main cannon


        }

        if (Input.GetKeyUp(KeyCode.Space) && ShellAmount > 0 && playerCanShoot == true) //the rest of the main cannon code. will not work in "if (Input.GetKey(KeyCode.K))"
        {
            Debug.Log("cannon code 2 running");

            audioSources[0].Stop();

            if (cannonChargeValue >= cannonChargeLimit)
            {
                ++logicscript.shellsOnScreen;
                ++logicscript.shellsFired;
                Instantiate(shell, transform.position, transform.rotation);
                
                

                --ShellAmount;
                cannonChargeValue = 0;

                if (ShellAmount <=0 )
                {
                    audioSources[2].Play();
                }
                else if (ShellAmount > 0)
                {
                    audioSources[1].Play();
                }

                if (ShieldScript.ShieldClosed == true)
                {
                    shieldDamage += shieldDamageLimit + 5;   // maxes shield damage instantly. the +5 is there to make things more sure
                }

            }

            else if (cannonChargeValue < cannonChargeLimit)
            {
                cannonChargeValue = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) /*&& ShellAmount <= 0*/ && bulletsOnScreen < bulletLimit && playerCanShoot == true)
        {
            Debug.Log("attempting bullet fire");

            
                if (lastGunFired == 2)
                {
                    machinegunscript1.fireBullet();
                    lastGunFired = 1;
                    //++bulletsOnScreen done on firebullet();
                }

                else if (lastGunFired == 1)
                {
                    machinegunscript2.fireBullet();
                    lastGunFired = 2;
                    //++bulletsOnScreen;
                }

            if (ShieldScript.ShieldClosed == true)
            {
                ++shieldDamage;
            }

        }



        if (Input.GetKey(KeyCode.F6) && logicscript.debugMode == true) //its just here so all the input stuff is in playerscript
        {

            //make debugstuff visible
         
        }
        if (Input.GetKey(KeyCode.F6) && logicscript.debugMode == false) //its just here so all the input stuff is in playerscript
        {


           //make debugstuff invisible


        }




        if (ShellAmount > 0 || GameObject.FindGameObjectWithTag("shell") == true || GameObject.FindGameObjectWithTag("extra shell") == true) 
        {
            hasShell = true;
        }
        else if (ShellAmount <= 0 || GameObject.FindGameObjectWithTag("shell") == false || GameObject.FindGameObjectWithTag("extra shell") == false)
        {
            hasShell = false;
        }

        if (ShellAmount > 3)
        {
            --ShellAmount;
        }

        
           
        if (bulletsOnScreen < 0)
        {
            bulletsOnScreen = 0; //ensure it dosent go to negatives
        }

        if (shieldDamage > 0)
        {
            shieldDamage -= 1 * Time.deltaTime;

            //StartCoroutine(shieldDamageDeduct());
        }

        if (shieldDamage < 0)
        {
            shieldDamage = 0;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.CompareTag("boulder"))
        {
            logicscript.gameover();//gameover
        }*/ //will be done by boulderscript for a smaller hitbox

        if (collision.gameObject.CompareTag("extra shell"))
        {
            audioSources[3].Play();
            ++ShellAmount;
        }



        



    }

  public void turnInvisible()
    {
        // Disable the renderer of the parent object
        Renderer parentRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (parentRenderer != null)
        {
            parentRenderer.enabled = false;
        }

        // Disable the renderers of all children
        Renderer[] childRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (Renderer renderer in childRenderers)
        {
            renderer.enabled = false;
        }
    }

    public void turnVisible()
    {
        // Disable the renderer of the parent object
        Renderer parentRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (parentRenderer != null)
        {
            parentRenderer.enabled = true;
        }

        // Disable the renderers of all children
        Renderer[] childRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (Renderer renderer in childRenderers)
        {
            renderer.enabled = true;
        }
    }

    /*IEnumerator shieldDamageDeduct() //delay that gives time for sound effects and animations before destroying the gameobject
    {
        

        // Wait for a delay (e.g., 5 seconds)
        yield return new WaitForSeconds(1.5f);

        // Start the countdown
        while (shieldDamage > 0)
        {
            
            shieldDamage -= 1 * Time.deltaTime;
        }

    }*/

}
