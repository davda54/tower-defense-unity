using Assets.Scripts;
using UnityEngine;


// uses algorithm from https://gamedev.stackexchange.com/questions/27056/how-to-achieve-uniform-speed-of-movement-on-a-bezier-curve
public class PathFollower : MonoBehaviour
{

    public GameObject Path;
    public float Speed;

    private Path path;
    private int segmentIndex;

    private Vector2 A, B, C, D;
    private Vector2 v1, v2, v3;
    private float t;

	void Start ()
    {
        path = Path.GetComponent<PathCreator>().path;
        segmentIndex = 0;

        RecomputeSegment();
        transform.position = A;
	}
	
	void FixedUpdate ()
    {
        if (segmentIndex >= path.NumSegments) return;

        if(t >= 1.0f)
        {
            segmentIndex++;
            if (segmentIndex >= path.NumSegments) return;

            RecomputeSegment();
        }

        var tangent = t * t * v1 + t * v2 + v3;
        t += Time.deltaTime*Speed / tangent.magnitude;

        transform.position = Bezier.EvaluateCubic(A, B, C, D, t);
        transform.eulerAngles = new Vector3(0, 0, MathHelpers.Angle(tangent, Vector2.right));
    }

    private void RecomputeSegment()
    {
        var segment = path.GetPointsInSegment(segmentIndex);

        A = segment[0];
        B = segment[1];
        C = segment[2];
        D = segment[3];

        v1 = -3 * A + 9 * B - 9 * C + 3 * D;
        v2 = 6 * A - 12 * B + 6 * C;
        v3 = -3 * A + 3 * B;

        t = 0;
    }
}
