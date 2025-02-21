using UnityEngine;

public class SelectionScript : MonoBehaviour
{
    public void gotoMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
