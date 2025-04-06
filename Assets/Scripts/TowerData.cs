using UnityEngine;

public class TowerData : MonoBehaviour
{
    [Header("Basic Cost")]
    public int coffeeCost = 100;

    [Header("Upgrade Values")]
    public int baseDamage = 1;
    public float baseFireRate = 1f;

    [Header("Upgrade Costs")]
    public int upgradeDamageCost = 200;
    public int upgradeFireRateCost = 150;

    [Header("Tower Info")]
    public int towerTypeID;
    public string musicID;
}
