using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject selectedTower;
    public GameObject currentPreview;
    public TMP_Text[] coffeeTexts;

    private TowerSlot[] slots;
    private bool justPlacedTower = false;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawRay(mousePos, Vector2.up * 0.5f, Color.red, 1f);

        // Evitar abrir el panel justo después de colocar una torre
        if (justPlacedTower)
        {
            justPlacedTower = false;
            return;
        }

        // Click izquierdo para abrir menú si tocamos torre
        if (Input.GetMouseButtonDown(0))
        {
            GameObject tower = GetTowerAtPosition(mousePos);
            if (tower != null)
            {
                TowerSlot slot = FindSlotForTower(tower);
                if (slot != null)
                {
                    TowerUI.instance.ShowOptions(tower, slot);
                    return;
                }
            }
        }

        // Click derecho para vender o cerrar panel
        if (Input.GetMouseButtonDown(1))
        {
            GameObject tower = GetTowerAtPosition(mousePos);
            if (tower != null)
            {
                TowerSlot slot = FindSlotForTower(tower);
                if (slot != null)
                {
                    TowerData data = tower.GetComponent<TowerData>();
                    if (data != null)
                    {
                        int refund = Mathf.RoundToInt(data.coffeeCost * 0.7f);
                        GameManager.main.AddCoffee(refund);

                        if (!string.IsNullOrEmpty(data.musicID) && MusicManager.instance != null)
                        {
                            MusicManager.instance.UnregisterTower(data.musicID);
                        }
                    }

                    Destroy(tower);
                    slot.occupied = false;
                }
            }
            else
            {
                TowerUI.instance.HideOptions(); // cerrar panel si clic derecho no fue en torre
            }
        }
    }

    GameObject GetTowerAtPosition(Vector2 pos)
    {
        Collider2D hit = Physics2D.OverlapPoint(pos);
        if (hit != null && hit.CompareTag("Tower"))
        {
            return hit.gameObject;
        }
        return null;
    }

    TowerSlot FindSlotForTower(GameObject tower)
    {
        TowerSlot[] slots = FindObjectsOfType<TowerSlot>();
        foreach (TowerSlot slot in slots)
        {
            if (slot.occupied && Vector2.Distance(slot.transform.position, tower.transform.position) < 1.0f)
            {
                return slot;
            }
        }
        return null;
    }

    public void SelectTower(GameObject prefab)
    {
        selectedTower = prefab;

        if (currentPreview != null)
            Destroy(currentPreview);

        currentPreview = Instantiate(prefab);
        currentPreview.tag = "Untagged";
        currentPreview.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);

        TowerShot shot = currentPreview.GetComponent<TowerShot>();
        if (shot != null) shot.enabled = false;

        slots = FindObjectsOfType<TowerSlot>();
        foreach (var slot in slots) slot.ShowHighlight();
    }

    public void ClearSelection()
    {
        selectedTower = null;

        if (currentPreview != null)
        {
            Destroy(currentPreview);
            currentPreview = null;
        }

        if (slots == null) return;
        foreach (var slot in slots) slot.HideHighlight();

        justPlacedTower = true;
    }

    public void UpdateCoffeeUI(int amount)
    {
        foreach (TMP_Text t in coffeeTexts)
        {
            if (t != null)
                t.text = "Coffee: " + amount.ToString();
        }
    }
}
