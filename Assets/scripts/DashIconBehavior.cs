using TMPro;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;
public class DashIconBehavior : MonoBehaviour
{
    TextMeshProUGUI text;
    Image overlay;
    float cooldown;
    float cooldownRate;

    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();

        Image[] images = GetComponentsInChildren<Image>();
        for(int i = 0; i < images.Length; i++)
        {
            if(images[i].tag == "Overlay")
            {
                overlay = images[i];
            }
        }

        cooldownRate = PinBehavior.cooldownRate;
        overlay.fillAmount = 0.0f;
    }

    void Update()
    {
        cooldown = PinBehavior.cooldown;

        string message = "";

        if(cooldown > 0.0)
        {
            float fill = cooldown / cooldownRate;
            message = string.Format("{0:0.0}", cooldown);
            overlay.fillAmount = fill;
        }

        text.SetText(message);
    }
}
