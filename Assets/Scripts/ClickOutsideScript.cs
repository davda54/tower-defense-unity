using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutsideScript : MonoBehaviour, IPointerDownHandler {
    public void OnPointerDown(PointerEventData eventData)
    {
        if(BuildLocationScript.OpenCanvas != null)
        {
            BuildLocationScript.OpenCanvas.SetActive(false);
        }
    }
}
