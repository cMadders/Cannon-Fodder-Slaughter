using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float horizontalBound = 15.0f;
    public float currentAmmo;
    public float startingAmmo = 20;
    public int HighScore;
    public int currentScore;
    public int PreviousScore;

    public Text ammo;
    public Text HighScoreText;
    public Text PreviousScoreText;

    // Start is called before the first frame update
    void Start()
    {
        ammo.text = "Ammo: " + startingAmmo;
        HighScore = DataManager.Instance.HighScore;
        currentScore = 0;
        PreviousScore = DataManager.Instance.PreviousScore;
        HighScoreText.text = "High Score: " + DataManager.Instance.HighScore;
        PreviousScoreText.text = "Previous Score: " + DataManager.Instance.PreviousScore;
        currentAmmo = startingAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        DataManager.Instance.PreviousScore = currentScore;

        if (currentScore > HighScore)
        {
            DataManager.Instance.HighScore = currentScore;
            DataManager.Instance.SaveScore();
        }

        SceneManager.LoadScene(0);
    }

    public bool checkEndStatus()
    {
        if(currentAmmo == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void shotFired()
    {
        if(currentAmmo > 0)
        {
            currentAmmo--;
            ammo.text = "Ammo: " + currentAmmo;
        }
    }
}
