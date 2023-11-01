using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEvent : MonoBehaviour
{

    private CardTemplate[] cardArray;

    private int random;

    void Start()
    {
        random = Random.Range(1, 13);
    }

    void Update()
    {
        
    }
}
