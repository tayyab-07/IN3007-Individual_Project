using UnityEngine;

public class PlayerZipline : MonoBehaviour
{
    public bool zipping = false;
    public KeyCode ziplineKey = KeyCode.E;
    public Rigidbody rb;
    public PlayerMovement pm;

    private Vector3 startposition;
    private Vector3 endPosition;

    private Zipline currentZipline;

    private float yOffset = 1f;
    private float Radius = 2f;

    private float ZiplineTime = 10.0f;
    private float totalTime; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(ziplineKey))
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, yOffset, 0), Radius, Vector3.up);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Zipline")
                {
                    currentZipline = hits[i].collider.gameObject.GetComponent<Zipline>();

                    if (currentZipline.landingZone != null) 
                    { 
                        zipping = true;
                        startposition = transform.position;
                        endPosition = currentZipline.landingZone.zipHookPoint.position;

                        pm.enabled = false;
                        rb.useGravity = false;
                        rb.isKinematic = true;
                    }
                }
            }
        }

        if (zipping)
        {
            Zip();
        }
    }

    void Zip()
    {
        totalTime += Time.deltaTime;
        float percentageCompleted = totalTime / ZiplineTime;
        transform.position = Vector3.Lerp(startposition, endPosition, percentageCompleted);

        if (percentageCompleted >= 0.99f)
        { 
            zipping = false;

            pm.enabled = true;
            rb.useGravity = true;
            rb.isKinematic = false;

            totalTime = 0;
            percentageCompleted = 0;
        }
    }
}
