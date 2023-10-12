using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
this is a list of event names, for refrence
Fight = ambush
Cards = 3 random cards
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

    //this tracks how many events are left before the boss
    public int eventsLeft;

    //these track how long it's been since the last event of its kind for all the events
    //Ambush
    public int timesSinceAmbush;
    //random 3 cards
    public int timesSinceCards;
    //Wizard event
    public int timesSinceStatbuff;



    void Start()
    {
        
    }


    void Update()
    {
        





        if (eventsLeft <= 0)
        {
            fork = false;
            nextEvent = "Boss";
        }
    }
}
