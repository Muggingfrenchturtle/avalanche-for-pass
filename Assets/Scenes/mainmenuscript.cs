using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenuscript : MonoBehaviour
{
    public GameObject mountain;
    public GameObject menu2Buttons;
    public GameObject titleLogo;

    public Text pressKeyInstruction;


    public Vector3 buttonTransformBasedOnMainmenuscriptTransform;

    public SpriteRenderer logoSpriteRenderer;

    public int menuPhase;
    public float mountainMovespeed;

    private Vector3 targetMountainPosition; // Target position for the mountain



    public persistentscript persistentscript;

    void Start()
    {
        logoSpriteRenderer = titleLogo.GetComponent<SpriteRenderer>();
        persistentscript = GameObject.FindGameObjectWithTag("persistence").GetComponent<persistentscript>();
        targetMountainPosition = mountain.transform.position; // Initialize the target position
        buttonTransformBasedOnMainmenuscriptTransform = menu2Buttons.transform.position;

        Cursor.visible = true;

    }

    void Update()
    {
        if (Input.anyKeyDown && menuPhase == 1)
        {
            targetMountainPosition = new Vector3(-12.5f, 5.13f, 0); // Set the target position


           
        }

        if (Input.GetKeyDown(KeyCode.Escape) && menuPhase == 2)
        {
            targetMountainPosition = new Vector3(3.81f, -9.94f, 0); // Set the target position


            

        }

        if (mountain.transform.position.x != -12.5f) //only access once mountain is moving
        {
            Color currentColor = logoSpriteRenderer.color;
            currentColor.a = 0f;
            logoSpriteRenderer.color = currentColor; Debug.Log("make logo invisible");// make title invisible
            pressKeyInstruction.enabled = false;
        }

            if (mountain.transform.position.x == -12.5f) //only access once mountain is done moving
        {
            menuPhase = 2;

            menu2Buttons.transform.position = buttonTransformBasedOnMainmenuscriptTransform; // make menu2 buttons visible
        }

        if (mountain.transform.position.x != -12.5f)
        {
            menu2Buttons.transform.position = new Vector2(1000, 1000); // make menu2 buttons invisible
        }



            if (mountain.transform.position.x == 3.81f)
        {
            menuPhase = 1;

            Color currentColor = logoSpriteRenderer.color;
            currentColor.a = 1f; 
            logoSpriteRenderer.color = currentColor;// make title visible
            pressKeyInstruction.enabled = true;

        }

        // Move the mountain towards the target position
        mountain.transform.position = Vector3.MoveTowards(mountain.transform.position, targetMountainPosition, mountainMovespeed * Time.deltaTime);
    }

    public void startNormalGame()
    {
        persistentscript.gameType = 1;
        SceneManager.LoadScene("SampleScene");
    }

    public void startRandomizedGame()
    {
        persistentscript.gameType = 2;
        SceneManager.LoadScene("SampleScene");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}