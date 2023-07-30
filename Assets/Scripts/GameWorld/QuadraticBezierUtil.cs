using Unity.Mathematics;

public static class QuadraticBezierUtil
{
    public static float3 GetPosition(float3 a, float3 b, float3 c, float t)
    {
        float oneMinusT = 1.0f - t;
        float tSqr = t * t;
        return a * oneMinusT * oneMinusT + b * 2.0f * oneMinusT * t + c * tSqr;
    }

    public static float3 GetTangent(float3 a, float3 b, float3 c, float t)
    {
        return math.normalize(a * (2.0f * t - 2.0f) + (2.0f * c - 4.0f * b) * t + 2.0f * b);
    }
}
