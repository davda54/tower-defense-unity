using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildLocationScript : MonoBehaviour
{
    public GameObject Turret;

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
        Instantiate(Turret, transform.position, Quaternion.identity);
        used = true;

        GetComponent<SpriteRenderer>().enabled = false;
    }
}
