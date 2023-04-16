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

    float angleA;
    float angleB;
    float angleC;

    float zone1Timer;
    float zone2Timer;
    float zone3Timer;
    float zone4Timer;
    float zone5Timer;

    float farViewingDist;
    float mediumViewingDist;
    float nearViewingDist;

    public bool playerSeen = false;
    public bool guardSearchingArea1 = false;
    public bool guardSearchingArea2 = false;
    public bool guardSearchingArea3 = false;

    Vector3 xPosEdge = new Vector3(23, 1, 1);
    Vector3 xNegEdge = new Vector3(-23, 1, 1);
    Vector3 zPosEdge= new Vector3(1, 1, 23);
    Vector3 zNegEdge= new Vector3(1, 1, -23);

    System.Random rnd = new System.Random();

    //made variable public, to be able to check timer during run time
    public float timePlayerVisible;
    public float searchTimer;

    Transform player;   
    Color initialSpotlightColour; 
    Color spottedColour;
    Vector3 initialGuardLocation = new Vector3();

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
        initialGuardLocation = transform.position;

        // define angles timers and distances for zones
        angleA = 30;
        angleB = 90;
        angleC = 150;

        zone1Timer = 1.0f;
        zone2Timer = 1.5f;
        zone3Timer = 2.0f;
        zone4Timer = 3.0f;
        zone5Timer = 5.0f;

        farViewingDist = 40.0f;
        mediumViewingDist = 25.0f;
        nearViewingDist = 15.0f;

        initialSpotlightColour = spotlight.color; //[2]
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
                playerSeen = true;
                ResetSearch();
            }
        }

        else if (zone == ZoneState.zone2)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  //[2]
            if (timePlayerVisible >= zone2Timer)
            {
                spottedColour = Color.magenta;
                agent.SetDestination(player.position);
                playerSeen = true;
                ResetSearch();
            }
        }

        else if (zone == ZoneState.zone3)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone3Timer)
            {
                spottedColour = Color.yellow;
                agent.SetDestination(player.position);
                playerSeen = true;
                ResetSearch();
            }
        }

        else if (zone == ZoneState.zone4)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone4Timer)
            {
                spottedColour = Color.green;
                agent.SetDestination(player.position);
                playerSeen = true;
                ResetSearch();
            }
        }

        else if (zone == ZoneState.zone5)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone5Timer)
            {
                spottedColour = Color.blue;
                agent.SetDestination(player.position);
                playerSeen = true;
                ResetSearch();
            }
        }

        else if (zone == ZoneState.emptyZone)
        {
            spottedColour = initialSpotlightColour;
            timePlayerVisible = timePlayerVisible - Time.deltaTime;    //[2]
        }

        if (playerSeen == true && zone == ZoneState.emptyZone)
        { 
            searchTimer = searchTimer + Time.deltaTime;

            if (searchTimer == 0) // fix all this
            {
                GuardLookAround();
            }

            if (searchTimer < 10) 
            {
                GuardLookAround();
            }

            if (searchTimer > 10 && searchTimer < 20 && guardSearchingArea1 == false)
            {
                GuardSearch(rnd.Next(0,3));
                guardSearchingArea1 = true;
            }

            if (searchTimer > 20 && searchTimer < 30 && guardSearchingArea2 == false)
            {
                GuardSearch(rnd.Next(0, 3));
                guardSearchingArea2 = true;
                //GuardLookAround();
            }

            if (searchTimer > 30 && searchTimer < 40 && guardSearchingArea3 == false)
            {
                GuardSearch(rnd.Next(0, 3));
                guardSearchingArea3 = true;
                //GuardLookAround();
            }

            if (searchTimer > 40 && timePlayerVisible < 0.1)
            {
                agent.SetDestination(initialGuardLocation);
                playerSeen = false;
                ResetSearch();
            }

        }

        if (playerSeen == false)
        {
            ResetSearch();
        }

        // timer doesnt exceed 0 or zone 5 timer
        timePlayerVisible = Mathf.Clamp(timePlayerVisible, 0, zone5Timer);   //[2]
        spotlight.color = spottedColour;
    }

    public void StateHandler()
    {
        Vector3 distToPlayer;
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


    private void GuardSearch(int corner)
    {
        if (corner == 0)
        {
            agent.SetDestination(xPosEdge + new Vector3(rnd.Next(-2, 2), 0, rnd.Next(-6, 6)));
        }

        else if (corner == 1)
        {
            agent.SetDestination(xNegEdge + new Vector3(rnd.Next(-2, 2), 0, rnd.Next(-6, 6)));
        }

        else if (corner == 2)
        {
            agent.SetDestination(zPosEdge + new Vector3(rnd.Next(-6, 6), 0 ,rnd.Next(-2, 2)));
        }

        else if (corner == 3)
        {
            agent.SetDestination(zNegEdge + new Vector3(rnd.Next(-6, 6), 0, rnd.Next(-2, 2)));
        }
    }

    private void ResetSearch()
    {
        searchTimer = 0;
        guardSearchingArea1 = false;
        guardSearchingArea2 = false;
        guardSearchingArea3 = false;
    }

    private void GuardLookAround()
    {
        float guardRotatePos = 0;
        guardRotatePos = guardRotatePos + (Time.deltaTime * 0.05f);

        float guardRotateNeg = 0;
        guardRotateNeg = guardRotateNeg - (Time.deltaTime * 0.05f);

        transform.Rotate(new Vector3(0, 1, 0), 1.5f);
        //transform.Rotate(new Vector3(0, 1, 0), guardRotateNeg);
    }

    // draws gizmo to help visulise distance in scene view
    private void OnDrawGizmos()  //[1]
    {
        Gizmos.color = Color.red;  //[1]
        Gizmos.DrawRay(transform.position, transform.forward * 30);  //[1]
    }

}
