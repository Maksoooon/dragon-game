using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class DragonSpawnerNEW : MonoBehaviour
{
    //public NewDragonMove newDragonMove;
    //private SplineContainer spline;
    DragonSpawnerNEW spawner;
    public GameObject playerHolder;
    public GameObject canvas;

    [HideInInspector] public PowerUpSystem powerUPsys;
    [HideInInspector] public PauseMenu pausemenuScrip;
    
    [Header("Dragon Body Parts")]
    public GameObject dragonHead;
    public GameObject dragonBody;
    public GameObject dragonTail;
    public bool spawnVars;
    public int varEveryn = 5;
    public GameObject[] dragonTailVariations;
    
    [Header("Body Part Length")]
    public float tailPartLength;
    public float bodyPartLength;
    public float headPartLength;

    

    [Header("Dragon Parts")]
    public float dragonSpeed;
    public float dragonFastSpeed;
    public float dragonFastPercentage;
    public float backTime;

    [Header("EndGame")]
    public float endPercentage;

    [Header("Health")]
    public float headHealth;
    public float bodyHealth;
    public float tailHealth;

    public int fullAlphaAmount;
    public int alphaGradientAmount;
    

    [Header("Length")]
    public int tailLength;
    [HideInInspector] public int tailLengthPermenent;
    private float dragonDistance = 0;

    [Header("GameObjects")]
    public GameObject[] tailGO;
    public float _t = 0;
    public float loseProtect = 5f;
    public float loseProtectTime;
    public bool isPaused = false;

    private void Start()
    {
        tailGO = new GameObject[tailLength+1];
        spawner = gameObject.GetComponent<DragonSpawnerNEW>();
        powerUPsys = playerHolder.GetComponent<PowerUpSystem>();
        pausemenuScrip = canvas.GetComponent<PauseMenu>();
        tailLengthPermenent = tailLength;
    }
    private void Update()
    {
        _t += Time.deltaTime;
    }
    public void SpawnDragon()
    {
        GameObject tail =  Instantiate(dragonTail);
        tail.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, tailHealth, dragonFastSpeed, dragonFastPercentage, tailPartLength, backTime, spawner);
        tail.name = tailLength.ToString();
        dragonDistance += tailPartLength;
        tailGO[tailLength] = tail;
        if (spawnVars)
        {
            for (int i = tailLength - 1; i > 0; i--)
            {
                if(i % varEveryn == 0 && dragonTailVariations.Length != 0)
                {
                    int choice = UnityEngine.Random.Range(0, dragonTailVariations.Length);
                    GameObject body = Instantiate(dragonTailVariations[choice]);
                    body.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, bodyHealth, dragonFastSpeed, dragonFastPercentage, bodyPartLength, backTime, spawner);
                    body.name = i.ToString();
                    dragonDistance += bodyPartLength;
                    tailGO[i] = body;
                }
                else
                {
                    GameObject body = Instantiate(dragonBody);
                    body.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, bodyHealth, dragonFastSpeed, dragonFastPercentage, bodyPartLength, backTime, spawner);
                    body.name = i.ToString();
                    dragonDistance += bodyPartLength;
                    tailGO[i] = body;
                }
            }
        }
        else
        {
            for (int i = tailLength - 1; i > 0; i--)
            {
                GameObject body = Instantiate(dragonBody);
                body.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, bodyHealth, dragonFastSpeed, dragonFastPercentage, bodyPartLength, backTime, spawner);
                body.name = i.ToString();
                dragonDistance += bodyPartLength;
                tailGO[i] = body;
            }
        }
        
        GameObject head = Instantiate(dragonHead);
        head.GetComponent<DragonMovement>().setSpeedANDDistanceANDHealthANDFastspeed(dragonSpeed, dragonDistance, headHealth, dragonFastSpeed, dragonFastPercentage, headPartLength, backTime, spawner);
        head.GetComponent<DragonMovement>().ChangeTextAlpha(0);
        head.GetComponent<DragonMovement>().isHead = true;
        head.name = 0.ToString();
        tailGO[0] = head;
        dragonDistance += headPartLength;

        float reduceAlphaBy = 1f / (float)alphaGradientAmount;
        float InitialAlpha = ((float)fullAlphaAmount + (float)alphaGradientAmount) * reduceAlphaBy;
        
        for (int i = 1 ; i < tailLength+1; i++)
        {
            //Debug.Log(InitialAlpha);
            tailGO[i].GetComponent<DragonMovement>().ChangeTextAlpha(InitialAlpha);
            InitialAlpha -= reduceAlphaBy;
        }

    }

    public void WinCondition()
    {
        pausemenuScrip.WinScreen();
    }

    public void LoseCondition()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


        pausemenuScrip.LoseScreen();
    }
    public void MiniGameWin()
    {
        loseProtectTime = _t + loseProtect;
        DragonMovement movementHead = tailGO[1].GetComponent<DragonMovement>();
        float backDistance =  movementHead.distanceTraveled - movementHead.fastSpeedDistance;
        
        for (int i = 0; i < tailLengthPermenent + 1; i++)
        {
            try
            {
                DragonMovement movement = tailGO[i].GetComponent<DragonMovement>();
                movement.backDistance = movement.distanceTraveled - backDistance;
                movement.returnToStart = true;
                //Debug.Log($"{i}  " + $"{movement.distanceTraveled - backDistance}");
            }
            catch {  }

            // Calculate backDistance for smoother transition
            
            //movement.backDistance = distanceToFastSpeed;
        }
        StartCoroutine(PauseForNSeconds(2f));
    }

    public IEnumerator PauseForNSeconds(float waitTime)
    {
        isPaused = true;
        pausemenuScrip._joystickScript.isPaused = true;
        yield return new WaitForSeconds(waitTime);
        isPaused = false;
        pausemenuScrip._joystickScript.isPaused = false;
    }
}
