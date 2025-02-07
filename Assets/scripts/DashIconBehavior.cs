using TMPro;
using UnityEngine;

public class DashIconBehavior : MonoBehaviour
{
    TextMeshProUGUI text;
    float cooldown;
    float cooldownRate;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(PinBehavior.cooldown > 0)
        {
            text.text = string.Format("{0: 0.0}" + PinBehavior.cooldown);
        }
        
    }
}
