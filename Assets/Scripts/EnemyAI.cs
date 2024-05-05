using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    /*
     If a player comes close to enemy, 
    enemy will start chasing the player
    and when player stops
    and enemy reaches a certain distance
    then it will start attacking it

    for patrolling lets create some variables so that enemy goes 
    to that specific location one by oneand then creates a cycle
     
    we also need to assign the shooting script to a prefab
     so we will create prefab after this
    now we need to create variables to check distance of enemy to player
     */

    // reference to player

    // causes error.... player field is public and
    // we did not assign anything to it in editor
    // so assign it in start
    Transform player;
    // creating layermask for player
    public LayerMask isPlayer;

    // variable for shooting
    public Shooting shooting; 

    // creating a list for patrol
    List<Transform> points;
    // destination / index point where enemy will go to 0
    int destPoint = 0;
    // finally we need to reference navmeshagent, need AI library of unity
    NavMeshAgent Agent;

    // sightrange for enemy detecting the player and start chase
    // attackrange value which tells enemy when to start attacking it
    // bools for conditions
    public float sightRange, attackRange;
    private bool inSightRange, inAttackRange;

    // particle effect for destroying the enemy
    public GameObject particleEffect;

    // as this script is attached on prefab we will create a private field
    private ScoreManager scoreManager;

    // this will help assign different score for different enemies
    // setting default value of 100in case nothing is assigned
    public int enemyScoreValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        // initializing player variable
        // XRPlayerController this tag is found by finding
        // all the layers assigned as player
        // other error was in context for navmeshagent but 
        // we have assigned it to the enemy prefab so it
        // should not be an issue
        player = GameObject.FindGameObjectWithTag("XRPlayerController").transform;

        // initializing agent
        Agent = GetComponent<NavMeshAgent>();

        // using the same list used in enemy spawner in enemy AI
        points = EnemySpawner.spawnPoints;

        // since we have only one score manager this will work
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // will create a sphere around enemy for detection radius
        // if player enters the sphere will be set to true else will be false
        // we need to define radius as second parameter for the
        // checksphere function, so if its player only then
        // it will set inSightRange to true, also we need to create a layer mask
        inSightRange = Physics.CheckSphere(transform.position, sightRange
            , isPlayer);

        // we will use similar function for attack range.......
        // this was last step for first time editing now lets go back to unity
        inAttackRange = Physics.CheckSphere(transform.position, attackRange
            , isPlayer);

        if (!inSightRange && !inAttackRange) Patrol();
        else if (inSightRange && !inAttackRange) Chase();
        else if (inSightRange && inAttackRange) Attack();

        // constantly check if enemy health is below 0 or not
        if (GetComponent<Health>().health <= 0)
        {
            EnemyDeath();
            scoreManager.AddPoints(enemyScoreValue);
        }
    }

    // state when enemy is patrolling and not chasing ie player not near him
    void Patrol()
    {
        // we only want to switch these values , if
        // agent path pending is false and
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            if (points.Count == 0)
            {
                return;
            }
            Agent.destination = points[destPoint].position;

            // increase value of destination points
            // to make sure it does not cross the value of 
            // points count, like rotate between say 0 to 7, so
            // consider the remainder as well
            // so say start point is 0 and end point is 7
            // this function will update start point by 1 every time
            // 1 , so 1%7 = 1, then 2 so 2%7 = 2 and so on
            // when it reaches 7 so 7%7 = 0 and it will
            // return back to original position and
            // the cycle will continue
            destPoint = (destPoint * 1) % points.Count;
        }
    }

    // state when player is near to enemy and enemy is chasing player
    void Chase()
    {
        Agent.SetDestination(player.position);
    }

    // enemy reaches certain distance from player and attacks
    void Attack()
    {
        
        // here it will stop
        Agent.SetDestination(transform.position);

        // here it will look at the player
        transform.LookAt(player);

        // we use shooting script we had created from enemy
        shooting.Shoot();

    }

    public void EnemyDeath()
    {
        // when enemy is destroyed trigger the explosion particle effect
        particleEffect.SetActive(true);
        // just so that the effect does not go away when the parent object is destroyed we do this
        particleEffect.transform.SetParent(null);
        Destroy(gameObject);
    }

}
