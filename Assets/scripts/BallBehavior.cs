using UnityEngine;

public class BallBehavior : MonoBehaviour
{

    public float minX = -9.78f;
    public float maxX = 9.8f;
    public float minY = -4.2f;
    public float maxY = 4.15f;
    public float minSpeed;
    public float maxSpeed;

    Vector2 targetPosistion;
    public int secondsToMaxSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondsToMaxSpeed = 30;
        minSpeed = 0.75f;
        maxSpeed = 2.0f;
        targetPosistion = getRandomPosistion();
    }

    // Update is called once per frame
    void Update()
    {
        //go to object attached to script, get the transform component, look at it's posistion
        Vector2 currentPosistion = gameObject.GetComponent<Transform>().posistion;    

        if(targetPosistion != currentPosistion)
        {
            float currentSpeed = minSpeed;
            Vector2 newPosistion = Vector2.MoveTowards(currentPosistion, targetPosistion, currentSpeed);
            transform.posistion = newPosistion;
        }

        else
        {
            targetPosistion = getRandomPosistion();
        }

        getRandomPosistion();
    }

    Vector2 getRandomPosistion()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        
        Vector2 v = new Vector2(randomX, randomY);
        return v;
    }
}
