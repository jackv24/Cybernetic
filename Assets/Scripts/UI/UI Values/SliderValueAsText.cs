using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderValueAsText : MonoBehaviour
{
    public Text text;
    private string textString;

    public string[] values;

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        textString = text.text;

        slider.onValueChanged.AddListener(delegate { ValueChangedUpdate(); });
        ValueChangedUpdate();
    }

    void ValueChangedUpdate()
    {
        text.text = string.Format(textString, values[(int)slider.value]);
    }
}
