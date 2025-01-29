using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private float timer;
    private TextMeshProUGUI textField;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //fetchs the relevant component from the game object this is attached too
        textField = GetComponent<TextMeshProUGUI>();

        if(textField == null)
        {
            Debug.Log("No component found :(");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer = Time.time;

        int minutes = (int) timer / 60;
        int seconds = (int) timer % 60;

        //For the curly brackets, 0:00 means it'll find the first element in the list (0) and then represent it
        //in a two digit format (00). Same for 1:00, thats the second value.
        string message = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
        textField.text = message;
    }
}
