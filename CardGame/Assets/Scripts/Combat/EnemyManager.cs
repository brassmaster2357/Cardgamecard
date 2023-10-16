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
            enemyHP += actionIntensity;
            Debug.Log("Enemy healed for " + actionIntensity.ToString());
            if (enemyHP > enemyHPMax)
            {
                enemyHP = enemyHPMax;
            }
        } else
        {
            pManager.playerHP -= actionIntensity;
            Debug.Log("Enemy attacked player for " + actionIntensity.ToString());
            if (pManager.playerHP <= 0)
            {
                // Place "you died" script here
            }
            
        }
        return "Enemy turn complete";
    }
}
