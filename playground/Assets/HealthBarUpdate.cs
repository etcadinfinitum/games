using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdate : MonoBehaviour
{
    private Image HealthBarImage;

    public void SetHealthBarValue(float value)
    {
        HealthBarImage.fillAmount = value;
        if(HealthBarImage.fillAmount < 0.3f)
        {
            SetHealthBarColor(Color.red);
        }
        else if(HealthBarImage.fillAmount < 0.6f)
        {
            SetHealthBarColor(Color.yellow);
        }
        else
        {
            SetHealthBarColor(Color.green);
        }
        if (value >= 1.0f) {
            Debug.Log("Level 1 completed! Advancing to Level 2.");
            gameObject.GetComponent<SwitchLevels>().NextLevel();
        }
    }

    public float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    public void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }

    void Start()
    {
        HealthBarImage = GetComponent<Image>();
        SetHealthBarValue(0.1f);
    }
}
