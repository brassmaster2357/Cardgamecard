using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsScript : MonoBehaviour
{
    public GameObject gameVarHandler;
    private GameVariableHandler vars;

    private Vector3 moveTo;
    public Vector3 restPosition = new Vector3(0,-4,0);
    public GameObject player;
    public GameObject enemy1;
    public float detectionRange = 2;
    private bool isFollowing;

    // Start is called before the first frame update
    void Start()
    {
        vars = gameVarHandler.GetComponent<GameVariableHandler>();
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
            //Card played on enemy 1 (furthest to the right)
            Debug.Log("You have played the test card on enemy1");
            vars.enemy1HP -= 5;
        }
        else if (Mathf.Abs(gameObject.transform.position.x - player.transform.position.x) <= detectionRange && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) <= detectionRange)
        {
            //Card played on player
            Debug.Log("You have played the test card on yourself");
            vars.playerHP -= 5;
        }
        else if (gameObject.transform.position.y >= -1.5)
        {
            //Card played "in the field"
            Debug.Log("You have played the card without targeting anyone, or put it back (if it needed a target)");
        }
        else
        {
            //Card dropped near the hand, do nothing
            Debug.Log("You have put the card back");
        }
    }
}
