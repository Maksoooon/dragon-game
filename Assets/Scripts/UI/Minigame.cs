using UnityEngine;

public class Minigame : MonoBehaviour
{
    public RectTransform buttonRectTransform;

    public float screenHeightHalf;
    public float screenWidthHalf;

    public float distBetweenPoints;
    public float speed;
    public float timeBetweenMovement; 
    public float floatDist;
    public Vector2 targetPosition; 
    public float _t;

    void Start()
    {
        screenHeightHalf = Screen.height * (0.4f);
        screenWidthHalf = Screen.width * (0.4f);


        NewPosition();
    }

    void Update()
    {
       
        _t += Time.deltaTime;

        if (_t < timeBetweenMovement)
        {
            buttonRectTransform.localPosition = Vector2.LerpUnclamped(buttonRectTransform.localPosition, targetPosition, speed * Time.deltaTime);
        }
        else
        {
            _t = 0;
            NewPosition();
        }
        
    }

    public void NewPosition()
    {

        Vector2 newtargetPosition = new Vector2(UnityEngine.Random.Range(-screenWidthHalf, screenWidthHalf), UnityEngine.Random.Range(-screenHeightHalf, screenHeightHalf));
        if (Vector2.Distance(targetPosition, newtargetPosition) < distBetweenPoints)
        {
            NewPosition();
        }
        targetPosition = newtargetPosition;


    }

    
   
}
