using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    //base movement variables
    public float minX = -9.78f;
    public float maxX = 9.8f;
    public float minY = -4.2f;
    public float maxY = 4.15f;
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

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //secondsToMaxSpeed = 30;
        //minSpeed = 0.001f;
        //maxSpeed = 0.75f;
        targetPosition = getRandomPosition();
        startCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        //go to object attached to script, get the transform component, look at it's position
        Vector2 currentPosition = gameObject.GetComponent<Transform>().position;    
        float distance = Vector2.Distance(targetPosition, currentPosition);

        if (distance > 0.1)
        {
            float currentSpeed = 0f;

            if(launching == true)
            {
                float launchingForHowLong = Time.time - timeLaunchStart;

                if(launchingForHowLong > launchDuration)
                {
                    startCooldown();
                }

                else
                {
                    currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, getDifficultyPercentage());
                }
            }

            else
            {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, getDifficultyPercentage());
            }
            

            //This line says how much time has passed since the last check, so that framerate doesn't make things run faster. deltaTime might be 0.2 with an fps of 50 and 0.1 with an fps of 100, so it's equalized.
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPosition, targetPosition, currentSpeed);
            transform.position = newPosition;
        }

        else
        {
            targetPosition = getRandomPosition();
        }

        float timeLaunching = Time.time - timeLaunchStart;

        if (launching == true && timeLaunching > launchDuration)
        {
            startCooldown();
        }

        if (launching == true && onCooldown() == false)
        {
            launch();
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
        if (launching == false)
        {
            targetPosition = target.transform.position;
            timeLaunchStart = Time.time;
            launching = true;
        }
    }

    public bool onCooldown()
    {
        bool result = false;

        //subtracts the time of the last launch from the concept of time itself to get your actual time!
        timeLastLaunch = Time.time - timeLastLaunch;

        if (timeLastLaunch < cooldown)
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
}
