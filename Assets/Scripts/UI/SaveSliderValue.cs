using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveSliderValue : MonoBehaviour
{
    public string valueName;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        //When the value of this slider changes...
        slider.onValueChanged.AddListener(delegate { ValueChangedUpdate(); });
        ValueChangedUpdate();
    }

    public void ValueChangedUpdate()
    {
        //...save the value in playerprefs
        PlayerPrefs.SetInt(valueName, (int)slider.value);
    }
}
