using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Generic Summon", menuName = "Summon")]
public class SummonTemplate : ScriptableObject
{
    public Sprite art;
    public int health;
    public int attack;
    public bool canAttack;
    public SummonScript.SummonSpecial special;
}
