using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int maxPlanes = 10;
    public static int numberOfPlanes = 0;
    public static int destroyedPlanes = 0;
    private Bounds worldBound;
    public GameObject enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        worldBound = Camera.main.GetComponent<CameraSupport>().GetWorldBounds();
    }

    // Update is called once per frame
    void Update()
    {
        // If less than specified number of enemy planes then spawn more.
        if (numberOfPlanes < maxPlanes)
        {
            ++numberOfPlanes;
            Instantiate(enemyPrefab, RandomSpawnPosition(), enemyPrefab.transform.rotation);          
        }
    }

    // Call when an enemy is destroyed
    public void EnemyDestroyed() {
        --numberOfPlanes;
        destroyedPlanes++;
    } 

    // Returns a random position within the X and Y worldBounds, reduced by 90%.
    private Vector3 RandomSpawnPosition()
    {
        float x = Random.Range(worldBound.min.x * 0.9f, (worldBound.max.x + 1) * 0.9f);
        float y = Random.Range(worldBound.min.y * 0.9f, (worldBound.max.y + 1) * 0.9f);
        float z = 0.0f;
        return new Vector3(x, y, z);
    }
}
