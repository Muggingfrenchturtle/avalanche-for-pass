using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellamountspritescript : MonoBehaviour
{
    public playerscript playerscript;
    public SpriteRenderer spriteRenderer;
    public Sprite noshell;
    public Sprite oneshell;
    public Sprite twoshell;
    public Sprite threeshell;
    // Start is called before the first frame update
    void Start()
    {
        playerscript = GameObject.FindGameObjectWithTag("player").GetComponent<playerscript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        switch (playerscript.ShellAmount)
        {
            case 0:
                {
                    spriteRenderer.sprite = noshell; //i guessed my way through this lol, i didnt look at google
                    break;
                }
            case 1:
                {
                    spriteRenderer.sprite = oneshell;
                    break;
                }
            case 2:
                {
                    spriteRenderer.sprite = twoshell;
                    break;
                }
            case 3:
                {
                    spriteRenderer.sprite = threeshell;
                    break;
                }
        }
        
    }
}
