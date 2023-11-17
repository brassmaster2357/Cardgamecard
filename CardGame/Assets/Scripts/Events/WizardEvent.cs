using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WizardEvent : MonoBehaviour
{
    public PlayerCards pc;

    public CardTemplate selectedCard;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;

    public GameObject cardBase;
    private GameObject pch;
    public GameObject button1;
    public GameObject button2;

    public List<float> list1;
    public List<float> list2;
    public List<CardTemplate> powerList;

    public int arrayPos;
    public int width;
    public int listLength;
    public int listPos;
    int position;
    int newPosition;

    private string bigCardName;
    private string cardName;
    private string smallCardName;

    public bool middle = false;

    private float distance1;
    private float distance2;

    void Start()
    {
        listLength = pc.cardsTotal.Count;
        pch = GameObject.Find("PlayerCardHandler");

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

                GameObject cardObject = Instantiate(cardBase, new Vector2(list1[i] - ((1.17f) * distance1), 2.5f), Quaternion.identity);

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
    public void SelectedCardW(CardTemplate card)
    {
        if (card.purpose == CardTemplate.EPurpose.Summon)
        {
            selectedCard = card;

            GameObject[] toBeDestroyed = GameObject.FindGameObjectsWithTag("Card");
            for (int i = 0; i < toBeDestroyed.Length; i++)
            {
                Destroy(toBeDestroyed[i]);
            }

            TakeNames();
        }
    }
    void TakeNames()
    {
        position = powerList.IndexOf(selectedCard);

        switch (position)
        {
            case 0:
                bigCardName = powerList[19].name;
                smallCardName = powerList[position + 1].name;
                break;

            case 19:
                bigCardName = powerList[position - 1].name;
                smallCardName = powerList[0].name;
                break;

            default:
                bigCardName = powerList[position - 1].name;
                smallCardName = powerList[position + 1].name;
                break;
        }
        cardName = selectedCard.name;

        text1.text = "Split your " + cardName + " into two " + smallCardName + "s";
        text2.text = "Upgrade your " + cardName + " to a " + bigCardName;

        button1.SetActive(true);
        button2.SetActive(true);
    }
    public void Split()
    {
        pc.cardsTotal.RemoveAt(arrayPos);

        switch (position)
        {
            case 19:
                newPosition = 0;
                break;

            default:
                newPosition = position + 1;
                break;
        }

        pc.cardsTotal.Add(powerList[newPosition]);
        pc.cardsTotal.Add(powerList[newPosition]);

        SceneManager.LoadScene(1);
    }
    public void Upgrade()
    {
        pc.cardsTotal.RemoveAt(arrayPos);

        switch (position)
        {
            case 0:
                newPosition = 19;
                break;

            default:
                newPosition = position - 1;
                break;
        }

        pc.cardsTotal.Add(powerList[newPosition]);

        SceneManager.LoadScene(1);
    }
}
