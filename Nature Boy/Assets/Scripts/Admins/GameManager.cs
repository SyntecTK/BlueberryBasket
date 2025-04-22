using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action OnCollectiblePickedUp;
    private int currentCollectibles = 0;

    public static event Action<int> OnPlayerDamaged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple GameManagers in scene. Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentCollectibles = 0;
    }

    public void CollectiblePickedUp()
    {
        currentCollectibles++;
        OnCollectiblePickedUp?.Invoke();
    }

    public int GetCollectibleCount()
    {
        return currentCollectibles;
    }

    public void PlayerTookDamage(int currentHealth)
    {
        OnPlayerDamaged?.Invoke(currentHealth);
    }
}
