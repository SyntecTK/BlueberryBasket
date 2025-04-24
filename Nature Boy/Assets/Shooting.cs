using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float bulletSpeed = 20f;

    [SerializeField] private float _timeBetweenShots;
    private bool _shootContinuously;
    private bool _fireSingle;
    private float _lastFireTime;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject crosshair;
    private Vector2 mousePos;

    private void Update()
    {
        if(mainCamera != null)
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        crosshair.transform.position = mousePos;

        if (_shootContinuously || _fireSingle)
        {
            float timeSinceLastFire = Time.time - _lastFireTime;

            if (timeSinceLastFire >= _timeBetweenShots)
            {
                FireBullet();

                _lastFireTime = Time.time;
                _fireSingle = false;
            }
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().SetOriginObject(player);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.linearVelocity = bulletSpeed * firePoint.up;
    }

    private void OnAttack(InputValue inputValue)
    {
        _shootContinuously = inputValue.isPressed;

        if (inputValue.isPressed)
        {
            _fireSingle = true;
        }
    }
}
