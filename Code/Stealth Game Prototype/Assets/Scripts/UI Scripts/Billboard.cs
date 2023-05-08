using UnityEngine;

// This class was NOT designed by me. 
// Tutorial from: https://www.youtube.com/watch?v=BLfNP4Sc_iA&ab_channel=Brackeys

public class Billboard : MonoBehaviour
{
    public Transform playerCam;

    void LateUpdate()
    {
        transform.LookAt(transform.position + playerCam.forward);
    }
}
