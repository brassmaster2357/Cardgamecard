using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject healthBar;
    private Vector3 hBTransform;
    private Vector3 hBScale;

    public float playerHP = 40;
    public float playerHPMax = 40;
    public float powerModDefault = 0;
    public float powerMod = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Get default position of health bar
        hBTransform = healthBar.transform.position;
        hBScale = healthBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //Move and scale health bar to make it look like it's draining to the left
        hBTransform.x = -1.25f * (playerHP / playerHPMax) + gameObject.transform.position.x + 1.25f;
        hBScale.x = -2.5f * (playerHP / playerHPMax);
        healthBar.transform.position = hBTransform;
        healthBar.transform.localScale = hBScale;
    }
}
