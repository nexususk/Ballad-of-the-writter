using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool jugadorListo1;
    public bool jugadorListo2;

    public string puntaje1;
    public string puntaje2;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlistarJugador(int cual)
    {
        jugadorListo1 = jugadorListo1 || cual == 1;
        jugadorListo2 = jugadorListo2 || cual == 2;
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
}
