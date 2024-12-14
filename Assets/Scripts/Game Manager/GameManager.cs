using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TMP_Text targetText;
    public TMP_Text timeText;

    public int targetGoal = 20; // Change target goal to 20
    public float timeLimit = 120f;
    private int targetsShot = 0;
    private float timeRemaining;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            timeRemaining = timeLimit;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timeRemaining -= Time.deltaTime;
        UpdateTimeText();
        if (timeRemaining <= 0)
        {
            HandleTimeOut();
        }
    }

    public void AddKill(int points)
    {
        targetsShot++;
        UpdateTargetText();
        if (targetsShot >= targetGoal)
        {
            HandleGameWin();
        }
    }

    private void UpdateTargetText()
    {
        targetText.text = "Targets Shot: " + targetsShot + "/" + targetGoal;
    }
    private void UpdateTimeText()
    {
        timeText.text = "Time Left: " + Mathf.Ceil(timeRemaining) + "s";
    }

    private void HandleGameWin()
    {
        SceneManager.LoadScene("Win");
    }

    private void HandleTimeOut()
    {
        SceneManager.LoadScene("Lose_Time");
    }

    public void HandlePlayerDeath()
    {
        SceneManager.LoadScene("Lose_Died");
    }
}