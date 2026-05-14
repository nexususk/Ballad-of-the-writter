using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public string puntaje1;
    public string puntaje2;
    public Photon.Chat.DemoChat.ChatNewGui chatGui;

    public string[] mensajes;
    public GameObject canvasChat;
    public GameObject canvasProp;
    public Bridge bridge;

    public ParticleSystem particulasP;
    public Animator animatorP;
    public ParticleSystem particulasE;
    public Animator animatorE;

    public Image[] corazones;

   

    [SerializeField]
    int victorias;

    [SerializeField]
    int derrotas;

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
            if(miPuntaje.tiempo <= datoEnemigo.tiempo)
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
        particulasP.Play();
        animatorP.SetTrigger("Drawing");
        
        animatorE.SetTrigger("TookDamage");
        victorias++;
    }
    public void Perder()
    {
        print("Perdiste");
        particulasE.Play();
        
        animatorE.SetTrigger("Drawing");
        animatorP.SetTrigger("TookDamage");

        derrotas++;
        ActualizarCorazones();

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

    public bool jugadorListo1;
    public bool jugadorListo2;

    public bool palabraLista;
    public bool enviePalabraEscrita;
    public bool puntajesComparados;
    public void EnviarPalabraEscrita(string cual)
    {
        chatGui.chatClient.PublishMessage("Meow", "2|" + cual);
        enviePalabraEscrita = true;
    }

    public void ActualizarCorazones()
    {
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < derrotas)
            {
                corazones[i].enabled = false; 
            }
            else
            {
                corazones[i].enabled = true; 
            }
        }
    }

    IEnumerator FlujoDeJuego()
    {
        yield return new WaitUntil(() => jugadorListo1 && jugadorListo2);


        while (victorias < 3 && derrotas < 3)
        {
            yield return new WaitForSeconds(5f);
            Debug.LogWarning("Preparate para morir");
            palabraLista = false;
            PalabraPonderada p = bridge.ObtenerPalabra();
            chatGui.chatClient.PublishMessage("Meow", "1|" + JsonUtility.ToJson(p));
            yield return new WaitUntil(() => palabraLista);
            bridge.Reiniciar();
            enviePalabraEscrita = false;
            yield return new WaitUntil(() => enviePalabraEscrita);
            puntajesComparados = false;
            yield return new WaitUntil(() => puntajesComparados);
            yield return new WaitForSeconds(3);
        }

        if(victorias > 2)
        {
            Debug.LogError("Soy el gandor");
            animatorP.SetTrigger("Victory");
            animatorE.SetTrigger("Died");
            
        }
        else
        {
            Debug.LogError("Perdi mi dignidad");
            animatorE.SetTrigger("Victory");
            animatorP.SetTrigger("Died");
            
        }
    }
}
