using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tankclimbingspritescript : MonoBehaviour
{
    public playerscript playerscript;
    public bool enteringstage;

    public float movespeed;
    // Start is called before the first frame update
    void Start()
    {
        playerscript = GameObject.FindGameObjectWithTag("player").GetComponent<playerscript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.up * movespeed * Time.deltaTime);

        if (transform.position.y >= -4.1f && enteringstage == true)
        {
            playerscript.turnVisible();
            playerscript.playerCanShoot = true;
            Destroy(gameObject);
        }
    }

   
    //idk startanimation fucntion and endanimation function?
}
