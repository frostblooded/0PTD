using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text healthText;
    public float maxCastleHealth;
    public float currentCastleHealth;

    private void Start()
    {
        currentCastleHealth = maxCastleHealth;
        ShowCastleHealthText();
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
        healthText.text = "Castle health: " + currentCastleHealth + "/" + maxCastleHealth;
    }
}
