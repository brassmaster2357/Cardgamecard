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

    public void DecideAction()
    {
        // 25% chance to heal, otherwise attack
        if (Random.Range(0f,1f) >= 0.75f)
        {
            action = "Heal";
            actionIntensity = 4;
        } else
        {
            action = "Attack";
            actionIntensity = Random.Range(1,5);
        }
    }

    public string Action()
    {
        if (action == "Heal")
        {
            if (actionIntensity + powerMod > 0)
            {
                enemyHP += actionIntensity + powerMod;
                Debug.Log("Enemy healed for " + (actionIntensity + powerMod).ToString());
            } else
            {
                Debug.Log("Enemy tried to heal, but was debuffed too much");
            }
            if (enemyHP > enemyHPMax)
            {
                enemyHP = enemyHPMax;
            }
        } else
        {
            if (actionIntensity + powerMod > 0)
            {
                pManager.playerHP -= actionIntensity + powerMod;
                Debug.Log("Enemy attacked player for " + (actionIntensity + powerMod).ToString());
            } else
            {
                Debug.Log("Enemy tried to attack, but was debuffed too much");
            }
        }
        return "Enemy turn complete";
    }
}
