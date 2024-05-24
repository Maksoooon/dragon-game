using System;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class DragonMovement : MonoBehaviour
{
    public SplineContainer spline;
    public DragonSpawnerNEW dragonSpawnerScript;
    public bool isHead = false;
    public float speed = 1f;
    public float objectLength;
    public float currentSpeed;
    public float fastSpeed;
    public float fastSpeedDistance;
    public float fastSpeedPercentage;
    public float backSpeed = -4f;
    public float _t;
    public float backTime;
    public float backTimeT;

    public float distanceTraveled;
    public float distancePercentage;
    public float splineLength;
    public float knotLength;
    public float health;
    public TextMeshPro healthText1;
    public float healthTextAlpha;
    public bool textHealthGoBlank = true;

    public bool returnToStart = false;
    public float backDistance;
    public float returnToStartSpeed = -1000f;

    //public TextMeshPro healthText2;
    // <summary>
    // 0 - nothing 
    // 1 - Penetration
    // 2 - Bomb
    // 3 - 3x Shot speed
    // 4 - Triple shot
    // 5 - Lightning shot
    // </summary>

    public int powerUpOnDeath = 0;

    public bool isdead = false;
    public bool isGoingBack = false;
    public bool fast = true;

    private void Start()
    {

        spline = GameObject.Find("DragonPATH").GetComponent<SplineContainer>();
        splineLength = spline.CalculateLength();
        fastSpeedDistance = splineLength * fastSpeedPercentage + distanceTraveled;
        healthText1.text = health.ToString();
        //healthText2.text = health.ToString();
        backTimeT = 0f;
        textHealthGoBlank = true;
        returnToStartSpeed = -100f;
    }
    
    private void Update()
    {

        //go foward or foward fast or slow
        if (!dragonSpawnerScript.isPaused || returnToStart)
        {

            _t = dragonSpawnerScript._t;

            distanceTraveled += currentSpeed * Time.deltaTime;
            distancePercentage = distanceTraveled / splineLength;


            if (!(backTimeT > 0f) && !returnToStart)
            {
                if ((distanceTraveled < fastSpeedDistance) && fast)
                {

                    isGoingBack = false;
                    currentSpeed = fastSpeed;
                }
                else
                {
                    fast = false;
                    isGoingBack = false;
                    currentSpeed = speed;
                }

            }
            else if (distanceTraveled > backDistance && returnToStart)
            {
                currentSpeed = returnToStartSpeed;
            }
            else
            {
                backTimeT -= Time.deltaTime;
                isGoingBack = true;
                currentSpeed = -backSpeed;
                returnToStart = false;
            }
            
            //goto distence perentage 
            Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
            transform.position = currentPosition;


            //look direction
            Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.001f);
            Vector3 direction = nextPosition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction);


            if (isdead)
            {
                int position = int.Parse(this.name);
                for (int i = 0; i < position; i++)
                {
                    try
                    {
                        dragonSpawnerScript.tailGO[i].GetComponent<DragonMovement>().backTimeT += backTime;
                    }
                    catch
                    {

                    }

                }
                isdead = false;
                dragonSpawnerScript.tailLength -= 1;
                if (dragonSpawnerScript.tailLength < 1)
                {
                    dragonSpawnerScript.WinCondition();
                }
                Destroy(gameObject);
            }
        }
        //ChangeTextAlpha(healthTextAlpha);
        if (textHealthGoBlank && healthText1.color.a > 0) { ChangeTextAlpha(healthText1.color.a - 0.02f); }


    }
    public void setSpeedANDDistanceANDHealthANDFastspeed(float newSpeed, float newDistance, float newHealth, float newFastSpeed, float newFastSpeedPercentage, float newObjectLength, float newBackTime, DragonSpawnerNEW newScript)
    {
        speed = newSpeed;
        distanceTraveled = newDistance;
        health = newHealth;
        fastSpeed = newFastSpeed;
        fastSpeedPercentage = newFastSpeedPercentage;
        objectLength = newObjectLength;
        backTime = newBackTime;
        backSpeed = (newObjectLength - (newSpeed * newBackTime))/newBackTime;
        dragonSpawnerScript = newScript;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arrow"))
        {
            isdead = true;
        }
    }
    
    public void dragonHit(float damage)
    {
        health -= damage;
        healthText1.text = health.ToString();

        textHealthGoBlank = false;
        if (!isHead) ChangeTextAlpha(1f);
        //healthText2.text = health.ToString();
        if (health < 1)
        {
            isdead = true;
        }
    }

    public void ChangeTextAlpha(float alpha)
    {
        Color currentColor = healthText1.color;
        currentColor.a = alpha;
        healthText1.color = currentColor;
    }

    private void OnDestroy()
    {
        dragonSpawnerScript.powerUPsys.PowerUP(powerUpOnDeath);
    }

    
}
