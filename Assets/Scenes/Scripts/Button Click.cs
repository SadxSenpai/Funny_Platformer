using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image Unpressed_Image;
    [SerializeField] private Image Pressed_Image;

    public void OnPointerDown(PointerEventData eventData)
    {
        Unpressed_Image.sprite = Pressed_Image.sprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed_Image.sprite = Unpressed_Image.sprite;
    }
}