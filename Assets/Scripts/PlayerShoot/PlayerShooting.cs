using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject dragonSpawnerGO;
    private DragonSpawnerNEW dragonSpawner;
    public GameObject arrow;
    public Transform orientation;

    [Header("Shoot Times Per Second")]
    public float shootingSpeed; //times per second
    private float currentShootingSpeed;

    [Header("Arrow properties")]
    public float arrowDamage;
    public float arrowSpeed;
    public float arrowMaxDistance;
    private float _t;
    private float timeWhenShot;

    [Header("PowerUp")]
    [Header("FasterShooting")]

    public bool fasterShooting = false;
    public float shootingMultiplier = 3;

    [Header("Arrow Penetrate")]
    
    public bool arrowPenetrate = false;
    public float arrowPenetrateAmount = 2;

    [Header("Triple Shot")]
    public bool tripleArrow = false;
    public float angleDeviation;
    

    [Header("Bomb Arrow")]
    public bool bombArrowBool = false;
    public float bombRadius;
    public float bombDamage;
    public float bombSpeed;

    [Header("Lightning Shot")]
    public bool lightningArrow = false;
    public float lightningDamage;
    public float percentageTraveled;
    public int lightningAmount;
    public GameObject lightningGO;

    private void Start()
    {
        dragonSpawner = dragonSpawnerGO.GetComponent<DragonSpawnerNEW>();
    }
    void Update()
    {
        if (!dragonSpawner.isPaused)
        {
            _t += Time.deltaTime;
            if (fasterShooting)
            {
                currentShootingSpeed = shootingSpeed * shootingMultiplier;
            }
            else
            {
                currentShootingSpeed = shootingSpeed;
            }
            if (_t > (timeWhenShot + (1 / currentShootingSpeed)))
            {
                timeWhenShot = _t;
                if (bombArrowBool)
                {
                    GameObject BombArrowGO = Instantiate(arrow, orientation.position, orientation.rotation);
                    Arrow arrowScript = BombArrowGO.GetComponent<Arrow>();
                    arrowScript.speed = bombSpeed;
                    arrowScript.penetrate = false;
                    arrowScript.maxDistance = arrowMaxDistance;
                    arrowScript.arrowPenetrateAmount = arrowPenetrateAmount;
                    arrowScript.damage = arrowDamage;
                    arrowScript.bombArrowBool = true;
                    arrowScript.bombRadius = bombRadius;
                    arrowScript.bombDamage = bombDamage;
                    arrowScript.dragonSpawner = dragonSpawner;

                }
                else if (tripleArrow)
                {
                    // Arrow 2
                    GameObject arrow2 = Instantiate(arrow, orientation.position, orientation.rotation);
                    Arrow arrowScript2 = arrow2.GetComponent<Arrow>();
                    arrowScript2.speed = arrowSpeed;
                    arrowScript2.penetrate = arrowPenetrate;
                    arrowScript2.maxDistance = arrowMaxDistance;
                    arrowScript2.arrowPenetrateAmount = arrowPenetrateAmount;
                    arrowScript2.damage = arrowDamage;
                    arrowScript2.dragonSpawner = dragonSpawner;
                    // Arrow 1
                    Quaternion rotation1 = orientation.rotation * Quaternion.Euler(0, angleDeviation, 0);
                    GameObject arrow1 = Instantiate(arrow, orientation.position, rotation1);
                    Arrow arrowScript1 = arrow1.GetComponent<Arrow>();
                    arrowScript1.speed = arrowSpeed;
                    arrowScript1.penetrate = arrowPenetrate;
                    arrowScript1.maxDistance = arrowMaxDistance;
                    arrowScript1.arrowPenetrateAmount = arrowPenetrateAmount;
                    arrowScript1.damage = arrowDamage;
                    arrowScript1.dragonSpawner = dragonSpawner;
                    // Arrow 3
                    Quaternion rotation2 = orientation.rotation * Quaternion.Euler(0, -angleDeviation, 0);
                    GameObject arrow3 = Instantiate(arrow, orientation.position, rotation2);
                    Arrow arrowScript3 = arrow3.GetComponent<Arrow>();
                    arrowScript3.speed = arrowSpeed;
                    arrowScript3.penetrate = arrowPenetrate;
                    arrowScript3.maxDistance = arrowMaxDistance;
                    arrowScript3.arrowPenetrateAmount = arrowPenetrateAmount;
                    arrowScript3.damage = arrowDamage;
                    arrowScript3.dragonSpawner = dragonSpawner;
                }
                else if (lightningArrow)
                {
                    GameObject arrowGO = Instantiate(arrow, orientation.position, orientation.rotation);
                    Arrow arrowScript = arrowGO.GetComponent<Arrow>();
                    arrowScript.speed = arrowSpeed;
                    arrowScript.penetrate = arrowPenetrate;
                    arrowScript.maxDistance = arrowMaxDistance;
                    arrowScript.arrowPenetrateAmount = arrowPenetrateAmount;
                    arrowScript.damage = arrowDamage;

                    arrowScript.lightningArrowbool = true;
                    arrowScript.dragonSpawner = dragonSpawner;
                    arrowScript.lightningDamage = lightningDamage;
                    arrowScript.lightningAmount = lightningAmount;
                    arrowScript.distanceTraveled = percentageTraveled;
                    arrowScript.lightningGO = lightningGO;
                    arrowScript.dragonSpawner = dragonSpawner;

                }
                else
                {
                    GameObject arrowGO = Instantiate(arrow, orientation.position, orientation.rotation);
                    Arrow arrowScript = arrowGO.GetComponent<Arrow>();
                    arrowScript.speed = arrowSpeed;
                    arrowScript.penetrate = arrowPenetrate;
                    arrowScript.maxDistance = arrowMaxDistance;
                    arrowScript.arrowPenetrateAmount = arrowPenetrateAmount;
                    arrowScript.damage = arrowDamage;
                    arrowScript.dragonSpawner = dragonSpawner;
                }

            }
        }
        

    }
}
