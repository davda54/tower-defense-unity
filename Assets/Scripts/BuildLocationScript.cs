using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildLocationScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject TurretPrefab;

    private GameObject turret;
    private bool pressed = false;
    private bool used = false;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if(pressed || used) return;

        gameObject.transform.Translate(0, -3f, 0);
        pressed = true;
    }
    
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!pressed || used) return;

        gameObject.transform.Translate(0, 3f, 0);
        pressed = false;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!pressed || used) return;

        gameObject.transform.Translate(0, 3f, 0);

        int cost = TurretPrefab.GetComponent<TurretScript>().Cost;
        if (GameManager.Instance.GetMoney() >= cost)
        {
            turret = Instantiate(TurretPrefab, transform.position, Quaternion.identity);
            GameManager.Instance.TurretBuilt(turret);
            used = true;

            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
