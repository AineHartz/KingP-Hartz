using System.Collections;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public void gotoGame()
    {
        StartCoroutine(WaitForSoundAndTransistion("MainGame"));
    }

    public void gotoMenu()
    {
        StartCoroutine(WaitForSoundAndTransistion("MainMenu"));
    }

    public void gotoPin()
    {
        StartCoroutine(WaitForSoundAndTransistion("CharecterSelection"));
    }

    private IEnumerator WaitForSoundAndTransistion(string sceneName)
    {
        AudioSource audio = GetComponentInChildren<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
