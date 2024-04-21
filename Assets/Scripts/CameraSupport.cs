using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSupport : MonoBehaviour
{
    // Capture reference to Main Camera for method use
    private Camera mainCamera = null;
    
    // Holds the bounds of a 2D Game World in X and Y 
    private Bounds worldBound;

    // Start is called before the first frame update
    void Start()
    {
        // Get the main camera in the scene
        mainCamera = Camera.main;
        // Calculate bounds of the world
        CalculateWorldBound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Calculate bounds of world
    // Uses mainCamera and stores results in worldBound
    private void CalculateWorldBound()
    {
        float halfY = mainCamera.orthographicSize;
        float halfX = halfY * mainCamera.aspect;
        Vector3 center = mainCamera.transform.position;
        center.z = 0.0f;
        worldBound.center = center;
        worldBound.size = new Vector3(2 * halfX, 2 * halfY, 1.0f);
    }

    // Returns the worldBound
    public Bounds GetWorldBounds() { return worldBound; }

    // Checks if the Vector3 X and Y pos are within the bounds of the game world camera.
    public bool IsInsideBounds(Vector3 pos) { return !IsOutsideBounds(pos); }

    // Checks if the Vector3 X and Y pos are outside the bounds of the game world camera.
    public bool IsOutsideBounds(Vector3 pos)
    {
        // If input position is outside of game world boundaries
        if ( pos.x < worldBound.min.x || pos.x > worldBound.max.x ||
             pos.y < worldBound.min.y || pos.y > worldBound.max.y )
        {
            return true;
        } else return false;
    }

    
}
