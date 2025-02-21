using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] ballVariants;
    public GameObject targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio;

    public float minSpawn = 5.0f;
    public float maxSpawn = 10.0f;

    public float minX = -5.39f;
    public float maxX = 5.5f;
    public float minY = -3.29f;
    public float maxY = 2.95f;

    public Pins pinsDB;

    private void Start()
    {
        spawnBall();
        spawnPin();
    }

    private void Update()
    {
        float currentTime = Time.time;
        float timeElapsd = currentTime - startTime;

        if(timeElapsd > spawnRatio)
        {
            spawnBall();
        }
    }

    void spawnBall()
    {
        int numVariants = ballVariants.Length;
        if(numVariants > 0)
        {
            int selection = Random.Range(0, numVariants);
            newObject = Instantiate(ballVariants[selection], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            BallBehavior ballBehavior = newObject.GetComponent<BallBehavior>();
            ballBehavior.setBounds(minX, maxX, minY, maxY);
            ballBehavior.setTarget(targetObject);
            ballBehavior.initialPosition();
        }

        setRatio();
        startTime = Time.time;
    }

    void setRatio()
    {
        spawnRatio = Random.Range(minSpawn, maxSpawn);
    }

    void spawnPin()
    {
        targetObject = Instantiate(pinsDB.getPin(CharecterManager.selection).prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

    }
}
