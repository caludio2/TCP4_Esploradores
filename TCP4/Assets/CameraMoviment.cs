using UnityEngine;

public class CameraMoviment : MonoBehaviour
{
    public float maxVariation = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotationRate = Input.gyro.rotationRate;
        transform.Rotate(0, -rotationRate.y * Mathf.Rad2Deg / 15.25f, 0);
        if(Input.acceleration.x > maxVariation)
        {
            transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        }
    }
}
