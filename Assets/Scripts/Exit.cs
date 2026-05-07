using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void CambiarEscena()
    {
        SceneManager.LoadScene("Exit");
    }
}