using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsScript : MonoBehaviour
{
    private Vector3 moveTo;
    public Vector3 restPosition = new Vector3(0,-4,0);
    private bool isFollowing;
    public CardTemplate card;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = card.art;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
            moveTo = (gameObject.transform.position + Camera.main.ScreenToWorldPoint(Input.mousePosition)) / 2;
        else
            moveTo = (gameObject.transform.position + restPosition) / 2;
        moveTo.z = 0;
        gameObject.transform.position = moveTo;
    }

    void OnMouseDown()
    {
        isFollowing = true;
        Debug.Log("You have picked up the card");
    }
    void OnMouseUp()
    {
        isFollowing = false;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!isFollowing)
        {
            card.target = collision.gameObject;
            switch (card.target.name)
            {
                case "Player":
                    card.target.GetComponent<PlayerManager>().playerHP += card.power;
                    Destroy(gameObject);
                    break;
                case "Enemy":
                    card.target.GetComponent<EnemyManager>().enemyHP += card.power;
                    Destroy(gameObject);
                    break;
                default:
                    break;
            }
            Debug.Log(collision.gameObject);
        }
    }
}
