using UnityEngine;

namespace Foto
{
    public class CameraObjectTetection : MonoBehaviour
    {
        public float detectionRange = 20f;
        public LayerMask detectableLayers;

        public void DetectObjectsInView()
        {
            // Encontra todos os objetos em um raio da c�mera
            Collider[] hits = Physics.OverlapSphere(transform.position, Mathf.Infinity, detectableLayers);
            foreach (Collider hit in hits)
            {
                Vector3 viewportPos = Camera.main.WorldToViewportPoint(hit.transform.position);
                if (viewportPos.z > 0 && viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
                {
                    Debug.Log($"Objeto vis�vel: {hit.gameObject.name}");
                    // Aqui voc� pode adicionar l�gica adicional, como reagir ao objeto
                }
            }
        }
    }
}

