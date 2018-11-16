using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildLocationScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [HideInInspector]
    public static GameObject OpenCanvas;
    private GameObject canvas;
    private GameObject turret;
    private bool pressed = false;

    void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        canvas.SetActive(false);
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(pressed) return;

        if (OpenCanvas != null) OpenCanvas.SetActive(false);

        gameObject.transform.Translate(0, -3f, 0);
        pressed = true;
    }
    
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!pressed) return;

        gameObject.transform.Translate(0, 3f, 0);
        pressed = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!pressed) return;

        gameObject.transform.Translate(0, 3f, 0);
        canvas.SetActive(true);
        OpenCanvas = canvas;
    }
}
