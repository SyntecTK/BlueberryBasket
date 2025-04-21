using UnityEngine;

public class MeleeRotation : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector2 mousePos;

    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
