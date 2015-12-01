using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveTextFieldValue : MonoBehaviour
{
    public string valueName;

    private InputField field;

    void Start()
    {
        field = GetComponent<InputField>();

        field.text = PlayerPrefs.GetString(valueName, field.text);

        //When the value of this text field changes...
        field.onValueChange.AddListener(delegate { ValueChangedUpdate(); });
        ValueChangedUpdate();
    }

    public void ValueChangedUpdate()
    {
        //...save the value in playerprefs
        PlayerPrefs.SetString(valueName, field.text);
    }
}
