using Assets.Scripts;
using UnityEngine;


// uses algorithm from https://gamedev.stackexchange.com/questions/27056/how-to-achieve-uniform-speed-of-movement-on-a-bezier-curve
public class PathFollower : MonoBehaviour
{
    public enum PathType { Ground, Air }

    public PathType Type;
    public float Speed;
    public float OffsetAmount;

    private Vector2 PositionOffset;
    private Path path;
    private int segmentIndex;

    private Vector2 A, B, C, D;
    private Vector2 v1, v2, v3;
    private float t;

	void OnEnable ()
    {
        switch(Type)
        {
            case PathType.Ground: path = GameObject.Find("GroundPath").GetComponent<PathCreator>().path; break;
            case PathType.Air: path = GameObject.Find("AirPath").GetComponent<PathCreator>().path; break;
        }

        segmentIndex = 0;
        PositionOffset = Random.insideUnitCircle * Random.Range(-OffsetAmount, OffsetAmount);

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
        t = t + Time.deltaTime * Speed / tangent.magnitude;

        transform.position = Bezier.EvaluateCubic(A, B, C, D, t);
        transform.eulerAngles = new Vector3(0.0f, 0.0f, MathHelpers.Angle(tangent, Vector2.right));

        if (gameObject.layer == LayerMask.NameToLayer("enemy"))
            EnemyManagerScript.Instance.UpdateEnemy(gameObject, path.NumSegments - segmentIndex - t);
    }

    private void RecomputeSegment()
    {
        var segment = path.GetPointsInSegment(segmentIndex);

        A = segment[0] + PositionOffset;
        B = segment[1] + PositionOffset;
        C = segment[2] + PositionOffset;
        D = segment[3] + PositionOffset;

        v1 = -3 * A + 9 * B - 9 * C + 3 * D;
        v2 = 6 * A - 12 * B + 6 * C;
        v3 = -3 * A + 3 * B;

        t = 0;
    }
}
