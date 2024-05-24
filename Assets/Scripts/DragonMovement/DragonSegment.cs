using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class DragonSegment : MonoBehaviour
{
    public GameObject dragonParent;
    public DragonMovement _dragonMovement;
    public SplineContainer spline;

    public float distanceTraveled;
    public float distancePercentage;
    public float splineLength;
    public float multi;
    void Start()
    {
        
        //dragonParent = this.gameObject.transform.parent.gameObject.transform.parent.gameObject;
        _dragonMovement = dragonParent.GetComponent<DragonMovement>();
        spline = _dragonMovement.spline;
        switch (transform.GetSiblingIndex())
        {
            case 0:
                multi = 4;
                break;
            case 1:
                multi = 3;
                break;
            case 2:
                multi = 2;
                break;
            case 3:
                multi = 1;
                break;
            case 4:
                multi = 0;
                break;
            case 5:
                multi = -1;
                break;
            case 6:
                multi = -2;
                break;
            case 7:
                multi = -3;
                break;
            default:
                break;
        }
        splineLength = _dragonMovement.splineLength;
    }


    void Update()
    {

        distanceTraveled = _dragonMovement.distanceTraveled + 0.24f * multi;
        distancePercentage = distanceTraveled / splineLength;
        Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
        transform.position = currentPosition;


        //look direction
        Vector3 nextPosition = spline.EvaluatePosition(distancePercentage + 0.001f);
        Vector3 direction = nextPosition - currentPosition;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
