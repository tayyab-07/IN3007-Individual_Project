using UnityEngine;

public class BTGuardGroup : MonoBehaviour
{
    System.Random rnd = new System.Random();

    [Header("Guards")]
    public GuardBehaviourTree[] guards;

    [Header("Search Locations")]
    public Transform loc0;
    public Transform loc1;
    public Transform loc2;
    public Transform loc3;
    public Transform loc4;
    public Transform loc5;
    public Transform loc6;
    public Transform loc7;
    public Transform loc8;
    public Transform loc9;

    [Header("Search")]
    public float searchTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i].playerSeen == true && guards[i].playerVisible == false)
            {
                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].organiseSearch = true;
                }
                ConductSearch();
                break;
            }
        }

        ConductAttack();
    }

    void ConductSearch()
    {
        // Search for 30 seconds then reset to start location
        searchTimer = searchTimer + Time.deltaTime;

        // Set all guards to search one place until 15sec and another until 30sec
        for (int i = 0; i < guards.Length; i++)
        {
            if (searchTimer > 5 && searchTimer < 25 && guards[i].search1 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search1 = true;
            }

            if (searchTimer > 25 && searchTimer < 40 && guards[i].search2 == false)
            {
                GuardSearch(guards[i]);
                guards[i].search2 = true;
            }

            // After 40 sec, tell guards to go back to starting location
            if (searchTimer > 40)
            {
                ResetSearch();
            }
        }
    }

    void ConductAttack()
    {
        int guardsNotSeeingPlayer = 0;

        for (int i = 0; i < guards.Length; i++)
        {
            if (guards[i].playerVisible == true)
            {
                searchTimer = 0;
                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].search1 = false;
                    guards[j].search2 = false;
                    guards[j].organiseAttack = true;
                }
            }

            else
            {
                guardsNotSeeingPlayer = guardsNotSeeingPlayer + 1;
            }

            if (guardsNotSeeingPlayer == 4)
            {
                for (int j = 0; j < guards.Length; j++)
                {
                    guards[j].organiseAttack = false;
                }
                guardsNotSeeingPlayer = 0;
            }
        }
    }

    void GuardSearch(GuardBehaviourTree guard)
    {
        // Method to randomly assign a guard to a search location

        float rand = rnd.Next(0, 10);

        switch (rand)
        {
            case 0:
                guard.agent.SetDestination(loc0.position);
                break;
            case 1:
                guard.agent.SetDestination(loc1.position);
                break;
            case 2:
                guard.agent.SetDestination(loc2.position);
                break;
            case 3:
                guard.agent.SetDestination(loc3.position);
                break;
            case 4:
                guard.agent.SetDestination(loc4.position);
                break;
            case 5:
                guard.agent.SetDestination(loc5.position);
                break;
            case 6:
                guard.agent.SetDestination(loc6.position);
                break;
            case 7:
                guard.agent.SetDestination(loc7.position);
                break;
            case 8:
                guard.agent.SetDestination(loc8.position);
                break;
            case 9:
                guard.agent.SetDestination(loc9.position);
                break;
        }
    }

    void ResetSearch()
    {
        searchTimer = 0;
        for (int i = 0; i < guards.Length; i++)
        {
            guards[i].organiseSearch = false;
            guards[i].playerSeen = false;
            guards[i].search1 = false;
            guards[i].search2 = false;
        }
    }

}