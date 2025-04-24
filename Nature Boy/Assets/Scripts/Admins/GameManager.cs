using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] PlayerController player;
    public static event Action<int> OnPlayerLifeChange;
    public static event Action<int> OnLeafPickedUp;
    public static event Action OnCollectiblePickedUp;
    private int currentCollectibles = 0;
    private int currentNatureValue = 0;


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
    #region Crystal
    public void CrystalPickedUp()
    {
        currentCollectibles++;
        OnCollectiblePickedUp?.Invoke();
    }

    public int GetCrystalCount()
    {
        return currentCollectibles;
    }

    public void RemoveCrystals(int count)
    {
        currentCollectibles = currentCollectibles - count;
        OnCollectiblePickedUp?.Invoke();
    }
    #endregion
    #region Flower
    public void PicketUpFlower()
    {
        player.Heal(1);
    }

    public void PickedUpLeaf()
    {
        currentNatureValue++;
        OnLeafPickedUp?.Invoke(currentNatureValue);
    }
    #endregion
    public void UpdatePlayerLife(int currentHealth)
    {
        OnPlayerLifeChange?.Invoke(currentHealth);
    }
}
