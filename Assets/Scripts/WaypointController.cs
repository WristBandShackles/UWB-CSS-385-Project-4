using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    private float health = 1.0f;
    private float eggDamage = 0.25f;
    private float alphaValue = 1.0f;
    private float numOfEggCollisions = 0;

    private float moveRange = 15.0f;

    private SpriteRenderer spriteRenderer = null;

    private Bounds worldBound;

    private bool hideWaypoint = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get SpriteRenderer in order to manipulate color
        spriteRenderer = GetComponent<SpriteRenderer>();

        worldBound = Camera.main.GetComponent<CameraSupport>().GetWorldBounds();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            hideWaypoint = !hideWaypoint;
        }

        if (hideWaypoint)
        {
            // Hide Waypoint
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.0f);
        } else
        {
            // Change alpha to reflect remaining health.
            Color currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color(currentColor.r, currentColor.g, currentColor.b, alphaValue);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        // Else if an egg hits the plane then decrement health and alpha.
        if (other.name == "Egg(Clone)" && !hideWaypoint)
        {
            health = health - eggDamage;
            numOfEggCollisions++;
            alphaValue = alphaValue - eggDamage;

            // If four or more eggs hit the plane then destroy and move.
            if (numOfEggCollisions >= 4)
            {
                float minX = worldBound.min.x * 0.9f;
                float maxX = worldBound.max.x * 0.9f;
                float minY = worldBound.min.y * 0.9f;
                float maxY = worldBound.max.y * 0.9f;
                Vector3 initialPosition = transform.position;

                float randomX = Random.Range(Mathf.Max(minX, initialPosition.x - moveRange), Mathf.Min(maxX, initialPosition.x + moveRange));
                float randomY = Random.Range(Mathf.Max(minY, initialPosition.y - moveRange), Mathf.Min(maxY, initialPosition.y + moveRange));

                Vector3 targetPosition = new Vector3(randomX, randomY, 0f);

                // Move the object to the target position
                transform.position = targetPosition;

                health = 1.0f;
                alphaValue = 1.0f;
                numOfEggCollisions = 0;

            }

        } 
    }
}
