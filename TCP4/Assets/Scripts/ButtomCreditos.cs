using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControllerCreditos : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Creditos");
    }
}

