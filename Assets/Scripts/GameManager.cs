using NUnit.Framework;
using System.Collections;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool jugadorListo1;
    public bool jugadorListo2;

    public string puntaje1;
    public string puntaje2;
    public Photon.Chat.DemoChat.ChatNewGui chatGui;

    public string[] mensajes;
    public GameObject canvasChat;
    public GameObject canvasProp;
    public Bridge bridge;


    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine(Valores());
        StartCoroutine (FlujoDeJuego());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibirMensajes(string[] m)
    {
        mensajes = m;
    }

    public void ProcesarMensaje(string q)
    {
        print (q);

        string[] msj = q.Split('|');
        print(msj);
        print(chatGui);
        print (chatGui.UserName);
        if(msj.Length > 1 && msj[1] == "2")
        {
            if (msj[0] == chatGui.UserName)
            {
                AlistarJugador(1);
                
            }
            else
            {
                AlistarJugador(2);
            }
        }
    }

    public void AlistarJugador(int cual)
    {
        jugadorListo1 = jugadorListo1 || cual == 1;
        jugadorListo2 = jugadorListo2 || cual == 2;

        if(jugadorListo1 && jugadorListo2)
        {
            canvasChat.SetActive(false);
            canvasProp.SetActive(true);
            chatGui.chatClient.PublishMessage("Meow", "Hola");
        }
    }
    [ContextMenu("Iniciar comparacion")]
    public void CompararTest()
    {
        DatosPuntaje p1 = JsonUtility.FromJson<DatosPuntaje>(puntaje1);
        DatosPuntaje p2 = JsonUtility.FromJson<DatosPuntaje>(puntaje2);
        Comparar(p1 , p2 );
    }
    public void Comparar(DatosPuntaje miPuntaje, DatosPuntaje datoEnemigo)
    {
        if(miPuntaje.error == 0 && datoEnemigo.error == 0) 
        {
            if(miPuntaje.tiempo >= datoEnemigo.tiempo)
            {
                Ganar();
            }
            else
            {
                Perder();
            }
        }
        else if(miPuntaje.error + datoEnemigo.error > 0)
        {
            if (miPuntaje.error == 0)
            {
                Ganar();
            }
            else if(datoEnemigo.error == 0)
            {
                Perder();

            }
            else
            {
                if (miPuntaje.puntaje > datoEnemigo.puntaje)
                {
                    Ganar();
                }
                else
                { 
                    Perder(); 
                }
            }
        }
    }

    public void Ganar()
    {
        print("Ganaste!");
    }
    public void Perder()
    {
        print("Perdiste");
    }

    IEnumerator Valores()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (chatGui.chatClient!= null && chatGui.chatClient.State == Photon.Chat.ChatState.ConnectedToFrontEnd)
            {
               // chatGui.chatClient.PublishMessage("Meow", chatGui.UserName);
            }
        }
    }

    IEnumerator FlujoDeJuego()
    {
        yield return new WaitUntil(() => jugadorListo1 && jugadorListo2);
        yield return new WaitForSeconds(5f);
        Debug.LogWarning("Preparate para morir");
        PalabraPonderada p = bridge.ObtenerPalabra();
        chatGui.chatClient.PublishMessage("Meow", chatGui.UserName + "|1|" + JsonUtility.ToJson(p));

    }
}
