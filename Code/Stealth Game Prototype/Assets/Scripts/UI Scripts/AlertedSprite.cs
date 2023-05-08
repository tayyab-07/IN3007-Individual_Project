using UnityEngine;
using UnityEngine.UI;

public class AlertedSprite : MonoBehaviour
{
    public Image image;

    public void displayAlerted()
    {
        image.enabled = true;
    }

    public void disableAlerted() 
    { 
        image.enabled = false;
    }

}
