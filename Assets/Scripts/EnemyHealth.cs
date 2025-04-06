using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Base Stats")]
    public int baseHealth = 1;
    [SerializeField] private int baseReward = 15;

    private int currentHealth;
    private int coffeeReward;

    public int enemyTypeID;

    public void Initialize(int currentWave)
    {
        currentHealth = baseHealth + currentWave ;
        coffeeReward = baseReward + (currentWave * 2);
        Debug.Log($"Enemigo creado con vida: {currentHealth} y recompensa: {coffeeReward}");
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.main.AddCoffee(coffeeReward);
        Destroy(gameObject);
    }
}
