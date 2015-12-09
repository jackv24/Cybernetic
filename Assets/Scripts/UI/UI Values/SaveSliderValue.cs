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

        slider.value = PlayerPrefs.GetFloat(valueName, slider.value);

        //When the value of this slider changes...
        slider.onValueChanged.AddListener(delegate { ValueChangedUpdate(); });
        ValueChangedUpdate();
    }

    public void ValueChangedUpdate()
    {
        //...save the value in playerprefs
        PlayerPrefs.SetFloat(valueName, slider.value);
    }
}
