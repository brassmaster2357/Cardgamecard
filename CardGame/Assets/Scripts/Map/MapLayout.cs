using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    //refrences to all of the event icons
    public GameObject ambushIcon;
    public GameObject cardsIcon;
    public GameObject itemsIcon;
    public GameObject havenIcon;
    public GameObject playerIcon;

    //refrences the road object
    public GameObject straightRoad;

    //background art
    public GameObject cabinArt;
    public GameObject forestArt;

    //holds the position values for all the objects
    private Vector2 playerPos;
    private Vector2 eventOffset;

    //refrences the EventLoader
    public EventLoader el;

    void Start()
    {
        //initialize the position values
        playerPos = playerIcon.transform.position;
        eventOffset = playerPos + new Vector2(0, 10);

        //these statements decide which background art should be active based on the map
        if (el.isCabin)
        {
            //if it is the cabin, use the cabin art
            cabinArt.SetActive(true);
        }
        else
        {
            //if it is the forest, use the forest art
            forestArt.SetActive(true);
        }
    }

    
    void Update()
    {
        //if the event has been decided...
        if (el.eventDecided)
        {

            //call this function
            DecideNextEvent();

            //after everything, tell the code that the event is not decided so the code doesn't loop in Update
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
        else if (el.nextEvent == "Haven")
        {
            Instantiate(havenIcon, eventOffset, Quaternion.identity);
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

        //after everything, enable the road
        straightRoad.SetActive(true);
    }
}
