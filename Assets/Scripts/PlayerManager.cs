using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// need to use techXr Core Sense
using TechXR.Core.Sense;

public class PlayerManager : MonoBehaviour
{
    // reference instance from shooting script
    public Shooting shooting;

    // referencing sensecontroller
    public SenseController senseController;

    // referencing laserpointer game object
    public GameObject laserPointer;

    // referencing player health
    private Health playerHealth;

    // getting start position of player
    public Transform startPosition;

    // toggling our main menu
    public GameObject mainMenu;

    // referencing enemy container
    public Transform enemyContainer;

    // creating a bool to know when the game is started
    bool inGame;

    // referencing a variable to start game objects
    // to be instantiated at game start only
    public GameObject startGameAssets;

    // referencing score manager script
    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        // initializing health variable
        playerHealth = GetComponent<Health>();

        // assigning start position of player to the variable
        startPosition.position = transform.position;

        // assigning start rotation of player to the variable
        startPosition.rotation = transform.rotation;

        // triggering set highscore function when before game starts
        scoreManager.SetHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        // updated the logic to shoot only if we are ingame
        if(SenseInput.GetButton(ButtonName.L) && inGame)
        {
            shooting.Shoot();
        }

        // when player health drops to 0 we will trigger GameOver
        // When we start the game we need to reset the player health
        // so for this lets create another function called Reset
        // Health in the Health script
        if(playerHealth.health <= 0)
        {
            GameOver();
        }
    }

    public void GameStart()
    {
        // when game starts we will set ingame bool to true
        inGame = true;
        // when game starts toggle the main menu to false meaning not visible 
        mainMenu.SetActive(false);

        // when we start game joystick movement set to true
        // similarly can do for teleport movement also
        senseController.ToggleJoystickMovement(true);

        // we have shooting so toggle it on only once game starts
        shooting.gameObject.SetActive(true);
        
        // when game starts laser pointer will be off
        laserPointer.gameObject.SetActive(false);

        // when game starts we need these to be activated
        startGameAssets.SetActive(true);

        // triggering Reset score function when game starts
        // so user can see their current score
        scoreManager.ResetScore();
    }

    public void GameOver()
    {
        // when game stops we will set ingame bool to false
        inGame = false;

        // show main menu when game is over
        mainMenu.SetActive(true);

        senseController.ToggleJoystickMovement(false);
        shooting.gameObject.SetActive(false);
        // when game is over laser pointer will be visible
        laserPointer.gameObject.SetActive(true);

        // whenever you set the laserPointer to true , turn buttonstate to false
        laserPointer.GetComponent<LaserPointer>().ButtonState = false;

        // calling reset health from Health.cs
        playerHealth.resetHealth();

        // assigning the start position back to player at game over
        transform.position = startPosition.position;

        // assigning the start position back to player at game over
        transform.rotation = startPosition.rotation;

        // on game over destroy all enemies in the scene
        // when we clicked play we found that the enmy Death function
        // called here did not trigger for all spawned enemies
        // this was because the enemy Death function has an if condition
        // hence an update was required to the logic in the enemyAI script
        // as well
        foreach (Transform child in enemyContainer)
            child.gameObject.GetComponent<EnemyAI>().EnemyDeath();

        // when game ends we need these to be deactivated
        startGameAssets.SetActive(false);

        // triggering set highscore function when game starts
        scoreManager.SetHighScore();
    }

}
