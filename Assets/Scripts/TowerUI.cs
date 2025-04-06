using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUI : MonoBehaviour
{
    public static TowerUI instance;

    public GameObject towerOptionsUI;
    public GameObject towerBuyUI;

    public Button sellButton;
    public Button upgradeDamageButton;
    public Button upgradeSpeedButton;

    public TMP_Text damageCostText;
    public TMP_Text speedCostText;

    private GameObject selectedTower;
    private TowerSlot currentSlot;

    private void Awake()
    {
        instance = this;
        towerOptionsUI.SetActive(false);
    }

    public void ShowOptions(GameObject tower, TowerSlot slot)
    {
        selectedTower = tower;
        currentSlot = slot;

        if (towerOptionsUI != null)
            towerOptionsUI.SetActive(true);

        if (towerBuyUI != null)
            towerBuyUI.SetActive(false);

        UpdateCostTexts();

        sellButton.gameObject.SetActive(true);
        upgradeDamageButton.gameObject.SetActive(true);
        upgradeSpeedButton.gameObject.SetActive(true);
    }

    public void HideOptions()
    {
        selectedTower = null;
        currentSlot = null;

        if (towerOptionsUI != null)
            towerOptionsUI.SetActive(false);

        if (towerBuyUI != null)
            towerBuyUI.SetActive(true);
    }

    public void OnSellPressed()
    {
        if (selectedTower == null || currentSlot == null) return;

        TowerData data = selectedTower.GetComponent<TowerData>();
        if (data != null)
        {
            int refund = Mathf.RoundToInt(data.coffeeCost * 0.7f);
            GameManager.main.AddCoffee(refund);

            if (!string.IsNullOrEmpty(data.musicID) && MusicManager.instance != null)
            {
                MusicManager.instance.UnregisterTower(data.musicID);
            }
        }

        Destroy(selectedTower);
        currentSlot.occupied = false;
        HideOptions();
    }

    public void OnUpgradeDamage()
    {
        if (selectedTower == null) return;

        TowerData data = selectedTower.GetComponent<TowerData>();
        if (data != null)
        {
            if (GameManager.main.SpendCoffee(data.upgradeDamageCost))
            {
                data.baseDamage += 1;
                data.upgradeDamageCost = Mathf.RoundToInt(data.upgradeDamageCost * 1.5f);
                UpdateCostTexts();
                Debug.Log("Damage upgraded to: " + data.baseDamage);
            }
            else
            {
                Debug.Log("Not enough coffee for damage upgrade.");
            }
        }
    }

    public void OnUpgradeSpeed()
    {
        if (selectedTower == null) return;

        TowerData data = selectedTower.GetComponent<TowerData>();
        if (data != null)
        {
            if (GameManager.main.SpendCoffee(data.upgradeFireRateCost))
            {
                data.baseFireRate *= 1.05f;
                data.upgradeFireRateCost = Mathf.RoundToInt(data.upgradeFireRateCost * 1.5f);
                UpdateCostTexts();
                Debug.Log("Fire rate upgraded to: " + data.baseFireRate);
            }
            else
            {
                Debug.Log("Not enough coffee for fire rate upgrade.");
            }
        }
    }

    private void UpdateCostTexts()
    {
        if (selectedTower == null) return;

        TowerData data = selectedTower.GetComponent<TowerData>();
        if (data != null)
        {
            damageCostText.text = "Cost: " + data.upgradeDamageCost;
            speedCostText.text = "Cost: " + data.upgradeFireRateCost;
        }
    }
}
