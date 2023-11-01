using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummonScript: MonoBehaviour
{
    public bool isFollowing;
    public Vector3 moveTo;
    public Vector3 restPosition;

    public TextMeshProUGUI healthDisplay;
    public TextMeshProUGUI attackDisplay;
    public Sprite defaultSprite;

    public GameObject target;
    public GameObject enemy;
    public GameObject player;
    private PlayerManager playerScript;
    private EnemyManager enemyScript;

    public bool alive;
    public float health;
    public float healthMax;
    public float attack;
    public bool canAttack;
    public bool isAlly;
    public enum SummonSpecial
    {
        None,
        NoAttack
    }
    public SummonSpecial special;
    void Start()
    {
        restPosition = transform.position;
        enemyScript = enemy.GetComponent<EnemyManager>();
        playerScript = player.GetComponent<PlayerManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canAttack && (special == SummonSpecial.NoAttack))
        {
            canAttack = false;
        }
        if (isFollowing)
            moveTo = (gameObject.transform.position + target.transform.position) / 2;
        else
            moveTo = (gameObject.transform.position + restPosition) / 2;
        moveTo.z = 0;
        gameObject.transform.position = moveTo;
        healthDisplay.text = health.ToString();
        healthDisplay.enabled = alive;
        attackDisplay.text = attack.ToString();
        attackDisplay.enabled = alive;
    }

    public void Summon(float hp, float atk, bool can, Sprite art)
    {
        health = hp;
        healthMax = hp;
        attack = atk;
        canAttack = can;
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = art;
        if (renderer.sprite == null)
        {
            renderer.sprite = defaultSprite;
        }
        alive = true;
    }

    public void Attack()
    {
        //Move the summon to the target, to at least slightly animate battle
        isFollowing = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            isFollowing = false;
            SummonScript targetScript = target.GetComponent<SummonScript>();
            if (targetScript.alive)
            {
                targetScript.health -= attack;
                if (targetScript.health <= 0) {
                    targetScript.alive = false;
                }
            } else
            {
                if (isAlly)
                {
                    enemyScript.enemyHP -= attack;
                } else
                {
                    playerScript.playerHP -= attack;
                }
            }
        }
    }
}
