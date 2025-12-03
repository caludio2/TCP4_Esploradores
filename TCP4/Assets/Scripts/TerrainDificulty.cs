using UnityEngine;
using UnityEngine.Splines;

public class TerrainDificulty : MonoBehaviour
{
    public SplineContainer spline;
    public float t1 = 0f; // posição normalizada do primeiro ponto (0 a 1)
    public float t2 = 1f; // posição do segundo ponto

    public SplineScript script;

    void Start()
    {
        Vector3 p1 = spline.EvaluatePosition(t1); // ponto inicial
        Vector3 p2 = spline.EvaluatePosition(t2); // ponto final

        float diferencaAltura = p2.y - p1.y;

        script.altura = Mathf.Max(0f, 0.1f - diferencaAltura);
    }
}
