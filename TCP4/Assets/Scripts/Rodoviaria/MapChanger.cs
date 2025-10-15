using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class MapChanger : MonoBehaviour
{
    [SerializeField]
    string trilha;
    [SerializeField]
    string foto;
    [SerializeField]
    string pescaria;

    [SerializeField]
    ChangeScene changeScene;

    public Camera _cam;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            UnityEngine.Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    print(hit.collider.gameObject.name);
                    Debug.Log("Mouse hit: " + hit.collider.name);
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(trilha))
                    {
                        print("trocou de cena");
                        changeScene.Trilha();
                    }
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(foto))
                    {
                        changeScene.Foto();
                    }
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer(pescaria))
                    {
                        changeScene.Pescaria();
                    }
                }
            }
        }
    }
}
