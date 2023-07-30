using UnityEngine;
using Unity.Mathematics;

public class BezierCurveTest : MonoBehaviour
{
    public Transform StartTrans;
    public Transform ControlTrans;
    public Transform EndTrans;

    public Transform TargetTrans;
    public Transform TargetTrans1;

    [Range(0.0f, 2.0f)] public float Time;

    private void Start()
    {
        
    }

    private void Update()
    {
        float3 a = this.StartTrans.position;
        float3 b = this.ControlTrans.position;
        float3 c = this.EndTrans.position;

        // this.TargetTrans.position = math.lerp(math.lerp(a, b, this.Time), math.lerp(b, c, this.Time), this.Time);
        this.TargetTrans.position = QuadraticBezierUtil.GetPosition(a, b, c, this.Time);
        this.TargetTrans1.position = (float3)this.TargetTrans.position + QuadraticBezierUtil.GetTangent(a, b, c, this.Time);
    }
}
