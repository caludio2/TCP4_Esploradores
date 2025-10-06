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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer(trilha))
                {
                    changeScene.Trilha();
                }
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer(foto))
                {
                    changeScene.Foto();
                }
                if(hit.collider.gameObject.layer == LayerMask.NameToLayer(pescaria))
                {
                    changeScene.Pescaria();
                }
            }      
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow; // Set the gizmo color
        Gizmos.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * Mathf.Infinity); // Draw a sphere at the
    }
}
