using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public bool middle = false;

    public int width;

    public float distance;

    public List<float> list1;
    public List<float> list2;

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

        }
    }
    private void FindPositions()
    {
        int size = 2 * width;

        if (middle)
        {
            distance = size / (listLength + 1);

            float runningTotal = -width;

            for (int i = 0; i < listLength; i++)
            {
                runningTotal += distance;

                list1.Add(runningTotal);
            }
        }
        else
        {
            if (listLength % 2 == 1)
            {

            }
        }
    }
}
