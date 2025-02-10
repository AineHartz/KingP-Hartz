using TMPro;
using UnityEditor.Overlays;
using UnityEngine;

public class DashIconBehavior : MonoBehaviour
{
    TextMeshProUGUI text;
    Image overlay;
    float cooldown;
    float cooldownRate;

    void Start()
    {
        Image[] images = GetComponentInChildren<Image>();
        for(int i = 0; i < images.Length; i++)
        {
            if(images[i].tag == "overlay")
            {
                overlay = images[i];
            }
        }

        text = GetComponentInChildren<TextMeshProUGUI>();
        cooldownRate = PinBehavior.cooldownRate;
    }

    void Update()
    {
        cooldown = PinBehavior.cooldown;

        if(cooldown > 0)
        {
            float fill = cooldown / cooldownRate;
            text.text = string.Format("{0: 0.0}" + PinBehavior.cooldown);
            overlay.fillAmount = fill;
        }
    }
}
