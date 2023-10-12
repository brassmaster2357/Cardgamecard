using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject gameVarHandler;
    private GameVariableHandler vars;
    public GameObject combatManager;
    private CombatManager combat;

    public string action;
    public int actionIntensity;

    public GameObject healthBar;
    private Vector3 hBTransform;
    private Vector3 hBScale;

    // Start is called before the first frame update
    void Start()
    {
        vars = gameVarHandler.GetComponent<GameVariableHandler>();
        combat = combatManager.GetComponent<CombatManager>();
        // Get default position of health bar
        hBTransform = healthBar.transform.position;
        hBScale = healthBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Move and scale health bar to make it look like it's draining to the left
        hBTransform.x = -1.25f * (vars.enemy1HP / vars.enemy1HPMax) + gameObject.transform.position.x + 1.25f;
        hBScale.x = 2.5f * (vars.enemy1HP / vars.enemy1HPMax);
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
            vars.enemy1HP += actionIntensity;
            Debug.Log("Enemy healed for " + actionIntensity.ToString());
            if (vars.enemy1HP > vars.enemy1HPMax)
            {
                vars.enemy1HP = vars.enemy1HPMax;
            }
        } else
        {
            vars.playerHP += actionIntensity;
            Debug.Log("Enemy attacked player for " + actionIntensity.ToString());
            if (vars.playerHP <= 0)
            {
                // Place "you died" script here
            }
            
        }
        return "Enemy turn complete";
    }
}
