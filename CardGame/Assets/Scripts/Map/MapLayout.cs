using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLayout : MonoBehaviour
{
    //refrences to all of the event icons
    public GameObject ambushIcon;
    public GameObject cardsIcon;
    public GameObject wizardIcon;
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
        //initialize the values
        el = GameObject.Find("EventLoader").GetComponent<EventLoader>();
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
        if (!el.eventDecided)
        {

            //call this function
            DecideNextEvent();

            //after everything, tell the code that the event is not decided so the code doesn't loop in Update
            el.eventDecided = true;
        }
    }

    //this function creates the icon objects for when there is only one event
    private void DecideNextEvent()
    {
        switch (el.nextEvent)
        {
            //if the event is cards, make the cards Icon with the single event position offset
            case "Cards":
                Instantiate(cardsIcon, eventOffset, Quaternion.identity);
                break;

            //if the event is safe haven, make the haven Icon with the single event position offset
            case "Haven":
                Instantiate(havenIcon, eventOffset, Quaternion.identity);
                break;

            //if the event is ambush, make the ambush Icon with the single event position offset
            case "Fight":
                Instantiate(ambushIcon, eventOffset, Quaternion.identity);
                break;

            //if the event is wizard, make the wizard Icon with the single event position offset
            case "Wizard":
                Instantiate(wizardIcon, eventOffset, Quaternion.identity);
                break;

            default:
                break;
        }
        straightRoad.SetActive(true);
    }
}
