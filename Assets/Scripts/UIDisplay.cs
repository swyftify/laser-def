using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;

    [SerializeField]
    TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    Health playerHealth;

    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        playerHealth = FindObjectOfType<Player>().GetComponent<Health>();
    }

    void Start()
    {
        healthBar.maxValue = playerHealth.GetHealth();
    }

    void Update()
    {
        while (playerHealth.GetHealth() > 0)
        {
            scoreText.text = scoreKeeper.GetScore().ToString("000000000");
            healthBar.value = playerHealth.GetHealth();
        }
    }
}
