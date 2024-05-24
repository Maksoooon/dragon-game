using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class TextSliderOnChanged : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider.value.ToString("0.00");
    }
    
}
