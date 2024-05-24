using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [Header("Count Down Time")]
    public float timeToCount;
    public float _t;
    public TextMeshProUGUI _textMeshPro;
    private DragonSpawnerNEW _spawnDragon;
    private GameObject _statingPoint;
    bool spawn = true;

    // Start is called before the first frame update
    void Start()
    {
        _statingPoint = GameObject.Find("DragonSpawner");
        _spawnDragon = _statingPoint.GetComponent<DragonSpawnerNEW>();
        _spawnDragon.isPaused = true;
        _t = timeToCount;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _t -= Time.deltaTime;
        
        if (_t < 0 && spawn)
        {
            spawn = false;
            _spawnDragon.SpawnDragon();
            _textMeshPro.gameObject.SetActive(false);
            _spawnDragon.isPaused = false;
        }
        else
        {
            _textMeshPro.SetText(Convert.ToString((int)_t));
        }

    }
}
