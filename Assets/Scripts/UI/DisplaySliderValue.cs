using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplaySliderValue : MonoBehaviour
{
    public Slider slider;

    private Text text;
    private string textString;

    void Start()
    {
        text = GetComponent<Text>();
        textString = text.text;

        slider.onValueChanged.AddListener(delegate { ValueChangedUpdate(); });
        ValueChangedUpdate();
    }

    public void ValueChangedUpdate()
    {
        text.text = string.Format(textString, slider.value);
    }
}
