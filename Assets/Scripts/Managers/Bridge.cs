using UnityEngine;
using Photon.Chat.DemoChat;

public class Bridge : MonoBehaviour
{
    public string[] mensaje;
    public PalabraPonderada p;

    void Start()
    {
        ChatNewGui.onMessage += Mensajero;
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

    public void Comparar(string error)
    {
        print("El mensaje de error que llego es: " + error);
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