using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    private BaseCharacter player;
    public GameObject shopPanel;
    public TMP_Text rerollCostText;
    public Image itemDisplay;

    public Sprite breadSprite;
    public Sprite beerSprite;
    public Sprite healthSprite;
    public Sprite energySprite;
    public Sprite hungerSprite;

    private Sprite[] itemSprites;

    private int rerollCount = 0;
    private int rerollCost = 0;
    private bool isRolling = false;

    [SerializeField] private AnimationCurve spinCurve;

    private void Start()
    {
        shopPanel.SetActive(false); // Hide the shop on start
        itemSprites = new Sprite[] { breadSprite, beerSprite, healthSprite, energySprite, hungerSprite };
        UpdateShopUI();
    }

    public void ToggleShop()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
        if (shopPanel.activeSelf )
        {
            Debug.Log("Shop opened!");
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Debug.Log("Shop closed!");
            Time.timeScale = 1f; // Resume the game
        }
        UpdateShopUI();
    }

    public void TryReroll()
    {
        if (isRolling) return;

        Debug.Log("TryReroll clicked!");

        if (InventoryManager.Instance.coinCount >= rerollCost)
        {
            InventoryManager.Instance.UseCoin(rerollCost);

            rerollCount++;

            if (rerollCount % 2 == 0) // Every 2 rolls
            {
                rerollCost += 1; // Increase reroll cost by 1
            }

            UpdateShopUI();

            StartCoroutine(RerollRoutine());
        }
        else
        {
            Debug.Log("Not enough coins to reroll!");
        }
    }

    IEnumerator RerollRoutine()
    {
        Debug.Log("Started RerollRoutine!");

        isRolling = true;

        float rollDuration = 1.5f; // Spin for 2 seconds
        int totalSpins = 20;     // Show 10 images
        float interval = rollDuration / totalSpins;

        for (int i = 0; i < totalSpins; i++)
        {
            Sprite randomSprite = itemSprites[Random.Range(0, itemSprites.Length)];
            itemDisplay.sprite = randomSprite;

            yield return new WaitForSecondsRealtime(interval); // Wait fixed time between spins
        }

        // After spin, pick a final result
        int resultIndex = Random.Range(0, itemSprites.Length);
        itemDisplay.sprite = itemSprites[resultIndex];

        HandleRerollResult(resultIndex);

        isRolling = false;
    }

    private void HandleRerollResult(int index)
    {
        BaseCharacter player = FindFirstObjectByType<BaseCharacter>();

        switch (index)
        {
            case 0: // Bread
                int breadAmount = Random.Range(1, 3);
                for (int i = 0; i < breadAmount; i++)
                {
                    InventoryManager.Instance.AddBread();
                }
                break;
            case 1: // Beer
                int beerAmount = Random.Range(1, 3);
                for (int i = 0; i < beerAmount; i++)
                {
                    InventoryManager.Instance.AddBeer();
                }
                break;
            case 2: // Health
                player.IncreaseMaxHealth(10);
                break;
            case 3: // Energy
                player.IncreaseMaxStamina(10);
                break;
            case 4: // Hunger
                player.IncreaseMaxHunger(10);
                break;
        }
    }

    private void UpdateShopUI()
    {
        rerollCostText.text = rerollCost.ToString();
    }
    
    void Update()
    {
        // Check for E key press
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleShop();
        }
    }
}
