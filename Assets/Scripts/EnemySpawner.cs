using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // takes public list of spawn points which will be used in enemyAI script
    // static list so value wont change and its public so we can assign spawnPoints
    // we equate it to new ListTransform because its a static list
    public static List<Transform> spawnPoints = new List<Transform>();
    // 2 new public fields one to contain the prefab and one to contain the enemies
    public GameObject enemyPrefab, enemyContainer;
    // after how much time enemy will spawn one after another = spawnTime
    // how many enemies will spawn at once = enemyBurstCount
    // suppose there are 4 points and you want 4 enemies to spawn
    // keep burstCount as 4
    // suppose you have 4 points and 2 enemies are destroyed then 
    // 2 enemies will be respawned with a time gap of 1 second
    public float enemyBurstCount = 3, spawnTime = 1;

    // 
    Transform oldLocation;

    Transform location;
    float updateTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
            spawnPoints.Add(child);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > updateTime)
        {
            updateTime = Time.time + spawnTime;
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        // if enemy burstcount is greater than childcount in enemy container
        // say container has 3 enemies and burst count is 4 then this script will run
        // and only one enemy will be spawned after that this condition will no longer satisfy
        if (enemyContainer.transform.childCount < enemyBurstCount)
        {
            // assigning random location to enemy to spawn from
            location = spawnPoints[Random.Range(0, transform.childCount)];

            // if old location is equal to new location we dont aassign a new value to location
            // till oldlocation is not equal to location this while loop will continue
            // once new location point is obtained, new enemy will be instantiated
            // from that particular location
            while (location == oldLocation)
                location = spawnPoints[Random.Range(0, transform.childCount)];

            // storing old location so that we dont spawn an enemy from same location
            oldLocation = location;

            var enemyInstance = Instantiate(enemyPrefab, location.position, location.rotation);
            enemyInstance.transform.SetParent(enemyContainer.transform);

        }
    }
}
