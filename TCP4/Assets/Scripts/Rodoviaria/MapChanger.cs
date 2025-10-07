using UnityEngine;

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
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Ended)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
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
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // Set the gizmo color
        Gizmos.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 100); // Draw a sphere at the
    }
}
