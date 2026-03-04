using UnityEngine;
using TMPro;
using System.Collections;
public class Diccionario : MonoBehaviour
{
    public TextAsset dic;
    public string[] arregloDic;
    public string palabraActual;
    public string letra;
    public TextMeshProUGUI textoUi;
    public GameObject wrong;
    public bool iswrong;

    void Start()
    {
        arregloDic = dic.text.Split(new char[] { ' ' });
        wrong.SetActive(false);
        iswrong = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(ObtenerLetraPresionada() != null){
            letra = ObtenerLetraPresionada();
            if(letra.Equals(palabraActual.Substring(0, 1).ToUpper()))
            {
                palabraActual = palabraActual.Substring(1);
            }
            else if (iswrong == false)
            {
                StartCoroutine("Malo");
            }
                
        }
        textoUi.text = palabraActual;

    }
    public void AsignarPalabra(int cual)
    {
        palabraActual = arregloDic[cual];
    }

    [ContextMenu("Generar palabra al azar")]
    public void ElegirPalabraAzar()
    {
        AsignarPalabra(Random.Range(0,arregloDic.Length));
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
}
