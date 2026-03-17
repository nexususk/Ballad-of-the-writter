using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
public class Diccionario : MonoBehaviour
{
    public TextAsset        dic;
    public string[]         arregloDic;
    public string           palabraActual;
    public string           letra;
    public TextMeshProUGUI  textoUi;
    public GameObject       wrong;
    public bool             iswrong;
    public float            tempo;
    public Estado           estado;
    public int              error;
    public int              tamPalabra;
    public float            puntaje;
    public float            pFinal;
    

   
    void Start()
    {
        arregloDic = dic.text.Split(new char[] { ' ' });
        wrong.SetActive(false);
        iswrong = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ObtenerLetraPresionada() != null && estado == Estado.juego){
            letra = ObtenerLetraPresionada();
            if(letra.Equals(palabraActual.Substring(0, 1).ToUpper()))
            {
                palabraActual = palabraActual.Substring(1);
                if(palabraActual.Length < 1)
                {
                    PalabraF();
                }
            }
            else if (iswrong == false)
            {
                StartCoroutine("Malo");
                error++;
            }
                
        }
        textoUi.text = palabraActual;

        if (estado==Estado.juego)
        {
            tempo += Time.deltaTime;
            puntaje = (tamPalabra - error)/(1+tempo);
        }
    }
    public void AsignarPalabra(int cual)
    {
        palabraActual = arregloDic[cual];

    }

    [ContextMenu("Generar palabra al azar")]
    public void ElegirPalabraAzar()
    {
        AsignarPalabra(Random.Range(0,arregloDic.Length));
        estado = Estado.juego;
        tempo = 0;
        error = 0;
        tamPalabra = palabraActual.Length;
        puntaje = 0;
    }
    public string ObtenerLetraPresionada()
    {
        if (Input.inputString.Length > 0)
        {
            char c = Input.inputString[0];

            // Verifica si es letra (incluye ñ automáticamente)
            if (char.IsLetter(c))
            {
                return char.ToUpper(c).ToString();
            }
        }

        return null;
    }
    public IEnumerator Malo()
    {
        wrong.SetActive(true);
        iswrong = true;
        yield return new WaitForSeconds(1f);
        wrong.SetActive(false);
        iswrong = false;
    }
    public void PalabraF()
    {
        estado= Estado.espera;
        DatosPuntaje datosPuntaje = new DatosPuntaje();
        datosPuntaje.puntaje = puntaje;
        datosPuntaje.error = error;
        datosPuntaje.tiempo = tempo;
        Debug.Log(JsonUtility.ToJson(datosPuntaje));

    }

    public void Ganador()
    {
        pFinal = puntaje - error;
    }
}


public enum Estado
{
    espera = 0, 
    juego = 1,
    cinematica =2
}

[System.Serializable]
public class DatosPuntaje
{
    public float puntaje;
    public int error;
    public float tiempo;

}