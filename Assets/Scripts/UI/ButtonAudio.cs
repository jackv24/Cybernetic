using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlaySound("hover");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.instance.PlaySound("click");
    }
}
