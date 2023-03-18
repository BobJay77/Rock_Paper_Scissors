using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class GameMaster : MonoBehaviour
{
    int playerInput = 0;
    public Text resultText;
    public Sprite[] images;
    public Image enemyIcon;
    public AudioManager audioManager;

    bool gameStart = true;
    public GameObject ProbabilityFile;
    public GameObject buttons;
    
    //Player match history
    public int totalNumberOfGames = 0;
    public int numberOfGamesWon = 0;
    public int numberOfGamesTied = 0;
    public int numberOfGamesLost = 0;
    public Text MHwins;
    public Text MHTies;
    public Text MHloses;


    // AI Stats
    public int numberOfTimesAIPlayedRock = 0;
    public int numberOfTimesAIPlayedPaper = 0;
    public int numberOfTimesAIPlayedScissor = 0;
    public Text AIplayedRock;
    public Text AIplayedPaper;
    public Text AIplayedScissors;

    //Player Stats
    public int numberOfTimesPlayerPlayedRock = 0;
    public int numberOfTimesPlayerPlayedPaper = 0;
    public int numberOfTimesPlayerPlayedScissor = 0;
    public Text PlayerplayedRock;
    public Text PlayerplayedPaper;
    public Text PlayerplayedScissors;

    public void PlayerButtonPressed(int button)
    {
        if (gameStart)
        {
            gameStart = false;
            playerInput = button;

            if (playerInput == 1)
            {
                numberOfTimesPlayerPlayedRock++;
            }
            else if (playerInput == 2)
            {
                numberOfTimesPlayerPlayedPaper++;
            }
            else
            {
                numberOfTimesPlayerPlayedScissor++;
            }

            AIButtonPressed();
        }
    }

    public void GoToAndFromProbabilityFile(bool back)
    {
        ProbabilityFile.SetActive(back);
        buttons.SetActive(!back);
    }

    private void AIButtonPressed()
    {
        int aiChoice = UnityEngine.Random.Range(1, 4);

        if (aiChoice == 1)
        {
            enemyIcon.sprite = images[0];
            numberOfTimesAIPlayedRock++;
        }
        else if (aiChoice == 2)
        {
            enemyIcon.sprite = images[1];
            numberOfTimesAIPlayedPaper++;
        }

        else if(aiChoice == 3)
        {
            enemyIcon.sprite = images[2];
            numberOfTimesAIPlayedScissor++;
        }
        print(aiChoice);
        Result(aiChoice);
    }

    private void Result(int aichoice)
    {
        resultText.gameObject.SetActive(true);
        if (playerInput == aichoice)
        {
            resultText.text = "You tied with the AI";
            numberOfGamesTied++;
            totalNumberOfGames++;
            audioManager.Play("Tie");
        }

        else if (playerInput == 1)
        {
            if (aichoice == 2)
            {
                resultText.text = "You lost against the AI";
                numberOfGamesLost++;
                totalNumberOfGames++;
                audioManager.Play("Lose");
            }
            else if (aichoice == 3)
            {
                resultText.text = "You won against the AI";
                numberOfGamesWon++;
                totalNumberOfGames++;
                audioManager.Play("Win");
            }
        }
        else if (playerInput == 2)
        {
            if (aichoice == 1)
            {
                resultText.text = "You won against the AI";
                numberOfGamesWon++;
                totalNumberOfGames++;

                audioManager.Play("Win");
            }
            else if (aichoice == 3)
            {
                resultText.text = "You lost against the AI";
                numberOfGamesLost++;
                totalNumberOfGames++;
                audioManager.Play("Lose");
            }
        }

        else if (playerInput == 3)
        {
            if (aichoice == 2)
            {
                resultText.text = "You won against the AI";
                numberOfGamesWon++;
                totalNumberOfGames++;
                audioManager.Play("Win");
            }
            else if (aichoice == 1)
            { 
                resultText.text = "You lost against the AI";
                numberOfGamesLost++;
                totalNumberOfGames++;
                audioManager.Play("Lose");
            }
        }

        UpdateTableValues();
        Invoke("resetTheAIIcon", 1.5f);

    }

    public void Exit()
    {
        Application.Quit();
    }

    private void resetTheAIIcon()
    {
        gameStart = true;
        enemyIcon.sprite = images[3];
    }

    public void UpdateTableValues()
    {
        MHwins.text = (Mathf.Round((float)(numberOfGamesWon / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";
        MHloses.text = (Mathf.Round((float)(numberOfGamesLost / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";
        MHTies.text = (Mathf.Round((float)(numberOfGamesTied / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";

        AIplayedRock.text = (Mathf.Round((float)(numberOfTimesAIPlayedRock / (float)totalNumberOfGames)*1000)/ 10).ToString() + "%";
        AIplayedPaper.text = (Mathf.Round((float)(numberOfTimesAIPlayedPaper / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";
        AIplayedScissors.text = (Mathf.Round((float)(numberOfTimesAIPlayedScissor / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";

        PlayerplayedRock.text = (Mathf.Round((float)(numberOfTimesPlayerPlayedRock / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";
        PlayerplayedPaper.text = (Mathf.Round((float)(numberOfTimesPlayerPlayedPaper / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";
        PlayerplayedScissors.text = (Mathf.Round((float)(numberOfTimesPlayerPlayedScissor / (float)totalNumberOfGames) * 1000) / 10).ToString() + "%";
    }

    public void RedoButton()
    {
        playerInput = 0;
        resultText.gameObject.SetActive(false);
 
        numberOfGamesLost = 0;
        numberOfGamesTied = 0;
        numberOfGamesWon = 0;

        numberOfTimesAIPlayedPaper = 0;
        numberOfTimesAIPlayedRock = 0;
        numberOfTimesAIPlayedScissor = 0;

        numberOfTimesPlayerPlayedPaper = 0;
        numberOfTimesPlayerPlayedRock = 0;
        numberOfTimesPlayerPlayedScissor = 0;

        UpdateTableValues();

        totalNumberOfGames = 0;

        enemyIcon.sprite = images[3];
    }
}
