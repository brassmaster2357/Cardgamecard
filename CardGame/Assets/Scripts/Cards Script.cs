using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsScript : MonoBehaviour
{
    private Vector3 moveTo;
    public Vector3 restPosition = new Vector3(0,-4,0);
    public GameObject player;
    public GameObject enemy1;
    public float detectionRange = 2;
    private bool isFollowing;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Mathf.Abs(gameObject.transform.position.x - enemy1.transform.position.x) <= detectionRange && Mathf.Abs(gameObject.transform.position.y - enemy1.transform.position.y) <= detectionRange)
        {
            Debug.Log("You have played the test card on the enemy");
        }
        else if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) <= detectionRange && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) <= detectionRange)
        {
            Debug.Log("You have played the test card on yourself");
            player.GetComponent<PlayerManager>().playerHP--;
        }
        else if (gameObject.transform.position.y >= -1.5)
        {
            Debug.Log("You have played the card without targeting anyone, or put it back (if it needed a target)");
        }
        else
        {
            Debug.Log("You have put the card back");
        }
    }
}
