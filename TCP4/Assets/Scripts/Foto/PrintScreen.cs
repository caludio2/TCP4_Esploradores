using UnityEngine;

public class PrintScreen : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    float fovCam;
    [SerializeField]
    float fovNormal;

    [SerializeField]
    GameObject canvas;
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            cam.fieldOfView = fovCam;
            canvas.SetActive(true);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            cam.fieldOfView = fovNormal;

            canvas.SetActive(false);

            string fileName = "Screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            print(fileName);
            ScreenCapture.CaptureScreenshot(fileName);
        }
    }
}
