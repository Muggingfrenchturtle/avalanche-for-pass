using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using static UnityEditor.PlayerSettings;

public class logicscript : MonoBehaviour
{
    
    public float score;
    public float scoreMultiplier;
    public float stageScore;
    public float shellsFired;

    public float bricksLeft;
    public float shellsOnScreen;
    public bool stageStarted; //so you wont gameover at the start of stages when there is no shells on screen
    public float bricksetSelect;
    public float campHealth;
    public int stageNumber = 0;
    public bool stageCompleteFunctionIsRunning;
    public bool debugMode;

    public Text stageText;
    public Text scoreText;
    public Text multiplierText;
    public Text gameoverText;
    public Text gameoverTextInstruction;
    public Text moreInstruction;

    public Text stagescoretext;
    public Text shellsfiredtext;
    public Text stagetotaltext;
    
    public GameObject DebugStuff;
    public Text bricksleftDebug;

    public GameObject stageCompleteUi;

    public GameObject brickset1;
    public GameObject brickset2;
    public GameObject brickset3;
    public GameObject brickset4;
    public GameObject brickset5;

    public GameObject randbrickset1;
    public GameObject randbrickset2;
    public GameObject randbrickset3;
    public GameObject randbrickset4;
    public GameObject randbrickset5;

    public GameObject tankClimbingSprite;

    public Tankclimbingspritescript tankClimbingSpriteRuntime;

    public persistentscript persistentscript;

    //public persistentscript editorPersistent;

    public playerscript playerscript;

    public AudioSource campdeath;

    public SpriteRenderer playerspriterenderer;


    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

        playerscript = GameObject.FindGameObjectWithTag("player").GetComponent<playerscript>();
        persistentscript = GameObject.FindGameObjectWithTag("persistence").GetComponent<persistentscript>();
        //editorPersistent = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<persistentscript>();

        stageInitiate();
    }

    // Update is called once per frame
    void Update()
    {
        if (bricksLeft <= 0 && stageCompleteFunctionIsRunning == false && score > 0 /* << prevents this from runnin when game is resetted by pressing 1*/)
        {
            //bricksleft + 1 because if something bad might happen??

            Debug.Log("stage complete");//stagecomplete
            stageComplete();
            stageCompleteFunctionIsRunning = true;
        }

        if (playerscript.hasShell == false && stageStarted == true || campHealth <=0) 
        {
            gameover();//gameover
        }

        

        if (stageStarted == false && shellsOnScreen > 0) 
        {
            stageStarted = true;
        }

        switch (shellsOnScreen)
        {
            case 0:
                {
                    scoreMultiplier = 0;
                    break;
                }

            case 1:
                {
                    scoreMultiplier = 1.0f;
                    break;
                }
            case 2:
                {
                    scoreMultiplier = 3.0f;
                    break;
                }
            case 3:
                {
                    scoreMultiplier = 5.0f;
                    break;
                }
            case 4:
                {
                    scoreMultiplier = 7.0f;
                    break;
                }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            restartgame();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            endgame();
        }


        scoreText.text = score.ToString();
        multiplierText.text = scoreMultiplier.ToString();
        stageText.text = stageNumber.ToString();

        bricksleftDebug.text = bricksLeft.ToString();


        if (tankClimbingSpriteRuntime.transform.position.y >= 5.5 /* north wall location*/) //ignore errors from here
        {
            Debug.Log("reached ceiling");
            stageCompleteFunctionIsRunning = false;
            stageInitiate();

        }//from stagecomplete()


        //code below the if statement above wont run

        
       

    }

    public void normalStageStart()
    {
        ++stageNumber;
        switch (stageNumber)
        {
            case 1: {
                    Instantiate(brickset1, transform.position, transform.rotation);
                    break;
                }
            case 2:
                {
                    Instantiate(brickset2, transform.position, transform.rotation);
                    break;
                }
            case 3:
                {
                    Instantiate(brickset3, transform.position, transform.rotation);
                    break;
                }
            case 4:
                {
                    Instantiate(brickset4, transform.position, transform.rotation);
                    break;
                }
            case 5:
                {
                    Instantiate(brickset5, transform.position, transform.rotation);
                    break;
                }
        }

        stagestartphase2();
    }
    
    public void randomStageStart()
    {
        
        bricksetSelect = Random.Range(1, 5);
        ++stageNumber;

        switch (bricksetSelect)
        {
            case 1:
                {
                    Instantiate(randbrickset1, transform.position, transform.rotation);
                    break;
                }
            case 2:
                {
                    Instantiate(randbrickset2, transform.position, transform.rotation);
                    break;
                }
            case 3:
                {
                    Instantiate(randbrickset3, transform.position, transform.rotation);
                    break;
                }
            case 4:
                {
                    Instantiate(randbrickset4, transform.position, transform.rotation);
                    break;
                }
            case 5:
                {
                    Instantiate(randbrickset5, transform.position, transform.rotation);
                    break;
                }
        }

        stagestartphase2();
        


    }//stagestart function, instantiate brickset, stagestarted will become true once the player shoots the first shell.


    public void stagestartphase2()
    {
        stageCompleteUi.transform.position = new Vector3(2019, 1484); //make stagecomplete invisible

        tankClimbingSpriteRuntime.transform.position = new Vector3(playerscript.transform.position.x, -6.59f);//teleport climbing tank sprite to bottom of screen
        //when reaching the specified y position, start slowly following the still invisible player maybe call functions in the climber script
        //after delay, destroy climbing sprite and
        tankClimbingSpriteRuntime.enteringstage = true;
        //make playertank visible again (done in tankclimbingspritescript)
    }



    public void stageInitiate() //chosses stage based on game type
    {
        if (persistentscript.gameType == 1 ) //editorpersistent dosent work, making testing difficult
        {
            normalStageStart();
        }
        else if (persistentscript.gameType == 2 )
        {
            randomStageStart();
        }

        
    }



    public void stageComplete()
    {
        playerscript.ShellAmount += shellsOnScreen;
        GameObject[] gos = GameObject.FindGameObjectsWithTag("shell");
        foreach (GameObject go in gos)
            Destroy(go);
            shellsOnScreen = 0;
            

        GameObject[] gosa = GameObject.FindGameObjectsWithTag("bullet");
        foreach (GameObject goa in gosa)
            Destroy(goa);
        playerscript.bulletsOnScreen = 0;

        StartCoroutine(waitanddestroybrickset());



        
        stageStarted = false;


        stagescoretext.text = stageScore.ToString();

        shellsFired /= 2; // 1 shell = .5 multiplier. playing with +3 shells at once is rewarded. playing safe with 1 shell is punished.

        shellsfiredtext.text = shellsFired.ToString();

        score -= stageScore;

        stageScore *= shellsFired;

        stagetotaltext.text = stageScore.ToString();

        score += stageScore;

        stageCompleteUi.transform.position = new Vector3 (683,383); //make stagecomplete visible

       
        stageScore = 0;
        shellsFired = 0; //reset values for next stage


        //display stage complete ui



        playerscript.turnInvisible();//make player invisible and prevent shooting and shield opening
        playerscript.playerCanShoot = false;
        Instantiate(tankClimbingSprite, playerscript.transform.position, playerscript.transform.rotation);//instantiate climbingtanksprite
        
        tankClimbingSpriteRuntime = GameObject.FindGameObjectWithTag("climbingsprite").GetComponent<Tankclimbingspritescript>();
        
        
        //after a delay (until sprite reaches top of screen)... (done in update())

        


    }//stagecomplete function, delete all shells and add them back to reserves. delete brickset. play animations. stagestarted = false

    public void gameover()
    {

        Destroy(GameObject.FindGameObjectWithTag("brickset"));
        Destroy(GameObject.FindGameObjectWithTag("player"));

        //playerscript.MoveSpeed -= shellsOnScreen * 2;

        GameObject[] gos = GameObject.FindGameObjectsWithTag("shell");
        foreach (GameObject go in gos)
            Destroy(go);


        GameObject[] gosa = GameObject.FindGameObjectsWithTag("bullet");
        foreach (GameObject goa in gosa)
            Destroy(goa);

        Color currentColor = gameoverText.color;
        currentColor.a = 1f; 
        gameoverText.color = currentColor;
        
        Color currentColory = gameoverTextInstruction.color;
        currentColory.a = 1f;
        gameoverTextInstruction.color = currentColory;

        Color currentColorya = moreInstruction.color;
        currentColorya.a = 1f;
        moreInstruction.color = currentColorya;

        stageCompleteUi.transform.position = new Vector3(2019, 1484); //make stagecomplete invisible, otherwise it will show up when you game over

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            restartgame();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            endgame();
        }


        //delete brickset, shells, bullets, and player

        //plan to make leaderboards and title screens and stuff

    }//gameover function

    IEnumerator waitanddestroybrickset()
    {
        yield return new WaitForSeconds(3);
        Destroy(GameObject.FindGameObjectWithTag("brickset"));
        bricksLeft = 0; //ensure that future stages dont prematurely get deducted brickamounts due to the brickdestroy delay(?)
        //so that sound effects of bricks smashing plays as the stage ends.
    }

    public void restartgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void endgame()
    {
        SceneManager.LoadScene("Main menu");
    }

    public void campDamage()
    {
        --campHealth;
        campdeath.Play();
    }

    
}
