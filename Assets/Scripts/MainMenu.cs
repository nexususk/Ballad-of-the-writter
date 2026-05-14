using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    


    public void Juego()
    {
        SceneManager.LoadScene("Game");
        FindAnyObjectByType<AudioManager>().Play("Button");
    }

    public void Ajustes()
    {
        FindAnyObjectByType<AudioManager>().Play("Button");
    }
    public void Quit()
    {
        FindAnyObjectByType<AudioManager>().Play("Button");
    }
}
