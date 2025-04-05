using UnityEngine;

public class MountedGun : MonoBehaviour
{
    public Transform barrel; // Referencia al cañón
    public bool aimAtMouse = true; // Puedes desactivarlo si quieres que apunte a otra cosa

    void Update()
    {
        if (aimAtMouse)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mouseWorldPos - barrel.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            barrel.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
