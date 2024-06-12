using Assets.scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float TotalTime;
    private float initialTotalTime; // Başlangıç zamanını saklamak için bir değişken ekledik

    bool gameOver = false;
    bool gameWon = false;

    Board board;

    void Start()
    {
        initialTotalTime = TotalTime; // Başlangıç zamanını kaydediyoruz
        board = FindObjectOfType<Board>(); // Board nesnesini buluyoruz
    }

    void Update()
    {
        if (!gameOver)
        {
            TotalTime -= Time.deltaTime;

            if (TotalTime <= 0)
            {
                Debug.Log("Sure bitti");
                gameOver = true;
                // Oyun bittiğinde kartları yeniden diz
                RestartGame();
            }
        }
    }

    public void RestartGame()
    {
        // Oyun süresini ve diğer değişkenleri sıfırla
        TotalTime = initialTotalTime;
        gameOver = false;
        gameWon = false;
        // Diğer yeniden başlatma işlemleri buraya eklenebilir

        // Kartları tekrar dizmek için Board sınıfına erişebilir ve randomizeCards() fonksiyonunu çağırabiliriz.
        if (board != null)
        {
            board.randomizeCards();
        }
    }

    public void LevelCleared()
    {
        gameOver = true;
        gameWon = true;
        Debug.Log("Kazandın!");
        RestartGame();
    }
}
