using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.scripts
{
    public class Board : MonoBehaviour
    {
        
        public Sprite[] Icons;
        public int row;
        public int column;
        public card cardPrefab;
        public Vector2 Gap;
        public Transform StartPos;
        card[] cards;
        card openCard1, openCard2;
        public float CardShowTime = 2;
        float CardCloseTime;
        bool card2Opened;
        int totalCards;

        void Start ()
        {
            generateBoard();
        }

        void Update ()
        {
            if (card2Opened)
            {
                CardCloseTime += Time.deltaTime;
                if (CardCloseTime >= CardShowTime)
                {
                    checkForMatch();
                    card2Opened = false;
                    CardCloseTime = 0;
                }
            }
        }

        void generateBoard()
        {
            
            float xPos = StartPos.position.x, yPos = StartPos.position.y;
            int cardIndex = 0;

            totalCards = row * column;

            cards = new card[totalCards];
            
            for (int i = 0; i < row; i++)
            {
                for(int j = 0; j < column; j++)
                {
                   card card = Instantiate(cardPrefab);
                    card.transform.position = new Vector3(xPos,yPos);
                    xPos += cardPrefab.Width + Gap.x;

                    int iconIndex = cardIndex / 2;
                    card.Icon = Icons[iconIndex];

                    cards[cardIndex] = card;

                    cardIndex++;


                }

                yPos -= cardPrefab.Height + Gap.y;
                xPos = StartPos.position.x;
            }
            
            randomizeCards();

        }

        public void randomizeCards()
        {
            for (int i = cards.Length - 1; i > 0; i--)
            {
                card card = cards[i];
                if (card == null || !card.gameObject.activeSelf) continue; // Kart yoksa veya aktif değilse atla

                int otherCardIndex = UnityEngine.Random.Range(0, i);
                card otherCard = cards[otherCardIndex];
                if (otherCard == null || !otherCard.gameObject.activeSelf) continue; // Diğer kart yoksa veya aktif değilse atla

                Vector3 tempPos = card.transform.position;
                card.transform.position = otherCard.transform.position;
                otherCard.transform.position = tempPos;
            }
        }




        public void CardOpened(card card)
        {
            if (openCard1 == null)
                openCard1 = card;
            else
            {
                openCard2 = card;
                card2Opened = true;
                

            }
        }

        void checkForMatch()
        {
            if (openCard1.Icon == openCard2.Icon)
            {
                totalCards -= 2;
                Destroy(openCard1.gameObject);
                Destroy(openCard2.gameObject);

                
            }
            else
            {
                Debug.Log("eslesme yok");
                openCard1.CloseCard();
                openCard2.CloseCard();
            }

            openCard1 = null;
            openCard2 = null;

            if (totalCards <= 0)
            {
                // Bütün kartlar yok edilmişse, bir sonraki sahneye geçiş işlemi yapılır
                GoToNextScene();
            }
        }

        void GoToNextScene()
        {
            // Mevcut sahnenin indeksini al
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Bir sonraki sahneye geçmek için sahne indeksini bir arttır ve yükle
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        public bool CanCardOpen()
        {
            return card2Opened == false;
        }


    }
}
