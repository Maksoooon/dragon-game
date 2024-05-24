using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool GameisPaused = false;

    [Header("GameObjects")]
    public GameObject pauseButtonGO;
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public GameObject joystickGO;
    public GameObject goBackButtonGO;
    public GameObject dragonSpawner;
    public GameObject playerHolder;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject miniGameHolder;

    //[Header("Scripts (Don't change)")]
    [HideInInspector] public JoyStickSimple _joystickScript;
    [HideInInspector] public DragonSpawnerNEW _dragonSpawnerScript;
    [HideInInspector] public PlayerAimRightLeft _playerAimRightLeft;
    [HideInInspector] public Minigame _miniGameScript;
    [HideInInspector] public CameraToggle _cameraToggle;


    [Header("UI GameObjects")]
    public GameObject XMult;
    public GameObject YMult;
    public GameObject YToggle;
    public GameObject MuteToggle;

    [Header("UI Components")]
    public Slider XMultSlider;
    public Slider YMultSlider;
    public Toggle YToggleToggle;
    public Toggle MuteToggleToggle;

    private float defaultSoundVolume = 0.2f;
    private float volume;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
        miniGameHolder.gameObject.SetActive(false);
        _dragonSpawnerScript = dragonSpawner.GetComponent<DragonSpawnerNEW>();
        _joystickScript = joystickGO.GetComponent<JoyStickSimple>();
        _playerAimRightLeft = playerHolder.GetComponent<PlayerAimRightLeft>();
        _miniGameScript = gameObject.GetComponent<Minigame>();
        _cameraToggle = gameObject.GetComponent<CameraToggle>();
        XMultSlider = XMult.GetComponent<Slider>();
        YMultSlider = YMult.GetComponent<Slider>();
        YToggleToggle = YToggle.GetComponent<Toggle>();
        MuteToggleToggle = MuteToggle.GetComponent<Toggle>();
        _miniGameScript = miniGameHolder.transform.GetChild(0).GetComponent<Minigame>();
        GetSettings();
    }

    public void Pause()
    {
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(true);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
        SetSettings();
    }

    public void OpenSettings()
    {
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(true);
        goBackButtonGO.gameObject.SetActive(true);
    }

    public void Resume()
    {
        GameisPaused = false;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(true);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
        miniGameHolder.gameObject.SetActive(false);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
        GetSettings();
    }

    public void WinScreen()
    {
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(true);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
    }
    public void LoseScreen()
    {
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
        _dragonSpawnerScript.isPaused = GameisPaused;
        _joystickScript.isPaused = GameisPaused;
    }
    public void StartMiniGame()
    {
        _cameraToggle.CameraToggler();
        _dragonSpawnerScript.loseProtectTime = 10000000;
        GameisPaused = true;
        pauseMenuUI.gameObject.SetActive(false);
        settingsMenuUI.gameObject.SetActive(false);
        pauseButtonGO.gameObject.SetActive(false);
        goBackButtonGO.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(false);
        miniGameHolder.gameObject.SetActive(true);
        
        //_miniGameScript.MoveButton();
    }
    public void MuteToggler(Toggle toggle)
    {
        if (toggle.isOn)
        {
            AudioListener.volume = defaultSoundVolume;
            volume = AudioListener.volume;
        }
        else
        {
            AudioListener.volume = 0;
            volume = AudioListener.volume;
        }
    }
    public void SetSettings()
    {
        PlayerPrefs.SetFloat("XMult", XMultSlider.value);
        PlayerPrefs.SetFloat("YMult", YMultSlider.value);
        PlayerPrefs.SetFloat("YInvert", YToggleToggle.isOn ? 1 : 0);
        PlayerPrefs.SetFloat("Mute", volume);
        PlayerPrefs.Save();
    }

    public void GetSettings()
    {
        XMultSlider.value = PlayerPrefs.GetFloat("XMult", 0.5f);
        YMultSlider.value = PlayerPrefs.GetFloat("YMult", 0.25f);
        YToggleToggle.isOn = PlayerPrefs.GetFloat("YInvert") == 1 ? true : false;
        volume = PlayerPrefs.GetFloat("Mute", defaultSoundVolume);
        _playerAimRightLeft.multiplierX = XMultSlider.value;
        _playerAimRightLeft.multiplierY = YMultSlider.value;
        _playerAimRightLeft.invertY = YToggleToggle.isOn;
        MuteToggleToggle.isOn = (volume == defaultSoundVolume) ? true : false;
        AudioListener.volume = volume;
    }



}
