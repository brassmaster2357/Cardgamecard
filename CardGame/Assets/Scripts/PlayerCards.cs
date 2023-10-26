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
    public GameObject card;
    public List<GameObject> objectHand;

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

    public void OrganizeHand()
    {
        objectHand.Clear();
        objectHand.AddRange(GameObject.FindGameObjectsWithTag("Card"));
        for (int i = 0; i < objectHand.Count; i++)
        {
            Debug.Log(objectHand[i]);
        }
    }

    public void BeginCombat()
    {
        discardPile.Clear();
        drawPile.Clear();
        cardsInHand.Clear();
        drawPile = cardsTotal;
    }

    public void Draw(int cardsToDraw)
    {
        for (int i = 0; i < cardsToDraw; i++)
        {
            Debug.Log(drawPile.Count);
            if (drawPile.Count <= 0)
            {
                //""""Shuffle"""" the discard pile back into the draw pile because we ran out of cards to draw
                Debug.Log("you're supposed to work");
                drawPile = discardPile;
                //discardPile.Clear();
            }
            //Draw a card
            cardsInHand.Add(drawPile[0]);
            GameObject newCardDrawn = Instantiate(card, Vector3.right * 16 + Vector3.down * 6, Quaternion.identity);
            CardsScript tempScript = newCardDrawn.GetComponent<CardsScript>();
            tempScript.card = drawPile[0];
            tempScript.enabled = true;
            tempScript.LoadCard();
            drawPile.RemoveAt(0);
        }
        Debug.Log(cardsInHand);
    }

    public void DiscardHand()
    {
        discardPile.AddRange(cardsInHand);
        cardsInHand.Clear();
        Debug.Log(discardPile);
    }

    public void Discard(CardTemplate card)
    {
        discardPile.Add(card);
        cardsInHand.Remove(card);
        Debug.Log(discardPile);
    }
}
