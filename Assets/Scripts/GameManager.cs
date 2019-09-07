using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text healthText;
    public Text timeText;
    public float maxCastleHealth;
    public float currentCastleHealth;

    private void Start()
    {
        currentCastleHealth = maxCastleHealth;
        ShowCastleHealthText();
        ShowTimeText();
    }

    private void Update() {
        ShowTimeText();
    }

    public void DamageCastle(float damage)
    {
        currentCastleHealth -= damage;
        ShowCastleHealthText();

        if(currentCastleHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void ShowCastleHealthText()
    {
        healthText.text = string.Format("Health: {0}/{1}", currentCastleHealth, maxCastleHealth);
    }

    private void ShowTimeText() {
        timeText.text = string.Format("{0:0.0} seconds", Time.time);
    }
}
