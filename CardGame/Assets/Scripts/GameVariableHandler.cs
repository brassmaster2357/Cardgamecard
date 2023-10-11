using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
How to include the global variables into a script:
1. Declare a public GameObject, this will host the object with the GameVariableHandler script. (ex: gameVarHandler)
2. Declare a private GameVariableHandler named something that is easily written. (ex: vars)
3. In Start(), the first line should be: vars = gameVarHandler.GetComponent<GameVariableHandler>();
4. Now, type "vars." to access any global variables.
*/
public class GameVariableHandler : MonoBehaviour
{
    private static GameVariableHandler instance;
    void Awake()
    {
        // Check if there is already a persisting GameVariableHandler object.
        if (instance != null && instance != this)
        {
            // Destroy this object since it is a duplicate.
            Destroy(gameObject);
        }
        else
        {
            // This is the first GameVariableHandler to load, so it will persist over the entire game.
            instance = this;
            DontDestroyOnLoad(transform.root);
        }
    }

    public float playerHP;
    public float playerHPMax;

    public float enemy1HP;
    public float enemy1HPMax;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
