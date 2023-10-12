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
    //this checks if ther is a fork
    public bool fork;
    //this tracks the type of fork. True is 3-way and false is 2-way
    public bool forkType;

    //this is the next event type
    public string nextEvent;
    //this is the second event in a fork
    public string secondEvent;
    //this is the third event in a fork
    public string thirdEvent;
    //this helps with choosing the random event
    public string randomEvent;

    //this tracks how many events are left before the boss
    public int eventsLeft;

    //this tracks how long it's been since the last ambush event
    public int timesSinceAmbush;

    public bool eventDecided = false;

    private int randomness;

    private int chanceIncrease;

    void Start()
    {
        timesSinceAmbush = 1;
        chanceIncrease = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ChooseEvent();
            if (!fork || nextEvent == "Fight")
                Debug.Log("The next event is " + nextEvent);
            else if (fork && nextEvent != "Fight")
            {
                Debug.Log("There is a fork in the road!");
                Debug.Log("The first option is " + nextEvent);
                Debug.Log("The second option is " + secondEvent);
            }
        }

            
    }
    private void ChooseEvent()
    {
        fork = false;

        randomness = Random.Range(1, 100);
        Debug.Log("first event rando: " + randomness);

        int forkDecider = (Random.Range(1, 100) - chanceIncrease);
        Debug.Log("forkDecider Rando: " + forkDecider);

        if (forkDecider <= 30)
            fork = true;

        if (timesSinceAmbush >= 3)
        {
            nextEvent = "Fight";
            timesSinceAmbush = 0;
        }
        else
        {
            if (randomness >= 1 && randomness <= 30)
            {
                nextEvent = "Items";
            }

            else if (randomness >= 31 && randomness <= 60)
            {
                nextEvent = "Wizard";
            }

            else if (randomness >= 61 && randomness <= 100)
            {
                nextEvent = "Cards";
            }
        }
        if (fork && nextEvent != "Fight")
        {
            randomness = Random.Range(1, 100);
            if (nextEvent == "Items")
            {
                if (randomness >= 1 && randomness <= 65)
                    secondEvent = "Cards";
                else
                    secondEvent = "Wizard";
            }
            else if (randomness >= 31 && randomness <= 60)
            {
                if (randomness >= 1 && randomness <= 65)
                    secondEvent = "Cards";
                else
                    secondEvent = "Items";
            }
            else if (randomness >= 61 && randomness <= 100)
            {
                if (randomness >= 1 && randomness <= 50)
                    secondEvent = "Wizard";
                else
                    secondEvent = "Items";
            }
            if (forkType)
            {
                //I'm going to leave this empty until we have more events
            }
            chanceIncrease = 0;
        }
        else if (!fork)
            chanceIncrease += 5;
        timesSinceAmbush++;
    }
}
