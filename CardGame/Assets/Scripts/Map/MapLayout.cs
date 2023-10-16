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

    //refrences the road objects
    public GameObject straightRoad;
    public GameObject forkedRoad;

    //holds the position values for all the objects
    private Vector2 playerPos;
    private Vector2 eventOffset;
    private Vector2 firstEventOffset;
    private Vector2 secondEventOffset;

    //refrences the EventLoader
    public EventLoader el;

    void Start()
    {
        //initialize the position values
        playerPos = playerIcon.transform.position;
        eventOffset = playerPos + new Vector2(0, 10);
        firstEventOffset = playerPos + new Vector2(-5, 10);
        secondEventOffset = playerPos + new Vector2(5, 10);
    }

    
    void Update()
    {
        //if the event has been decided...
        if (el.eventDecided)
        {
            
            //check to make sure its not a fork OR the next event is the ambush event
            if (!el.fork || el.nextEvent == "Fight")
            {
                //if so, call this function
                DecideNextEvent();
            }

            //otherwise, check if it's a fork
            else if (el.fork)
            {

                //if so, call this function
                DecideForkEvents();
            }

            //regardless, after everything, tell the code that the event is not decided so the code doesn't loop in Update
            el.eventDecided = false;
        }
    }

    //this function creates the icon objects for when there is only one event
    private void DecideNextEvent()
    {

        //if the event is cards, make the cards Icon with the single event position offset
        if (el.nextEvent == "Cards")
        {
            Instantiate(cardsIcon, eventOffset, Quaternion.identity);
        }

        //if the event is wizard, make the wizard Icon with the single event position offset
        else if (el.nextEvent == "Wizard")
        {
            Instantiate(wizardIcon, eventOffset, Quaternion.identity);
        }

        //if the event is items, make the items Icon with the single event position offset
        else if (el.nextEvent == "Items")
        {
            Instantiate(itemsIcon, eventOffset, Quaternion.identity);
        }

        //if the event is ambush, make the ambush Icon with the single event position offset
        else if (el.nextEvent == "Fight")
        {
            Instantiate(ambushIcon, eventOffset, Quaternion.identity);
        }

        //after everything, enable the single event road
        straightRoad.SetActive(true);
    }


    //this function creates the icon objects for when there is two events
    private void DecideForkEvents()
    {
        
        //same as the first function, but without the ambush event and with a different offset
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
        else if (el.secondEvent == "Wizard")
        {
            Instantiate(wizardIcon, secondEventOffset, Quaternion.identity);
        }
        else if (el.secondEvent == "Items")
        {
            Instantiate(itemsIcon, secondEventOffset, Quaternion.identity);
        }

        //after everything, enable the forked road
        forkedRoad.SetActive(true);
    }
}
