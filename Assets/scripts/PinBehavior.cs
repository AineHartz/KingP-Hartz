using UnityEngine;

public class PinBehavior : MonoBehaviour
{

    public float speed = 4.0f;
    public float normalSpeed = 4.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;
    Rigidbody2D body;

    //dash variables
    float dashSpeed = 8f;
    public bool dashing;
    public float dashDuration = 1f;
    public float timedashStart;

    //cooldown variables
    static public float cooldownRate = 5f;
    static public float endLastDash;
    static public float cooldown = 0f; 

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        dashing = false;
    }

    void Update()
    {
        dash();
    }

    private void FixedUpdate()
    {
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        //go from the current pos (transform.position) to the mouse pos at a certain speed)
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, speed * Time.fixedDeltaTime);
        //set the current position to the new value
        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with " + collided);
        if (collided == "Ball")
        {
            //UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
        }
    }

    private void dash()
    {
        if (dashing == true)
        {
            float howLong = Time.time - timedashStart;
            if (howLong > dashDuration)
            {
                dashing = false;
                speed = normalSpeed;
                endLastDash = Time.time;
                cooldown = cooldownRate;
            }
        }

        else
        {
            cooldown = cooldown - Time.deltaTime;

            if (Input.GetMouseButtonDown(0) == true && cooldown <= 0)
            {
                dashing = true;
                speed = dashSpeed;
                timedashStart = Time.time;
            }
        }
    }
}
