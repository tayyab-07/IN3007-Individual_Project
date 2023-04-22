using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tutorial from: https://www.youtube.com/watch?v=f473C43s8nE&t=379s&ab_channel=Dave%2FGameDevelopment
// This file is NOT written by me

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moves camera to the "CameraPos" object in "Player" holder
        transform.position = cameraPosition.position;
    }
}
