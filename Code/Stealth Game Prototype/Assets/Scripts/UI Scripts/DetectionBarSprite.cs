using UnityEngine;
using UnityEngine.UI;

public class DetectionBarSprite : MonoBehaviour
{
    public Slider slider;

    public Image fill;
    public Image border;
    public Image binoculars;

    public void SetDetection(float detection)
    {
        slider.normalizedValue = detection;
    }

    public void DisplayDetected()
    { 
        fill.enabled = true;
        border.enabled = true;
        binoculars.enabled = true;
    }

    public void DisableDetected()
    {
        fill.enabled = false;
        border.enabled = false;
        binoculars.enabled = false; 
    }

}
