using UnityEngine;

public class PinBehavior : MonoBehaviour
{

    public float speed = 4.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        //go from the current pos (transform.position) to the mouse pos at a certain speed)
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        //set the current position to the new value
        transform.position = newPosition;


    }
}
