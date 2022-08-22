using UnityEngine;
using UnityEngine.UI;

public class UIHealthIcon : MonoBehaviour
{
    Image lifeImage;
    Image deathImage;

    public void SwitchToDeathHeart()
    {
        lifeImage = GameObject.FindGameObjectWithTag("Life Image").GetComponent<Image>();
        deathImage = GameObject.FindGameObjectWithTag("Death Image").GetComponent<Image>();
        lifeImage.enabled = false;
        deathImage.enabled = true;
    }

    public void SwitchToLifeHeart()
    {
        lifeImage = GameObject.FindGameObjectWithTag("Life Image").GetComponent<Image>();
        deathImage = GameObject.FindGameObjectWithTag("Death Image").GetComponent<Image>();
        lifeImage.enabled = true;
        deathImage.enabled = false;
    }


}
