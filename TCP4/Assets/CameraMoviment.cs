using UnityEngine;

public class CameraMoviment : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Input.location.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Input.gyro.rotationRate);
    }
}
