using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //for emergency skip
/*
this is a list of event names, for refrence
Fight = ambush
Cards = 3 random cards
Haven = safe haven event
Boss = boss fight
Wizard = wizard buffs one of the stats of your card
*/


public class EventLoader : MonoBehaviour
{

    public GameObject road;

    private static EventLoader instance;

    //this checks to see of this is the first level
    public bool isCabin = false;

    //tracks the event number for the first level
    private int eventNum = 1;
    
    //this is the next event type
    public string nextEvent;

    //this tracks how many events are left before the boss
    public int eventsLeft;

    //this tracks how long it's been since the last ambush event
    public int timesSinceAmbush;

    //this bool makes it so it doesn't load new events every frame
    public bool eventDecided;

    //this stores the random value
    private int randomness;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(transform.root);
            instance = this;
        }
    }

    void Start()
    {
        //intitializing vars
        eventDecided = false;
        Debug.Log("The event was decided");
        //this makes it so there isn't 3 non-fight events in a row at the beginning of a map
        timesSinceAmbush = 1;
    }


    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            GameObject Event = GameObject.FindGameObjectWithTag("Event");
            if (Event == null)
            {
                eventDecided = false;
            }
        }
        if (!eventDecided)
        {
            ChooseEvent();
        }
    }
    //put this in a function to make it easier
    private void ChooseEvent()
    {
        //if the level isn't the first level, do all of this
        if (!isCabin)
        {
            //the random value for the event
            randomness = Random.Range(1, 100);
            Debug.Log("event rando: " + randomness);

            //fights happen every 3 events
            if (timesSinceAmbush >= 3)
            {
                nextEvent = "Fight";
                timesSinceAmbush = 0;
            }

            //if it's not a fight, generate random event
            else
            {
                //20% chance of the Item event
                if (randomness >= 1 && randomness <= 20)
                {
                    nextEvent = "Haven";
                }
                //40% chance of the Card event
                else if (randomness >= 21 && randomness <= 60)
                {
                    nextEvent = "Cards";
                }
                //40% chance of the Wizard event
                else if (randomness >= 61 && randomness <= 100)
                {
                    nextEvent = "Wizard";
                }
            }
            
            //add one time since the last ambush
            timesSinceAmbush++;
        }

        //if it is the first level, do the next scripted event for CABIN
        else
        {
            switch (eventNum)
            {
                case 1:
                    nextEvent = "Cards";
                    eventNum++;
                    break;

                case 2:
                    nextEvent = "Wizard";
                    eventNum++;
                    break;

                case 3:
                    nextEvent = "Ambush";
                    eventNum++;
                    break;

                case 4:
                    nextEvent = "Cards";
                    eventNum++;
                    break;

                case 5:
                    nextEvent = "Haven";
                    eventNum++;
                    break;

                case 6:
                    nextEvent = "Ambush";
                    isCabin = false;
                    break;

                default:
                    break;
            }
        }
    }
}
