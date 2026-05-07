using UnityEngine;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public void CambiarEscena()
    {
        SceneManager.LoadScene("Options");
    }
}