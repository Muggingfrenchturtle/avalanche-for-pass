using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class machinegunscript : MonoBehaviour
{
    public playerscript playerscript;
    public GameObject bullet;

    public float gunIdentity;

    public AudioSource gunshot;

    // Start is called before the first frame update
    void Start()
    {
        gunshot = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireBullet()
    {
        if (playerscript.lastGunFired == 2 && gunIdentity == 1)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            ++playerscript.bulletsOnScreen;
            gunshot.Play();
        }

        else if (playerscript.lastGunFired == 1 && gunIdentity == 2)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            ++playerscript.bulletsOnScreen;
            gunshot.Play();
        }
    }
}
