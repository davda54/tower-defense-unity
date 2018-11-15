using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLocationScript : MonoBehaviour
{
    public GameObject TurretPrefab;

    private GameObject turret;
    private bool pressed = false;
    private bool used = false;

    void OnMouseDown()
    {
        if(pressed || used) return;

        gameObject.transform.Translate(0, -3f, 0);
        pressed = true;
    }

    private void OnMouseExit()
    {
        if (!pressed || used) return;

        gameObject.transform.Translate(0, 3f, 0);
        pressed = false;
    }

    void OnMouseUp()
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
