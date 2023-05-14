using UnityEngine;

// This class was written by me

// Class to destroy the smoke prefab after a set amount of time

public class DestroySmoke : MonoBehaviour
{
    float destroyTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;

        if (destroyTimer > 12.5f)
        {
            Destroy(gameObject);
        }
    }
}
