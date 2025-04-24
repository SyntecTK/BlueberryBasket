using System.Collections;
using TMPro;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [Header ("Stats")]
    [SerializeField] float health;
    [SerializeField] private float interactionRange = 5f;
    [SerializeField] private GameObject interactionPrompt;
    [Header("Collectibles")]
    [SerializeField] private int requiredCrystals = 3;
    [SerializeField] private GameObject flowerPrefab;
    [SerializeField] private GameObject leafPrefab;
    [SerializeField] private int collectibleCount;
    [Header("Sprites/Animations"), SerializeField] private Sprite destroyedSprite;
    [SerializeField] private Sprite treeSprite;
    [SerializeField] private Transform treePos;
    [SerializeField] private GameObject plantGrowAnimationPrefab;
    [SerializeField] private GameObject explosionPrefab;


    private Transform player;
    private bool isInRange = false;
    private bool isDestroyed = false;
    private bool isRestored = false;
    private TMP_Text interactionText;
    private TMP_Text tipText;
    private string originalTipText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        TMP_Text[] textComponents = interactionPrompt.GetComponentsInChildren<TMP_Text>();
        interactionText = textComponents[0];
        tipText = textComponents[1];
        originalTipText = tipText.text;
    }

    private void Update()
    {
        Vector2 toPlayer = player.position - transform.position;
        float distance = toPlayer.magnitude;

        if (distance < interactionRange && isDestroyed)
        {
            isInRange = true;
            interactionText.text = requiredCrystals.ToString();
            if(!isRestored)
            {
                interactionPrompt.SetActive(true);
            }
            if(Input.GetMouseButtonDown(1))
            {
                TryInteract();
            }
        }
        else
        {
            isInRange = false;
            interactionPrompt.SetActive(isInRange);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0 && !isDestroyed)
        {
            isDestroyed = true;
            //GetComponent<BoxCollider2D>().enabled = false;
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            explosion.GetComponent<DestroyAnimation>().PlayBuildingAnimation();
            ChangeToDestroyedSprite();
        }
    }

    private void TryInteract()
    {
        if(GameManager.Instance.GetCrystalCount() >= requiredCrystals)
        {
            isRestored = true;
            GameManager.Instance.RemoveCrystals(requiredCrystals);
            GetComponent<SpriteRenderer>().sprite = treeSprite;
            interactionPrompt.SetActive(false);

            for(int i = 0; i < requiredCrystals; i++)
            {
                GameObject prefabToSpawn = Random.value < 0.5f ? flowerPrefab : leafPrefab;
                Instantiate(prefabToSpawn, treePos.position, Quaternion.identity);
            }
        }
        else
        {
            tipText.text = "Not enough Crystals";
            StartCoroutine(SwitchText());
        }
    }

    private void ChangeToDestroyedSprite()
    {
        GetComponent<SpriteRenderer>().sprite = destroyedSprite;
    }

    IEnumerator SwitchText()
    {
        yield return new WaitForSeconds(2f);
        tipText.text = originalTipText;
    }
}
