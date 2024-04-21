using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    // Variable controlling Speed of egg
    public float moveSpeed = 40.0f;

    // Variable for camera to check if outside bounds.
    private Camera mainCamera = null;

    // Start is called before the first frame update
    void Start()
    {
        // Increase number of eggs that player has shot and are existing.
        PlayerController.numberOfEggs++;

        // Get the main camera in the scene
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Move Egg forward continuously
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);

        // Get Camera Support component
        CameraSupport camSupport = mainCamera.GetComponent<CameraSupport>();

        // If egg is out of bounds delete. Use IsOutsideBounds from camSupport script.
        if (camSupport.IsOutsideBounds(transform.position))
        {
            // if out of bounds then delete and decrement number of eggs player has shot.
            PlayerController.numberOfEggs--;
            Destroy(gameObject);
        }
    }

    // Detects if egg has collided with another game object that has trigger activated.
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Delete and decrement number of eggs player has shot.
        PlayerController.numberOfEggs--;
        Destroy(gameObject);
    }
    
}
