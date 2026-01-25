using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonControllerVoltar : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Menu");
    }
}

