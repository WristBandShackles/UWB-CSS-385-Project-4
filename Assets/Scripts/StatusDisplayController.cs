using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplayController : MonoBehaviour
{
    public TMP_Text textComponent;
    private bool hideWaypoint = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.H))
        {
            hideWaypoint = !hideWaypoint;
        }

        // Get values for status display. These are mostly static variables in other classes
        bool mouseModeActive = PlayerController.moveWithCamera;
        int eggsOnScreen = PlayerController.numberOfEggs;
        int enemyCount = SpawnManager.numberOfPlanes;
        int enemyDestroyed = SpawnManager.destroyedPlanes;
        bool randomMode = EnemyController.randomMode;
        float cooldown = PlayerController.cooldown;
        String drive = null;
        String waypointsVisibility = null;
        String mode = null;

        if (mouseModeActive)
        {
            drive = "Mouse";
        } else
        {
            drive = "Key";
        }

        if (hideWaypoint)
        {
            waypointsVisibility = "Not Shown";
        } else
        {
            waypointsVisibility = "Show";
        }

        if (randomMode)
        {
            mode = "Random";
        } else
        {
            mode = "Sequential";
        }

        // Display text
        textComponent.text = "Hero: Drive(" + drive +")    EGG: OnScreen(" + eggsOnScreen + ")    Enemy: Count(" + enemyCount + ") Destroyed(" + enemyDestroyed + ")    Mode(" + mode + ")    Waypoints(" + waypointsVisibility + ")    Cooldown(" + Mathf.RoundToInt(cooldown) + "sec)";
    }
}
