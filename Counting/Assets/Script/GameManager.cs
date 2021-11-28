using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;   
    public TextMeshProUGUI timer;
    public GameObject endGameOption;
    public GameObject mainMenu;
    public GameObject inGameUi;
    public GameObject box;
    public GameObject pauseScreen;
    private bool isPaused;

    public int time = 90;
    public float gravityModifier;


    public bool gameOver;

    // Start is called before the first frame update
    
    public void StartGame(int difficulty)
    {//when difficulty button is clicked
        
        //active box in game scene
        box.SetActive(true);
        box.GetComponent<BoxControl>().speed %= difficulty;
        
        //close main menu and open inGameUi
        mainMenu.SetActive(false);
        inGameUi.SetActive(true);

        //initiate variable 
        gameOver = false;
        Physics.gravity = new Vector3(0,-9.81f,0)* gravityModifier;

        //create a player(ball)
        Instantiate(player, player.transform.position, player.transform.rotation);
        
        //start to count time 
        InvokeRepeating("CountTime", 0, 1);        
    }



    private void Update()
    {
        //if time =0, game over
        GameOver();
       
    }
    public void SpawnBall()
    {
        
        Instantiate(player, player.transform.position, player.transform.rotation);
    }

    void CountTime()
    {
        if (time > 0)
        {
            time -= 1;
        }        
        timer.text = "time: " + string.Format("{0:00}:{1:00}", time / 60, time % 60);
    }

    void GameOver()
    {
        if (time == 0)
        {
           
            endGameOption.SetActive(true);
            gameOver = true;
        }
    }

    public void RestartGame()
    {
        CheckPaused();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }   

    public void CheckPaused()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }

    }
}
