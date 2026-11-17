using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth = 5;
    public Image healthFill;
    private int health;
    private bool isDead;

    private void Start()
    {
        health = maxHealth;
        UpdateBar();
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        health -= amount;
        UpdateBar();
        Debug.Log("Player health: " + health);

        if (health <= 0)
            Die();
    }

    private void UpdateBar()
    {
        if (healthFill != null)
            healthFill.fillAmount = (float)health / maxHealth;
    }

    private void Die()
    {
        isDead = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}