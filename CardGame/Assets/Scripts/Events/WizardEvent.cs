using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
okay me, this is how it's going to work.
you will load in
then you are able to choose a card
this card will be buffed by the player's choice of +1 attack or +2 health
then it will transferr back to the map
*/
public class WizardEvent : MonoBehaviour
{

    public int listLength;

    private bool CardSelected;

    public PlayerCards pc;

    public GameObject cardBase;
    public GameObject pch;
    public GameObject button1;
    public GameObject button2;

    public CardTemplate selectedCard;
    public int arrayPos;

    public bool middle = false;

    public int width;

    public List<float> list1;
    public List<float> list2;

    private float distance1;
    private float distance2;

    void Start()
    {
        listLength = pc.cardsTotal.Count;
        CardSelected = false;

        
    }
    
    void Update()
    {
        if (!CardSelected)
        {
            if (listLength <= 5)
            {
                middle = true;
            }
            else
            {
                middle = false;
            }

            FindPositions();
            LoadPositions();

            CardSelected = true;
        }
    }
    private void FindPositions()
    {
        int size = 2 * width;

        float runningTotal = -width;

        if (middle)
        {
            distance1 = size / (listLength + 1);

            for (int i = 0; i < listLength; i++)
            {
                runningTotal += distance1;
                list1.Add(runningTotal);
            }
        }
        else
        {

            float offset = 0.5f * (listLength % 2);

            int length1 = (int)((0.5f * listLength) + offset);

            int length2 = length1 - 1 * (listLength % 2);

            distance1 = size / length1 + 1;

            for (int i = 0; i < length1; i++)
            {
                runningTotal += distance1;

                list1.Add(runningTotal);
            }

            distance2 = size / length2 + 1;
           
            runningTotal = 0;

            for (int i = 0; i < length2; i++)
            {
                runningTotal += distance2;

                list2.Add(runningTotal);
            }
        }
    }

    private void LoadPositions()
    {
        if (middle == true)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                
                CardTemplate card = (pch.GetComponent<PlayerCards>().cardsTotal[i]);

                GameObject cardObject = Instantiate(cardBase, new Vector2(list1[i] - distance1, 0), Quaternion.identity);

                (cardObject.GetComponent<CardsScript>()).cardPos = i;

                CardsScript tempScript = cardObject.GetComponent<CardsScript>();

                tempScript.eventLoader = GameObject.Find("EventController");

                tempScript.card = pc.cardsTotal[i];

                tempScript.LoadCard();

                tempScript.restPosition = cardObject.transform.position;
            }
        }
        else {
            for (int i = 0; i < list1.Count; i++)
            {
                CardTemplate card = (pch.GetComponent<PlayerCards>().cardsTotal[i]);

                GameObject cardObject = Instantiate(cardBase, new Vector2(list1[i] - distance1, 2.5f), Quaternion.identity);

                (cardObject.GetComponent<CardsScript>()).cardPos = i;

                CardsScript tempScript = cardObject.GetComponent<CardsScript>();

                tempScript.eventLoader = GameObject.Find("EventController");

                tempScript.card = pc.cardsTotal[i];

                tempScript.LoadCard();

                tempScript.restPosition = cardObject.transform.position;
            }

            for (int i = 0; i < list2.Count; i++)
            {
                CardTemplate card = (pch.GetComponent<PlayerCards>().cardsTotal[i + list1.Count]);

                GameObject cardObject = Instantiate(cardBase, new Vector2(list2[i] - (3.5f * distance2), -2.5f), Quaternion.identity);

                (cardObject.GetComponent<CardsScript>()).cardPos = i + list1.Count;

                CardsScript tempScript = cardObject.GetComponent<CardsScript>();

                tempScript.eventLoader = GameObject.Find("EventController");

                tempScript.card = pc.cardsTotal[i + list1.Count];

                tempScript.LoadCard();

                tempScript.restPosition = cardObject.transform.position;
            }
        }
    }
    public void SelectedCard(CardTemplate card)
    {
        if (card.purpose == CardTemplate.EPurpose.Summon)
        {
            selectedCard = card;



            GameObject[] toBeDestroyed = GameObject.FindGameObjectsWithTag("Card");

            for (int i = 0; i < toBeDestroyed.Length; i++)
            {
                Destroy(toBeDestroyed[i]);
            }

            button1.SetActive(true);
            button2.SetActive(true);
        }
    }
    public void AddAttack()
    {
        CardTemplate creature = new CardTemplate();
        creature = selectedCard;
        creature.attack += 1;
        creature.name += "+";

        button1.SetActive(false);
        button2.SetActive(false);

        pc.cardsTotal[arrayPos] = creature;

        SceneManager.LoadScene(1);
    }
    public void AddDefense()
    {
        CardTemplate creature = selectedCard;
        creature.health += 2;
        creature.name += "+";

        button1.SetActive(false);
        button2.SetActive(false);

        pc.cardsTotal[arrayPos] = creature;

        SceneManager.LoadScene(1);
    }
}