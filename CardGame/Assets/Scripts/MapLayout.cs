using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    //refrences to all of the event icons
    public GameObject ambushIcon;
    public GameObject cardsIcon;
    public GameObject itemsIcon;
    public GameObject wizardIcon;

    public GameObject playerIcon;

    private Vector2 playerPos;
    private Vector2 eventOffset;
    private Vector2 firstEventOffset;
    private Vector2 secondEventOffset;

    public EventLoader el;

    void Start()
    {
        playerPos = playerIcon.transform.position;
        eventOffset = playerPos + new Vector2(0, 10);
        firstEventOffset = playerPos + new Vector2(-5, 10);
        secondEventOffset = playerPos + new Vector2(5, 10);
    }

    
    void Update()
    {
        if (el.eventDecided)
        {
            if (!el.fork || el.nextEvent == "Fight")
            {
                DecideNextEvent();
            }
            else if (el.fork)
            {
                DecideForkEvents();
            }
            el.eventDecided = false;
        }
    }

    private void DecideNextEvent()
    {
        if (el.nextEvent == "Cards")
        {
            Instantiate(cardsIcon, eventOffset, Quaternion.identity);
        }
        else if (el.nextEvent == "Wizard")
        {
            Instantiate(wizardIcon, eventOffset, Quaternion.identity);
        }
        else if (el.nextEvent == "Items")
        {
            Instantiate(itemsIcon, eventOffset, Quaternion.identity);
        }
        else if (el.nextEvent == "Fight")
        {
            Instantiate(ambushIcon, eventOffset, Quaternion.identity);
        }
    }

    private void DecideForkEvents()
    {
        if (el.nextEvent == "Cards")
        {
            Instantiate(cardsIcon, firstEventOffset, Quaternion.identity);
        }
        else if (el.nextEvent == "Wizard")
        {
            Instantiate(wizardIcon, firstEventOffset, Quaternion.identity);
        }
        else if (el.nextEvent == "Items")
        {
            Instantiate(itemsIcon, firstEventOffset, Quaternion.identity);
        }

        if (el.secondEvent == "Cards")
        {
            Instantiate(cardsIcon, secondEventOffset, Quaternion.identity);
        }
        if (el.secondEvent == "Wizard")
        {
            Instantiate(wizardIcon, secondEventOffset, Quaternion.identity);
        }
        if (el.secondEvent == "Items")
        {
            Instantiate(itemsIcon, secondEventOffset, Quaternion.identity);
        }
    }
}
