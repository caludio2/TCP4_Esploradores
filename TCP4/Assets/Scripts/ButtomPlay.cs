using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonController : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Rodoviaria");
    }
}

