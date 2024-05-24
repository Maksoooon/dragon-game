using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [SerializeField]
    private Transform[] routes;

    private int routeToGo;
    private float tParam;
    private Vector3 DragonPosition;
    
    [SerializeField] private float speedModifier;
    private bool coroutineAllowed;

    void Start()
    {
        routeToGo = 0;
        tParam = 0;
        
        coroutineAllowed = true;
    }
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNumber].GetChild(0).position;
        Vector3 p1 = routes[routeNumber].GetChild(1).position;
        Vector3 p2 = routes[routeNumber].GetChild(2).position;
        Vector3 p3 = routes[routeNumber].GetChild(3).position;

        float curveLength = CalculateBezierCurveLength(p0, p1, p2, p3);

        float speed = speedModifier / curveLength;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speed;

            DragonPosition = Mathf.Pow(1 - tParam, 3) * p0 +
                3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 +
                3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 +
                Mathf.Pow(tParam, 3) * p3;

            // Move the dragon to the calculated position
            transform.position = DragonPosition;

            // Calculate direction towards the next position
            Vector3 direction = Mathf.Pow(1 - tParam, 2) * (p1 - p0) +
                                2 * (1 - tParam) * tParam * (p2 - p1) +
                                Mathf.Pow(tParam, 2) * (p3 - p2);

            // Rotate the dragon to face the direction it's moving towards
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo++;
        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }
        coroutineAllowed = true;
    }
    private float CalculateBezierCurveLength(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float length = 0f;
        Vector3 lastPoint = BezierCurve(p0, p1, p2, p3, 0f);

        for (float t = 0.1f; t <= 1f; t += 0.1f)
        {
            Vector3 nextPoint = BezierCurve(p0, p1, p2, p3, t);
            length += Vector3.Distance(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }

        return length;
    }
    private Vector3 BezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}

