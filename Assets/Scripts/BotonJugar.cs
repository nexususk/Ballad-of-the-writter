using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonJugar : MonoBehaviour
{
    public void IrAlJuego()
    {
        SceneManager.LoadScene(1);
    }
}