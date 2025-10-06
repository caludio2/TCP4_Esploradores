using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Rodoviaria()
    {
        SceneManager.LoadScene("Rodoviaria", LoadSceneMode.Additive);
    }
    public void Trilha()
    {
        SceneManager.LoadScene("Trilha", LoadSceneMode.Additive);
    }
    public void Foto()
    {
        SceneManager.LoadScene("Foto", LoadSceneMode.Additive);
    }
    public void Pescaria()
    {
        SceneManager.LoadScene("Pescaria", LoadSceneMode.Additive);
    }
}
