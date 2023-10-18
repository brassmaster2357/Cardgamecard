using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blank Card", menuName = "Card Template")]
public class CardTemplate : ScriptableObject
{
    public Sprite art;
    public int cost;
    public string description;
    public GameObject target; //Default target if something goes wrong
    public SummonTemplate summon;
    public int attack;
    public int health;
    public enum EPurpose
    {
        Attack,
        Heal,
        Summon,
        Buff,
        Debuff
    }
    public EPurpose purpose;
}
