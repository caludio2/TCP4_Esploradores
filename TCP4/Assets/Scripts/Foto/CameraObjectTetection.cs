using UnityEngine;

namespace Foto
{
    public class CameraObjectTetection : MonoBehaviour
    {
        public float detectionRange = 20f;
        public string detectableLayers;

        public int pointAmount;

        private int points;
        public void DetectObjectsInView()
        {
            // Encontra todos os objetos em um raio da câmera
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRange);
            foreach (Collider hit in hits)
            {
                if (hit.gameObject.tag == detectableLayers)
                {

                    Vector3 viewportPos = Camera.main.WorldToViewportPoint(hit.transform.position);
                    if (viewportPos.z > 0 && viewportPos.x >= 0 && viewportPos.x <= 1 && viewportPos.y >= 0 && viewportPos.y <= 1)
                    {
                        Debug.Log($"Objeto visível: {hit.gameObject.name}");
                        if (hit.gameObject.GetComponent<InscanciadorDeAnimais>().isSpawned == true)
                        {
                            points += pointAmount;
                            PointManager.Instance.SetPoints("fotoPoints", points);
                            Destroy(hit.gameObject);
                        }
                    }
                }
            }
        }
    }
}

