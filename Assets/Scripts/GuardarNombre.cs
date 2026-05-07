using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GuardarNombre : MonoBehaviour
{
    public TMP_InputField inputNombre;

    public void Guardar()
    {
        string nombreJugador = inputNombre.text;

        // Guardar nombre
        PlayerPrefs.SetString("NombreJugador", nombreJugador);
        PlayerPrefs.Save();

        // Ir a la escena de inicio
        SceneManager.LoadScene(0);
    }
}