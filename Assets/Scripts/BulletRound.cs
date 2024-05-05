using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRound : MonoBehaviour
{
    // variable for particle system instantiate
    public ParticleSystem hitEffect;

    // bullet Round if its of an enemy it should only affect player
    // bullet Round if its of player it should only effect the enemy
   
    public int targetObjectLayer;
    public int damage;

    // check if bullet has collided with any collider or not
    private void OnCollisionEnter(Collision collision)
    {
        // based on the target object affect the health of the target object
        if (collision.gameObject.layer == targetObjectLayer)
        {
            collision.gameObject.GetComponent<Health>().Damage(damage);
        }

        // now lets definte the public variables in BulletRound in editor

        // to get hit effect
        hitEffect.gameObject.SetActive(true);
        hitEffect.transform.SetParent(null);

        // if yes then sdestroy game object
        Destroy(gameObject);
    }


    /* following functions not needed
    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {   
    }
    */
}
