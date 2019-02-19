using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class FlyingShotScript : MonoBehaviour
{
    [System.NonSerialized] public float Speed;
    [System.NonSerialized] public Vector2 Direction;
    [System.NonSerialized] public GameObject Target;
    [System.NonSerialized] public float Range;
    [System.NonSerialized] public float Damage;
    [System.NonSerialized] public List<string> EnemyTags;
    [System.NonSerialized] public Transform Turret;

    public virtual void BlowUp()
    {
        Pool.Instance.DeactivateObject(gameObject);
    }
}
