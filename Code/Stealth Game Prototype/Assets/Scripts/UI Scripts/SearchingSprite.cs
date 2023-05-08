using UnityEngine;
using UnityEngine.UI;

public class SearchingSprite : MonoBehaviour
{
    public Image image;

    public void displaySearching()
    {
        image.enabled = true;
    }

    public void disableSearching()
    {
        image.enabled = false;
    }
}
