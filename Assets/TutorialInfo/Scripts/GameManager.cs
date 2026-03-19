using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public int player1Lives = 3;
    public int player2Lives = 3;
    public int currentRound = 1;

    [Header("UI")]
    public Text player1LivesText;
    public Text player2LivesText;
    public Text roundText;
    public Text winnerText; // 🔥 nuevo texto

    void Start()
    {
        winnerText.text = ""; // empieza vacío
        UpdateUI();
    }

    void Update()
    {
        // 🎮 Controles de prueba
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            LoseLife(1);
        }

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            LoseLife(2);
        }
    }

    public void LoseLife(int player)
    {
        if (player == 1)
        {
            player1Lives--;
        }
        else if (player == 2)
        {
            player2Lives--;
        }

        CheckGameOver();

        // 🔥 avanzar ronda automáticamente
        NextRound();

        UpdateUI();
    }

    void NextRound()
    {
        currentRound++;
    }

    void UpdateUI()
    {
        player1LivesText.text = "P1 Vidas: " + player1Lives;
        player2LivesText.text = "P2 Vidas: " + player2Lives;
        roundText.text = "Ronda: " + currentRound;
    }

    void CheckGameOver()
    {
        if (player1Lives <= 0)
        {
            winnerText.text = "Jugador 2 gana!";
            Debug.Log("Jugador 2 gana!");
            enabled = false;
        }
        else if (player2Lives <= 0)
        {
            winnerText.text = "Jugador 1 gana!";
            Debug.Log("Jugador 1 gana!");
            enabled = false;
        }
    }
}