using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extrashellscript : MonoBehaviour
{
    public playerscript playerscript;
    // Start is called before the first frame update
    void Start()
    {
        playerscript = GameObject.FindGameObjectWithTag("player").GetComponent<playerscript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player") || collision.gameObject.CompareTag("deadzone"))
        {
            Destroy(gameObject);
        }
    }
}
