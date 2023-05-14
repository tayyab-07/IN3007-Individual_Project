using UnityEngine;

// This file was largely NOT written by me
// I only wrote the parts related to the cooldown and reset throw
// Tutorial from: https://www.youtube.com/watch?v=BYL6JtUdEY0&ab_channel=Brackeys

// Class to throw a smoke bomb

public class DropSmoke : MonoBehaviour
{
    [Header("Variables")]
    public float throwForce;
    public float throwCooldown;
    bool readyToThrow = true;
    Vector3 offset = new Vector3();

    [Header("Objects")]
    public GameObject grenadePrefab;
    public Transform Orienatation;

    [Header("Grenade Key")]
    public KeyCode grenadekey = KeyCode.G;

    // Update is called once per frame
    void Update()
    {
        // only throw the grenade if ready to throw. once thrown only reset the throw after 'throwCooldown' time
        if (Input.GetKey(grenadekey) && readyToThrow == true)
        {
            readyToThrow = false;

            ThrowGrenade();

            Invoke(nameof(ResetThrow), throwCooldown);
        }
    }

    // throw grenade in direction of player orientation object. Found under Player in hierachy
    void ThrowGrenade()
    { 
        offset = (Orienatation.forward * 1.5f);
        GameObject grenade = Instantiate(grenadePrefab, transform.position + offset, Orienatation.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(Orienatation.forward * throwForce, ForceMode.VelocityChange);
    }

    void ResetThrow()
    {
        readyToThrow = true;
    }
}
