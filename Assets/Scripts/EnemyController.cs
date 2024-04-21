using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float health = 1.0f;
    private float eggDamage = 0.2f;
    private float numOfEggCollisions = 0;
    private float alphaValue = 1.0f;
    private SpriteRenderer spriteRenderer = null;
    private SpawnManager spawnManager = null;

    public float moveSpeed = 20.0f;
    public float rotationSpeed = 100.0f;

    public GameObject[] waypoints;

    private int currentTarget;
    public static bool randomMode = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get SpriteRenderer in order to manipulate color
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get SpawnManager in order to call EnemyDestroyed and keep track of deaths.
        spawnManager = FindFirstObjectByType<SpawnManager>();

        // Find the "Waypoints" game object
        GameObject waypointsObject = GameObject.Find("Waypoints");

        // Check if the "Waypoints" object exists
        if (waypointsObject != null)
        {
            // Get references to the child waypoints and populate the array
            waypoints = new GameObject[waypointsObject.transform.childCount];
            for (int i = 0; i < waypointsObject.transform.childCount; i++)
            {
                waypoints[i] = waypointsObject.transform.GetChild(i).gameObject;
            }
            if (randomMode)
            {
                currentTarget = Random.Range(0, waypoints.Length);
            } else 
            {
                currentTarget = 0;
            }
            
        }
        else
        {
            Debug.LogError("Waypoints game object not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Change alpha to reflect remaining health.
        Color currentColor = spriteRenderer.color;
        spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alphaValue);

        // Waypoint targeting.
        if (waypoints[currentTarget] != null)
        {
            // Move Forward
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

            // Calculate angle between current rotation and target direction
            Vector3 targetDirection = waypoints[currentTarget].transform.position - transform.position;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle - 90);

            // Smoothly rotate towards the target angle
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // If player hits plane then just destroy entirely.
        if (other.name == "Player") {
            spawnManager.EnemyDestroyed();
            Destroy(gameObject);

        // Else if an egg hits the plane then decrement health and alpha.
        } else if (other.name == "Egg(Clone)")
        {
            health = health - eggDamage;
            numOfEggCollisions++;
            alphaValue = alphaValue * 0.8f;

            // If four or more eggs hit the plane then destroy.
            if (numOfEggCollisions >= 4)
            {
                spawnManager.EnemyDestroyed();
                Destroy(gameObject);
            }

        } else if (other.CompareTag("Waypoint"))
        {
            if (randomMode)
            {
                int temp = Random.Range(0, waypoints.Length);

                if(temp == currentTarget)
                {
                    temp++;
                    temp %= waypoints.Length;
                }
                currentTarget = temp;
            } else
            {
                currentTarget++;
                currentTarget %= waypoints.Length;
            }
           
        }
    }


}
