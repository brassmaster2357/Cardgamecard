using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
this is a list of event names, for refrence
Fight = ambush
Cards = 3 random cards
Items = failed adventurer
Boss = boss fight
Wizard = wizard buffs one of the stats of your card
*/
public class EventLoader : MonoBehaviour
{
    //this checks if there is a fork
    public bool fork;
    //this tracks the type of fork. True is 3-way and false is 2-way
    public bool forkType;

    //this is the next event type
    public string nextEvent;
    //this is the second event in a fork
    public string secondEvent;
    //this is the third event in a fork
    public string thirdEvent;

    //this tracks how many events are left before the boss
    public int eventsLeft;

    //this tracks how long it's been since the last ambush event
    public int timesSinceAmbush;

    //this bool makes it so it doesn't load new events every frame
    public bool eventDecided = false;

    //this stores the random value
    private int randomness;

    //this increases the chance of a fork every time there's not a fork
    private int chanceIncrease;

    void Start()
    {
        //intitializing vars
        //this makes it so there isn't 3 non-fight events in a row at the beginning of a map
        timesSinceAmbush = 1;
        chanceIncrease = 0;
    }


    void Update()
    {
        //this debug lets me test the random logic
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChooseEvent();
            if (!fork || nextEvent == "Fight")
                //if there isn't a fork, sya this \/
                Debug.Log("The next event is " + nextEvent);
            else if (fork && nextEvent != "Fight")
            {
                //if there is a fork, say this \/
                Debug.Log("There is a fork in the road!");
                Debug.Log("The first option is " + nextEvent);
                Debug.Log("The second option is " + secondEvent);
            }
        }

            
    }
    //put this in a function to make it easier
    private void ChooseEvent()
    {
        //sets fork to false at the beginning so that debug works
        fork = false;

        //the random value for the first event
        randomness = Random.Range(1, 100);
        Debug.Log("first event rando: " + randomness);

        //the random value for forking - the chance increase
        int forkDecider = (Random.Range(1, 100) - chanceIncrease);
        Debug.Log("forkDecider Rando: " + forkDecider);

        //makes fork true at a specific number
        if (forkDecider <= 30)
            fork = true;

        //fights happen every 3 events
        if (timesSinceAmbush >= 3)
        {
            nextEvent = "Fight";
            timesSinceAmbush = 0;
        }

        //if it's not a fight, generate random event
        else
        {
            //30% chance of the Item event
            if (randomness >= 1 && randomness <= 30)
            {
                nextEvent = "Items";
            }
            //30% chance of the Card event
            else if (randomness >= 31 && randomness <= 60)
            {
                nextEvent = "Cards";
            }
            //40% chance of the Wizard event
            else if (randomness >= 61 && randomness <= 100)
            {
                nextEvent = "Wizard";
            }
        }

        //if there is a fork and there isn't a fight, generate a second event
        if (fork && nextEvent != "Fight" )
        {
            //new random value
            randomness = Random.Range(1, 100);

            //these lines make sure that there aren't duplicate events in a fork
            if (nextEvent == "Items")
            {
                if (randomness >= 1 && randomness <= 65)
                    secondEvent = "Wizard";
                else
                    secondEvent = "Cards";
            }
            else if (nextEvent == "Cards")
            {
                if (randomness >= 1 && randomness <= 65)
                    secondEvent = "Wizard";
                else
                    secondEvent = "Items";
            }
            else if (nextEvent == "Wizard")
            {
                if (randomness >= 1 && randomness <= 50)
                    secondEvent = "Cards";
                else
                    secondEvent = "Items";
            }

            //currently unused code for a 3-way fork
            if (forkType)
            {
                //I'm going to leave this empty until we have more events
            }

            //if there was a fork, reset the chance increase for a fork
            chanceIncrease = 0;
        }

        //if there wasn't a fork, increase the chance of a fork
        else if (!fork)
            chanceIncrease += 5;

        //add one time since the last ambush
        timesSinceAmbush++;

        //we decided the event, so make this true
        eventDecided = true;
    }
}
