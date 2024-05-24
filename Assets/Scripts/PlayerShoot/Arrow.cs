using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public float maxDistance;
    public float distance;
    public float _t;
    public float killTimeOut;
    public float killTime;
    public bool penetrate;
    public float arrowPenetrateAmount;
    public float PenetrateAmount = 0;
    public bool killing = true;
    public float damage;
    public bool bombArrowBool = false;
    public float bombDamage;
    public float bombRadius;
    public GameObject bombSphere;


    public bool lightningArrowbool = false;
    public GameObject lightningGO;
    public float lightningTime = 0.5f;
    public float lightningDamage;
    public int lightningAmount;
    public DragonSpawnerNEW dragonSpawner;
    public float distanceTraveled;

    private void Start()
    {
        killTimeOut = 0.02f;
    }
    private void Update()
    {
        if (!dragonSpawner.isPaused)
        {
            _t += Time.deltaTime;
            transform.position += speed * Time.deltaTime * transform.forward;
            distance += speed * Time.deltaTime;

            if (distance > maxDistance)
            {
                Destroy(gameObject);
            }
            if (PenetrateAmount == arrowPenetrateAmount)
            {
                Destroy(gameObject);
            }
            if (_t > killTime + killTimeOut)
            {
                killing = true;
            }
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dragon") && killing)
        {
            killing = false;
            killTime = _t;
            other.gameObject.GetComponent<DragonMovement>().dragonHit(damage);
            if (!penetrate)
            {
                Destroy(gameObject);
            }
            else
            {
                PenetrateAmount++;
            }
        }
    }
    private void OnDestroy()
    {
        if (bombArrowBool)
        {
            GameObject explosionSphere = Instantiate(bombSphere, transform.position, transform.rotation);
            explosionSphere.transform.localScale = new Vector3(bombRadius * 2, bombRadius * 2, bombRadius * 2);
            Destroy(explosionSphere, 1.0f);
            Collider[] colliders = Physics.OverlapSphere(transform.position, bombRadius);
            foreach (Collider hitCollider in colliders)
            {
                if (hitCollider.CompareTag("Dragon"))
                {
                    hitCollider.gameObject.GetComponent<DragonMovement>().dragonHit(bombDamage);
                    
                }
            }
        }
        if (lightningArrowbool)
        {
            int[] lightningStrikes = new int[lightningAmount]; // Initialize the array

            for (int i = 0; i < lightningAmount; i++)
            {
                float tempPercent = 0;
                while (tempPercent < distanceTraveled)
                {
                    try
                    {
                        int tempRandom = Random.Range(0, dragonSpawner.tailGO.Length);
                        tempPercent = dragonSpawner.tailGO[tempRandom].gameObject.GetComponent<DragonMovement>().distancePercentage;
                        lightningStrikes[i] = tempRandom;
                    }
                    catch
                    {

                    }
                    
                    
                }
            }
            for (int i = 0;i < lightningStrikes.Length; i++)
            {
                DragonMovement tempDragMove = dragonSpawner.tailGO[lightningStrikes[i]].gameObject.GetComponent<DragonMovement>();
                tempDragMove.dragonHit(lightningDamage);
                GameObject LightningStrikeGOTemp = Instantiate(lightningGO, tempDragMove.transform.position, Quaternion.identity);
                Destroy(LightningStrikeGOTemp, lightningTime);
            }
        }
    }
}
