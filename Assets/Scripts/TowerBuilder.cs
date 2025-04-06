using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{
    void Update()
    {
        if (UIManager.instance.selectedTower != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UIManager.instance.currentPreview.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

            if (Input.GetMouseButtonDown(0))
            {
                TowerSlot slot = GetSlotAtPosition(mousePos);

                if (slot != null && !slot.occupied && slot.canBuild)
                {
                    GameObject tower = UIManager.instance.selectedTower;
                    TowerData data = tower.GetComponent<TowerData>();

                    if (data != null && GameManager.main.SpendCoffee(data.coffeeCost))
                    {
                        GameObject newTower = Instantiate(tower, slot.transform.position, Quaternion.identity);
                        newTower.tag = "Tower";
                        slot.occupied = true;

                        if (!string.IsNullOrEmpty(data.musicID) && MusicManager.instance != null)
                        {
                            MusicManager.instance.RegisterTower(data.musicID);
                        }

                        UIManager.instance.ClearSelection();
                    }
                    else
                    {
                        Debug.Log("Not enough coffee to place this tower.");
                    }
                }
                else
                {
                    // Cancelar solo si NO clickeamos sobre UI ni sobre una torre
                    if (!EventSystem.current.IsPointerOverGameObject())
                    {
                        GameObject clickedTower = GetTowerAtPosition(mousePos);
                        if (clickedTower == null)
                        {
                            UIManager.instance.ClearSelection();
                        }
                    }
                }
            }
        }
    }

    private TowerSlot GetSlotAtPosition(Vector2 pos)
    {
        TowerSlot[] slots = FindObjectsOfType<TowerSlot>();
        foreach (TowerSlot slot in slots)
        {
            if (Vector2.Distance(slot.transform.position, pos) <= 0.4f)
            {
                return slot;
            }
        }
        return null;
    }

    private GameObject GetTowerAtPosition(Vector2 pos)
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");
        foreach (GameObject tower in towers)
        {
            if (Vector2.Distance(tower.transform.position, pos) < 1.0f)
            {
                return tower;
            }
        }
        return null;
    }
}
