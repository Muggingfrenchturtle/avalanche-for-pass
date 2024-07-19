using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camphealthspritescript : MonoBehaviour
{

    public logicscript logicscript;
    public SpriteRenderer spriteRenderer;
    public Sprite noalive;
    public Sprite onealive;
    public Sprite twoalive;
    public Sprite threealive;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (logicscript.campHealth)
        {
            case 0:
                {
                    spriteRenderer.sprite = noalive; //i guessed my way through this lol, i didnt look at google
                    break;
                }
            case 1:
                {
                    spriteRenderer.sprite = onealive;
                    break;
                }
            case 2:
                {
                    spriteRenderer.sprite = twoalive;
                    break;
                }
            case 3:
                {
                    spriteRenderer.sprite = threealive;
                    break;
                }
        }
    }
}
