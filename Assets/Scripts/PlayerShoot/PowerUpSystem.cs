using UnityEngine;
using UnityEngine.UI;


public class PowerUpSystem : MonoBehaviour
{
    public GameObject player;
    public PlayerShooting _pShooting;
    public float powerTime = 5f;
    private float elapsedTime = 0f;
    public bool havePower = false;

    [Header("UI")]
    public GameObject SliderGroup;
    private CanvasGroup SliderCG;
    private Slider sliderLeft;
    private Slider sliderRight;

    /// <summary>
    /// 0 - nothing 
    /// 1 - Penetration
    /// 2 - Bomb
    /// 3 - 3x Shot speed
    /// 4 - Triple shot
    /// 5 - Lightning shot
    /// </summary>

    public enum PowerUpType
    {
        None,
        Penetration,
        Bomb,
        FastShot,
        TripleShot,
        LightningShot
    }


    [Header("")]
    public PowerUpType currentPowerUp = PowerUpType.None;
    void Start()
    {

        SliderCG = SliderGroup.GetComponent<CanvasGroup>();
        SliderCG.alpha = 0f;
        sliderLeft  = SliderGroup.transform.GetChild(0).GetComponent<Slider>();
        sliderRight = SliderGroup.transform.GetChild(1).GetComponent<Slider>();
        sliderLeft.maxValue  = powerTime;
        sliderRight.maxValue = powerTime;

        if (player != null)
            _pShooting = player.GetComponent<PlayerShooting>();
        else
            Debug.LogError("Player reference is not set in PowerUpSystem script.");
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        sliderLeft.value = powerTime - elapsedTime;
        sliderRight.value = powerTime - elapsedTime;
        
        if (elapsedTime >= powerTime)
        {
            DisableCurrentPowerUp();
        }
    }

    public void PowerUP(int power)
    {
        if (!havePower && (power != 0))
        {
            currentPowerUp = (PowerUpType)power;
            elapsedTime = 0;

            ApplyPowerUp();
        }
        
    }

    void ApplyPowerUp()
    {
        havePower = true;
        SliderCG.alpha = 1f;
        switch (currentPowerUp)
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Penetration:
                _pShooting.arrowPenetrate = true;
                break;
            case PowerUpType.Bomb:
                _pShooting.bombArrowBool = true;
                break;
            case PowerUpType.FastShot:
                _pShooting.fasterShooting = true;
                break;
            case PowerUpType.TripleShot:
                _pShooting.tripleArrow = true;
                break;
            case PowerUpType.LightningShot:
                _pShooting.lightningArrow = true;
                break;

                // Add more cases for other power-ups
        }
    }

    void DisableCurrentPowerUp()
    {
        havePower = false;
        SliderCG.alpha = 0f;
        switch (currentPowerUp)
        {
            case PowerUpType.None:
                break;
            case PowerUpType.Penetration:
                _pShooting.arrowPenetrate = false;
                break;
            case PowerUpType.Bomb:
                _pShooting.bombArrowBool = false;
                break;
            case PowerUpType.FastShot:
                _pShooting.fasterShooting = false;
                break;
            case PowerUpType.TripleShot:
                _pShooting.tripleArrow = false;
                break;
            case PowerUpType.LightningShot:
                _pShooting.lightningArrow = false;
                break;
                // Add cases for other power-ups if needed
        }

        currentPowerUp = PowerUpType.None;
    }
}
