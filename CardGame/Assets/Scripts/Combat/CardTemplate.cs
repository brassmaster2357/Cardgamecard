using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Blank Card", menuName = "Card Template")]
public class CardTemplate : ScriptableObject
{
    public Sprite art;
    public int cost;
    public GameObject target; //Default target if something goes wrong
    public SummonTemplate summon;
    public int power;
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
