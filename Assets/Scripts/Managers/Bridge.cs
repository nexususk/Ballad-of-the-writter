using UnityEngine;
using Photon.Chat.DemoChat;
using TMPro;

public class Bridge : MonoBehaviour
{
    public string[] mensaje;
    public PalabraPonderada p;
    public Diccionario diccionario;

    public PalabraPonderada p1;
    public PalabraPonderada p2;

    DatosPuntaje miPuntaje;
    DatosPuntaje datoEnemigo;
    int datoObtenido = 0;

   
 

    void Start()
    {
        //ChatNewGui.onMessage += Mensajero;
        p.Ponderar();
        print(JsonUtility.ToJson(p));
    }
    public void Mensajero(string msj)
    {
        mensaje = msj.Split('|');
        if (mensaje.Length == 3)
        {
            switch (mensaje[1])
            {
                case "0":
                    AsignarPalabra(mensaje[2]);
                    break;
                case "1":
                    Comparar(mensaje[2]);
                    break;
                case "2":
                    LlegoPalabraPuntaje(mensaje[2], mensaje[0]);
                    break;
                default:
                    break;
            }
        }
    }   
    
    public void AsignarPalabra(string palabra)
    {
        p = JsonUtility.FromJson<PalabraPonderada>(palabra);
        print("La palabra asignada es: " +  p.palabra);
    }
    public void LlegoPalabraPuntaje(string palabra, string nombre)
    {
        DatosPuntaje dp = JsonUtility.FromJson<DatosPuntaje>(palabra);
        if(nombre == GameManager.instance.chatGui.UserName)
        {
            miPuntaje = dp;
        }
        else
        {
            datoEnemigo = dp;
        }
        datoObtenido++;
        if(datoObtenido > 1 )
        {
            GameManager.instance.Comparar(miPuntaje, datoEnemigo);
            GameManager.instance.puntajesComparados = true;
        }
        
    }

    public void Reiniciar()
    {
        diccionario.ReiniciarJuego();
        datoObtenido = 0;
    }

    public void Comparar(string error)
    {
        if(p1 == null || p1.valor <10) 
        {
            p1 = JsonUtility.FromJson<PalabraPonderada>(error);
            Debug.LogWarning(error);
        }
        else
        {
            p2 = JsonUtility.FromJson<PalabraPonderada>(error);

            if (p1.valor > p2.valor)
            {
                p = p1;
            }
            else 
            {
                p = p2;
            }
            GameManager.instance.palabraLista = true;
            diccionario.AsignarPalabra(p.palabra);
            diccionario.MostrarPalabra();
        }

    }

    public PalabraPonderada ObtenerPalabra()
    {
        diccionario.ElegirPalabraAzar();
        PalabraPonderada palabra = new PalabraPonderada();
        palabra.palabra = diccionario.palabraActual;
        palabra.Ponderar();
        return palabra;
    }
}

[System.Serializable]
public class PalabraPonderada
{
    public string palabra;
    public int valor;

    public void Ponderar()
    {
        valor = Random.Range(1, 99999999);
    }
}