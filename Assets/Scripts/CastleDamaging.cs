using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDamaging : MonoBehaviour
{
    public float damage;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DamageCastle()
    {
        gameManager.DamageCastle(damage);
    }
}
