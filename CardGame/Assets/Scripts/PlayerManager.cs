using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float playerHP = 40;
    public float playerHPMax = 40;

    public GameObject healthBar;
    private Vector3 hBTransform;
    private Vector3 hBScale;

    // Start is called before the first frame update
    void Start()
    {
        hBTransform = healthBar.transform.position;
        hBScale = healthBar.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        hBTransform.x = -1.25f * (playerHP / playerHPMax);
        hBScale.x = -2.5f * (playerHP / playerHPMax);
        healthBar.transform.position = hBTransform;
    }
}
