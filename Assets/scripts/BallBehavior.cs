using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    //base movement variables
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;

    Vector2 targetPosition;
    public int secondsToMaxSpeed;

    //launch variables
    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;
    public float timeLaunchStart;

    //physics
    Rigidbody2D body;
    public bool rerouting;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition();
    }

    //I'm not sure why ths isn't just in Start(), but that's what the slides said.
    public void initialPosition()
    {
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
        rerouting = true;
    }

    private void FixedUpdate()
    {
        if (onCooldown() == false)
        {
            if (launching == true)
            {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration)
                {
                    startCooldown();
                }
            }

            else
            {
                launch();
            }
        }


        //go to object attached to script, get the transform component, look at it's position
        Vector2 currentPosition = body.position;
        float distance = Vector2.Distance(targetPosition, currentPosition);

        if (distance > 0.1)
        {
            float currentSpeed;

            if (launching == true)
            {
                float launchingForHowLong = Time.time - timeLaunchStart;

                if (launchingForHowLong > launchDuration)
                {
                    startCooldown();
                }

                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, getDifficultyPercentage());
            }

            else
            {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, getDifficultyPercentage());
            }


            //This line says how much time has passed since the last check, so that framerate doesn't make things run faster. deltaTime might be 0.2 with an fps of 50 and 0.1 with an fps of 100, so it's equalized.
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, currentSpeed);
            body.MovePosition(newPosition);
        }

        else
        {
            if (launching == true)
            {
                startCooldown();
            }

            targetPosition = getRandomPosition();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            targetPosition = getRandomPosition();
        }

        if(collision.gameObject.tag == "Ball")
        {
            Reroute(collision);
            //Debug.Log("Does this happen?");
        }
    }

    Vector2 getRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        
        Vector2 v = new Vector2(randomX, randomY);
        return v;
    }

    private float getDifficultyPercentage()
    {
        //clamp01 is normalizing the number, making the number between the range of 0 and 1
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);

        return difficulty;
    }

    private void launch()
    {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;

        if (launching == false)
        {
            timeLaunchStart = Time.time;
            launching = true;
        }
    }

    public bool onCooldown()
    {
        bool result = false;

        //subtracts the time of the last launch from the concept of time itself to get your actual time!
        float timeSinceLastLaunch = Time.time - timeLastLaunch;

        if (timeSinceLastLaunch < cooldown)
        {
            result = true;
        }

        return result;
    }

    public void startCooldown()
    {
        timeLastLaunch = Time.time;
        launching = false;
    }

    public void Reroute(Collision2D collision)
    {
        GameObject otherBall = collision.gameObject;
        if(rerouting == true)
        {
            otherBall.GetComponent<BallBehavior>().rerouting = false;

            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;

            launching = false;

            float seperationDistance = 0.1f;
            ballBody.position += contact * seperationDistance;
        }

        else
        {
            rerouting = true;
        }
    }

    public void setBounds(float miX, float maX, float miY, float maY)
    {
        minX = miX;
        maxX = maX;
        minY = maY;
        maxY = miY;
    }

    public void setTarget(GameObject pin)
    {
        target = pin;
    }

}
