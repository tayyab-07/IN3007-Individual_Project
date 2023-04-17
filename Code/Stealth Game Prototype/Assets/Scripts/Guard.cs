using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

// [1] tutorial from: https://www.youtube.com/watch?v=TfhPBAe9Tt8&ab_channel=SebastianLague
// [2] tutorial from: https://www.youtube.com/watch?v=MOLg3W0HeLs&t=210s&ab_channel=SebastianLague

// Disclaimer: most of this file is written by me.
// there are some bits still kept from the sources above.
// i will use [1] and [2] at the end of the lines that were NOT written by me

public class Guard : MonoBehaviour
{
    public Light spotlight;
    public LayerMask viewMask;
    public NavMeshAgent agent;

    float angleA = 30;
    float angleB = 90;
    float angleC = 150;

    float zone1Timer = 1.0f;
    float zone2Timer = 1.5f;
    float zone3Timer = 2.0f;
    float zone4Timer = 3.0f;
    float zone5Timer = 5.0f;

    float farViewingDist = 40.0f;
    float mediumViewingDist = 25.0f;
    float nearViewingDist = 15.0f;

    public bool playerSeen = false;

    [Header("Search")]
    public bool search1 = false;
    public bool search2 = false;

    public bool attackPlayer = false;

    //made variable public, to be able to check timer during run time
    public float timePlayerVisible;

    Transform player;   
    Color initialSpotlightColour; 
    Color spottedColour;
    public Vector3 initialGuardLocation = new Vector3();

    // enum state to store different detection zones
    public ZoneState zone;
    public enum ZoneState
    { 
        emptyZone,
        zone1,
        zone2, 
        zone3,
        zone4,
        zone5
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;  //[1]

        initialSpotlightColour = spotlight.color; //[2]
        initialGuardLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StateHandler();

        // conditional statements set colour after set amount of time based on zone
        if (zone == ZoneState.zone1)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  //[2] 
            if (timePlayerVisible >= zone1Timer)
            {
                spottedColour = Color.red;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone2)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  //[2]
            if (timePlayerVisible >= zone2Timer)
            {
                spottedColour = Color.magenta;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone3)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone3Timer)
            {
                spottedColour = Color.yellow;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone4)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone4Timer)
            {
                spottedColour = Color.green;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.zone5)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone5Timer)
            {
                spottedColour = Color.blue;
                agent.SetDestination(player.position);
                attackPlayer = true;
                playerSeen = true;
            }
        }

        else if (zone == ZoneState.emptyZone)
        {
            attackPlayer = false;
            spottedColour = initialSpotlightColour;
            timePlayerVisible = timePlayerVisible - Time.deltaTime;    //[2]
        }

        // timer doesnt exceed 0 or zone 5 timer
        timePlayerVisible = Mathf.Clamp(timePlayerVisible, 0, zone5Timer);   //[2]
        spotlight.color = spottedColour;
    }

    public void StateHandler()
    {
        Vector3 distToPlayer; // make these locally defined once
        float playerGuardAngle;

        // conditional statements to find out what zone a player is in
        // always set to empty zone if any condition isnt met
        if (Vector3.Distance(transform.position, player.position) < nearViewingDist)  //[1]
        {
            distToPlayer = (player.position - transform.position).normalized;  //[1]
            playerGuardAngle = Vector3.Angle(transform.forward, distToPlayer);  //[1]
            if (playerGuardAngle < angleA / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone1;
            }

            else if (playerGuardAngle < angleC / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone2;
            }

            else
            {
                zone = ZoneState.emptyZone;
            }
        }

        else if (Vector3.Distance(transform.position, player.position) < mediumViewingDist)  //[1]
        {
            distToPlayer = (player.position - transform.position).normalized;  //[1]
            playerGuardAngle = Vector3.Angle(transform.forward, distToPlayer);  //[1]
            if (playerGuardAngle < angleA / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone2;
            }

            else if (playerGuardAngle < angleB / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone3;
            }

            else if (playerGuardAngle < angleC / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone4;
            }

            else
            {
                zone = ZoneState.emptyZone;
            }
        }

        else if (Vector3.Distance(transform.position, player.position) < farViewingDist)  //[1]
        {
            distToPlayer = (player.position - transform.position).normalized;   //[1]
            playerGuardAngle = Vector3.Angle(transform.forward, distToPlayer);   //[1]
            if (playerGuardAngle < angleA / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone3;
            }

            else if (playerGuardAngle < angleB / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone4;
            }

            else if (playerGuardAngle < angleC / 2f && !Physics.Linecast(transform.position, player.position, viewMask))
            {
                zone = ZoneState.zone5;  
            }
            else
            {
                zone = ZoneState.emptyZone;
            }
        }

        else 
        {
            zone = ZoneState.emptyZone;
        }
    }

    // draws gizmo to help visulise distance in scene view
    private void OnDrawGizmos()  //[1]
    {
        Gizmos.color = Color.red;  //[1]
        Gizmos.DrawRay(transform.position, transform.forward * 30);  //[1]
    }

}
