using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DragonHeadGameCheacker : MonoBehaviour
{
    public DragonMovement dragonMovementScript;
    public DragonSpawnerNEW dragonSpawnerScript;
    public float endGame;

    void Start()
    {
        dragonMovementScript = gameObject.GetComponent<DragonMovement>();
        dragonSpawnerScript = dragonMovementScript.dragonSpawnerScript;
        endGame = dragonSpawnerScript.endPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        if (dragonMovementScript.distancePercentage > endGame & dragonSpawnerScript._t > dragonSpawnerScript.loseProtectTime)
        {
            dragonSpawnerScript.LoseCondition();
            
        }
    }
   
}
