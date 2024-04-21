using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Public variables
    public float moveSpeed = 15.0f;
    public float rotationSpeed = 60.0f;
    public float cooldownTime = 0.2f;

    public static float cooldown = 0.0f;

    // The Egg Game Object to to fire with Spacebar
    public GameObject eggPrefab; // Link established in Unity


    // Private variables
    private Camera mainCamera;
    private float lastSpawnTime;


    // Static variables
    public static bool moveWithCamera = true;
    public static int numberOfEggs = 0;


    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            moveWithCamera = !moveWithCamera;
        }
        

        if (mainCamera != null && moveWithCamera)
        {
            // Get the mouse position in screen coordinates
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position from screen coordinates to world coordinates
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0.0f;
            transform.position = worldPosition;

        } else
        {
            // Move forever forward and rotate with A and D.
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            transform.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * rotationSpeed * horizontalInput);
        }

        if (Time.time - lastSpawnTime >= cooldownTime)
        {
            cooldown = 0.0f;

            // Is space is hit then shoot eggs.
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(eggPrefab, transform.position, transform.rotation);
                lastSpawnTime = Time.time;
            }
        } else
        {
            cooldown = (Time.time - lastSpawnTime) * 10;
        }
    }
}

