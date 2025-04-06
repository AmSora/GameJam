using UnityEngine;

public class TowerSlot : MonoBehaviour
{
    [Header("Slot Configuration")]
    public bool canBuild = true;
    public bool occupied = false;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        HideHighlight();
    }

    public void ShowHighlight()
    {
        if (!occupied && canBuild)
            sr.color = Color.green;
        else
            sr.color = Color.red;
    }

    public void HideHighlight()
    {
        sr.color = Color.white;
    }
}
