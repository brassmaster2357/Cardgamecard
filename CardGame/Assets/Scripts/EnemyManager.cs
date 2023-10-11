using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject gameVarHandler;
    private GameVariableHandler vars;

    public GameObject healthBar;
    private Vector3 hBTransform;
    private Vector3 hBScale;

    // Start is called before the first frame update
    void Start()
    {
        vars = gameVarHandler.GetComponent<GameVariableHandler>();
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
}
