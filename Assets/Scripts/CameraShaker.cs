using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public float Speed;
    public float Amount;
    public float Decay;

    private float strength;
    private Vector3 originalPosition;

    private static CameraShaker cameraShakerInstance;
    public static CameraShaker Instance
    {
        get
        {
            if (cameraShakerInstance == null)
                cameraShakerInstance = FindObjectOfType<CameraShaker>();

            return cameraShakerInstance;
        }
    }

    void OnEnable ()
    {
        originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(strength < 0.001)
        {
            transform.position = originalPosition;
            return;
        }

        var a = Mathf.Sin(Speed*11*Time.realtimeSinceStartup + 1);
        var b = Mathf.Sin(Speed*17*Time.realtimeSinceStartup + 2);
        var c = Mathf.Sin(Speed*13*Time.realtimeSinceStartup + 3);
        var d = Mathf.Sin(Speed*19*Time.realtimeSinceStartup + 5);  

        var x = originalPosition.x + strength * (a + b);
        var y = originalPosition.y + strength * (c + d);

        transform.position = new Vector3(x, y, originalPosition.z);
        strength *= Decay;
    }

    public void Shake()
    {
        strength = Amount;
    }
}
