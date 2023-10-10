using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    public Image staminaProgressUI = null;
    public CanvasGroup staminaCanvasGroup = null;

    public void UpdateStamina(float currentStamina, float maxStamina, int value)
    {

        // Fill bar based on stamina amount
        staminaProgressUI.fillAmount = currentStamina / maxStamina;

        if (value == 0)
        {
            staminaCanvasGroup.alpha = 0;
        }
        else
        {
            staminaCanvasGroup.alpha = 1;
        }
    }
}
