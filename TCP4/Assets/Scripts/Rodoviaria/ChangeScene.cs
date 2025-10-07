using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void Rodoviaria()
    {
        SceneManager.LoadScene("Rodoviaria");
    }
    public void Trilha()
    {
        print("trocou de cena efetivamente");
        SceneManager.LoadScene("Trilha");
    }
    public void Foto()
    {
        SceneManager.LoadScene("Foto");
    }
    public void Pescaria()
    {
        SceneManager.LoadScene("Pescaria");
    }
}
