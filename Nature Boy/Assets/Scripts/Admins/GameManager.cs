using System;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] PlayerController player;
    public static event Action<int> OnPlayerLifeChange;
    public static event Action<int> OnLeafPickedUp;
    public static event Action OnCollectiblePickedUp;
    public static event Action GameOver;
    private int currentCollectibles = 0;
    private int currentNatureValue = 0;
    private int neededNatureValue = 20;
    private int babyCount = 0;

    public bool GameWon => gameWon;
    private bool gameWon = false;


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
    public void PickedUpFlower()
    {
        player.Heal(1);
    }

    public void PickedUpLeaf()
    {
        currentNatureValue++;
        OnLeafPickedUp?.Invoke(currentNatureValue);
        if(currentNatureValue == neededNatureValue)
        {
            gameWon = true;
            GameOver?.Invoke();
        }
    }
    #endregion
    public void UpdatePlayerLife(int currentHealth)
    {
        OnPlayerLifeChange?.Invoke(currentHealth);
        if(currentHealth <= 0)
        {
            GameOver?.Invoke();
        }
    }

    public int RegisterBaby()
    {
        babyCount++;
        return babyCount;
    }
}
