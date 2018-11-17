using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingShotScript : MonoBehaviour
{
    [System.NonSerialized]
    public float Speed;
    [System.NonSerialized]
    public Vector2 Direction;
    [System.NonSerialized]
    public GameObject Target;
    [System.NonSerialized]
    public float Range;
    [System.NonSerialized]
    public float Damage;

    public void BlowUp()
    {
        Pool.Instance.DeactivateObject(gameObject);
    }
}
