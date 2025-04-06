using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public int maxHealth = 3;
    private int currentHealth;

    public int currentCoffee = 500;

    private void Awake()
    {
        if (main == null)
            main = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        currentHealth = maxHealth;
        UIManager.instance.UpdateCoffeeUI(currentCoffee);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"Received {amount} damage! Remaining health: {currentHealth}");

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    public bool SpendCoffee(int amount)
    {
        if (currentCoffee >= amount)
        {
            currentCoffee -= amount;
            Debug.Log("Spent " + amount + " coffee. Remaining: " + currentCoffee);
            UIManager.instance.UpdateCoffeeUI(currentCoffee);
            return true;
        }
        else
        {
            Debug.Log("Not enough coffee.");
            return false;
        }
    }

    public void AddCoffee(int amount)
    {
        currentCoffee += amount;
        Debug.Log("Added " + amount + " coffee. Total: " + currentCoffee);
        UIManager.instance.UpdateCoffeeUI(currentCoffee);
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER!");
        Time.timeScale = 0;
    }
}
