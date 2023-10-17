using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSummons : MonoBehaviour
{
    public float health;
    public float healthMax;
    public float attack;
    public bool canAttack;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Summon(float hp, float atk, bool can, Sprite art)
    {
        health = hp;
        healthMax = hp;
        attack = atk;
        canAttack = can;
        gameObject.GetComponent<SpriteRenderer>().sprite = art;
    }
}
