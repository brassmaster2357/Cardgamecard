using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SummonScript: MonoBehaviour
{
    public bool isFollowing;
    public Vector3 moveTo;
    public Vector3 restPosition;
    public SpriteRenderer sprenderer;
    private float stopAttackingTimer;

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
        NoAttack,
        Spiky,
        Trap,
        Instakill
    }
    public SummonSpecial special;
    void Start()
    {
        restPosition = transform.position;
        enemyScript = enemy.GetComponent<EnemyManager>();
        playerScript = player.GetComponent<PlayerManager>();
        sprenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!alive)
            Die();
        if (canAttack && (special == SummonSpecial.NoAttack || special == SummonSpecial.Trap))
        {
            canAttack = false;
        }
        if (isFollowing)
        {
            moveTo = (gameObject.transform.position + target.transform.position) / 2;
            sprenderer.enabled = true;
        }
        else
        {
            if (health <= 0 && !isAlly)
            {
                moveTo = (gameObject.transform.position + enemy.transform.position) / 2;
                sprenderer.enabled = false;
            }
            else
            {
                moveTo = (gameObject.transform.position + restPosition) / 2;
                sprenderer.enabled = true;
            }
        }
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
        sprenderer.enabled = true;
        sprenderer.sprite = art;
        if (sprenderer.sprite == null)
        {
            sprenderer.sprite = defaultSprite;
        }
        alive = true;
    }

    public void Attack()
    {
        //Move the summon to the target, to at least slightly animate battle
        isFollowing = true;
        if (special == SummonSpecial.NoAttack || special == SummonSpecial.Trap) {
            isFollowing = false;
         }
        if (!alive)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!alive)
            Die();
        if (collision.gameObject == target && health > 0 && alive && canAttack)
        {
            Debug.Log(name + " is colliding with target");
            isFollowing = false;
            SummonScript targetScript = target.GetComponent<SummonScript>();
            if (targetScript.alive)
            {
                Debug.Log(name + " hit target");
                if (special == SummonSpecial.Instakill)
                {
                    targetScript.health -= 69420;
                }
                if (targetScript.special == SummonSpecial.Spiky)
                {
                    health -= targetScript.attack;
                    if (health <= 0)
                        Die();
                }
                targetScript.health -= attack;
                if (targetScript.health <= 0)
                {
                    targetScript.alive = false;
                }
            }
            else
            {
                Debug.Log(name + "'s target is dead");
                if (isAlly)
                {
                    enemyScript.enemyHP -= attack;
                }
                else
                {
                    playerScript.playerHP -= attack;
                }
            }
        }
        else if (stopAttackingTimer < 1)
            stopAttackingTimer += Time.deltaTime;
        else if (stopAttackingTimer >= 1)
        {
            Debug.Log(name + " STOP ATTACKING ALREADY");
            isFollowing = false;
            target = null;
        }
        else
            Die();
    }

    public void Die()
    {
        if (alive) { Debug.Log(name + " is ded"); }
        sprenderer.sprite = defaultSprite;
        sprenderer.enabled = false;
        alive = false;
        canAttack = false;
        isFollowing = false;
        transform.position = restPosition;
    }
}
