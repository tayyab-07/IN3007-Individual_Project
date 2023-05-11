using UnityEngine;

// This class was NOT designed by me. 
// Tutorial from: https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

public class Billboard : MonoBehaviour
{
    // class that makes the UI elements abbove a guard`s heads into a billboard
    // essentially, this class has the UI elements always face the player 

    public Transform playerCam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + playerCam.forward);
    }
}
