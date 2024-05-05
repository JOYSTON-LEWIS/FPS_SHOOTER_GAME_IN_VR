using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    // Instantiating bullet prefab
    public GameObject bulletRound;
    // Decide speed of bullet
    public float bulletSpeed;

    // Decide gun fire rate
    public float fireRate;

    // for an automatic gun with a fire rate
    // create a bool here
    bool shotFired;

    // Shooting logic
    public void Shoot()
    {
        // now we will check if the bool is false or not, only then
        // we will instantiate bullets
        if(!shotFired)
        {
            // Instantiate our bullet
            var spawnedBullet = GameObject.Instantiate(bulletRound, transform.position, transform.rotation);
            // access its rigidbody
            var rb = spawnedBullet.GetComponent<Rigidbody>();

            // bullet moves forward
            rb.velocity = spawnedBullet.transform.forward * bulletSpeed;

            // Now we dont want our bullets to stay in the scene
            // so after some time simply destroy it
            Destroy(spawnedBullet, 3f);

            // now we will set shotFired to True and
            // we will make it false again based on fire rate
            // fire rate is in seconds
            shotFired = true;
            Invoke("ResetRound", fireRate);
        }
    }

    public void ResetRound()
    {
        shotFired = false;
    }

    // dont need the following functions
    // Start is called before the first frame update
    /* void Start()
    {    
    }

    // Update is called once per frame
    void Update()
    {   
    }
    */

}
