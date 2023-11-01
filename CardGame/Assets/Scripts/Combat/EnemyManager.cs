using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject player;
    private PlayerManager pManager;

    public string action;
    public int actionIntensity;

    public GameObject healthBar;
    private Vector3 hBTransform;
    private Vector3 hBScale;

    public SummonTemplate[] summons;
    public GameObject[] summonPositions;

    public float enemyHP = 20;
    public float enemyHPMax = 20;
    public float powerModDefault = 0;
    public float powerMod = 0;

    // Start is called before the first frame update
    void Start()
    {
        pManager = player.GetComponent<PlayerManager>();
        // Get default position of health bar
        hBTransform = healthBar.transform.position;
        hBScale = healthBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Move and scale health bar to make it look like it's draining to the left
        hBTransform.x = -1.25f * (enemyHP / enemyHPMax) + gameObject.transform.position.x + 1.25f;
        hBScale.x = 2.5f * (enemyHP / enemyHPMax);
        healthBar.transform.position = hBTransform;
        healthBar.transform.localScale = hBScale;
    }

    public void EnemyTurn()
    {
        SummonTemplate summoned = (SummonTemplate)summons.GetValue(Random.Range(0, summons.Length));
        SummonScript target = ((GameObject)summonPositions.GetValue(Random.Range(0, 3))).GetComponent<SummonScript>();
        if (!target.alive)
        {
            target.Summon(summoned.health, summoned.attack, true, summoned.art);
        }
    }
}
