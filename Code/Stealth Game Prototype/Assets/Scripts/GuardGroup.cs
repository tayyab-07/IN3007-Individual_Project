using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardGroup : MonoBehaviour
{

    public Guard[] guards;
    Transform player;

    System.Random rnd = new System.Random();

    [Header("Search Locations")]
    public Vector3 loc0 = new Vector3(18, 0, 18);
    public Vector3 loc1 = new Vector3(0, 0, 22);
    public Vector3 loc2 = new Vector3(-22, 0, 22);
    public Vector3 loc3 = new Vector3(-22, 0, 0);
    public Vector3 loc4 = new Vector3(-22, 0, -22);
    public Vector3 loc5 = new Vector3(0, 0, -22);
    public Vector3 loc6 = new Vector3(22, 0, -22);
    public Vector3 loc7 = new Vector3(-22, 0, 0);
    public Vector3 loc8 = new Vector3(8, 0, 3.5f);
    public Vector3 loc9 = new Vector3(-8.5f, 0, 17);

    [Header("Search")]
    public float searchTimer;
    public bool searching = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDetection();

        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i].zone == Guard.ZoneState.emptyZone && guards[i].playerSeen == true)
            {
                ConductSearch();
            }
        }
    }

    void CheckDetection()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i].attackPlayer == true)
            {
                ResetSearch();

                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].agent.SetDestination(player.position);
                }
            }
        }
    }

    void ConductSearch()
    {
        searchTimer = searchTimer + Time.deltaTime;

        for (int i = 0; i < guards.Length; i++)
        {
            if (searchTimer > 2 && searchTimer < 15 && guards[i].search1 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search1 = true;
            }

            if (searchTimer > 15 && searchTimer < 30 && guards[i].search2 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search2 = true;
            }

            if (searchTimer > 30)
            {
                guards[i].agent.SetDestination(guards[i].initialGuardLocation);
                guards[i].playerSeen = false;
                ResetSearch();
            }
        } 
    }

    void GuardSearch(Guard guard)
    {
        float rand = rnd.Next(0, 10);

        switch (rand)
        {
            case 0:
                guard.agent.SetDestination(loc0);
                break;
            case 1:
                guard.agent.SetDestination(loc1);
                break;
            case 2:
                guard.agent.SetDestination(loc2);
                break;
            case 3:
                guard.agent.SetDestination(loc3);
                break;
            case 4:
                guard.agent.SetDestination(loc4);
                break;
            case 5:
                guard.agent.SetDestination(loc5);
                break;
            case 6:
                guard.agent.SetDestination(loc6);
                break;
            case 7:
                guard.agent.SetDestination(loc7);
                break;
            case 8:
                guard.agent.SetDestination(loc8);
                break;
            case 9:
                guard.agent.SetDestination(loc9);
                break;
        }
    }

    void ResetSearch()
    {
        searchTimer = 0;
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].search1 = false;
            guards[i].search2 = false;
        }
    }


}