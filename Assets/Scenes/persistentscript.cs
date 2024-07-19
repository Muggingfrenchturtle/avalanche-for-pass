using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class persistentscript : MonoBehaviour
{

    //put data you want to be persistent between scenes here

    public int gameType;

    public bool isEditor;


    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (isEditor == false)
        {
            SceneManager.LoadScene("Main menu");
        }
    }

    
    void Update()
    {
        
    }
}
