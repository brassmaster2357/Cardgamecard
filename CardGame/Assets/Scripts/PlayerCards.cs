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
    public GameObject cardPrefab;
    public List<GameObject> objectHand;
    public Vector3 defaultRest = new Vector3(0, -4, 0);

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
            GameObject theobject = objectHand[i];
            CardsScript script = theobject.GetComponent<CardsScript>();
            script.restPosition = defaultRest;
            if (objectHand.Count > 8)
            {
                script.restPosition.x = (float)(i * 2f - objectHand.Count) / (float)((float)objectHand.Count / 8);
            } else
            {
                script.restPosition.x = i * 2 - objectHand.Count;
            }
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
                Debug.Log(drawPile.Count);
                Debug.Log(discardPile.Count);
                drawPile.AddRange(discardPile);
                Debug.Log(drawPile.Count);
                Debug.Log(discardPile.Count);
                discardPile.Clear();
                Debug.Log(drawPile.Count);
                Debug.Log(discardPile.Count);
            }
            //Draw a card
            cardsInHand.Add(drawPile[0]);
            GameObject newCardDrawn = Instantiate(cardPrefab, Vector3.right * 16 + Vector3.down * 16, Quaternion.identity);
            CardsScript tempScript = newCardDrawn.GetComponent<CardsScript>();
            tempScript.card = drawPile[0];
            tempScript.enabled = true;
            tempScript.LoadCard();
            drawPile.RemoveAt(0);
        }
    }

    public void DiscardHand()
    {
        discardPile.AddRange(cardsInHand);
        GameObject[] allCardObjects = GameObject.FindGameObjectsWithTag("Card");
        for (int i = 0; i < cardsInHand.Count; i++)
        {
            Destroy(allCardObjects[i]);
        }
        cardsInHand.Clear();
    }

    public void Discard(CardTemplate card)
    {
        discardPile.Add(card);
        cardsInHand.Remove(card);

    }
}
