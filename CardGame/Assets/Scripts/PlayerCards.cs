using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour
{
    private static PlayerCards instance;
    public List<CardTemplate> cardsTotal;
    public List<CardTemplate> drawPile;
    public List<CardTemplate> cardsInHand;
    public List<CardTemplate> discardPile;
    public int cardsToDraw;
    public GameObject card;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(transform.root);
            instance = this;
        }
    }

    void BeginCombat()
    {
        drawPile.Clear();
        cardsInHand.Clear();
        discardPile = cardsTotal;
        DrawCards();
    }

    void DrawCards()
    {
        for (int i = 0; i < cardsToDraw; i++)
        {
            if (drawPile.Count == 0)
            {
                drawPile = discardPile;
                discardPile.Clear();
            }
            else
            {
                cardsInHand.Add(drawPile[0]);
                GameObject newCardDrawn = Instantiate(card, Vector3.down * 10, Quaternion.identity);
                newCardDrawn.GetComponent<CardsScript>().card = drawPile[0];
                drawPile.RemoveAt(0);
            }
        }
    }

    void DiscardHand()
    {
        discardPile.AddRange(cardsInHand);
        cardsInHand.Clear();
    }
}
