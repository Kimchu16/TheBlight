using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public int coinCount;
    public int beerCount;
    public int breadCount;

    // Drag your UI Text elements here in the inspector
    public TMP_Text coinText;
    public TMP_Text beerText;
    public TMP_Text breadText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional if you want it to persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Adders
    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateUI();
    }

    public void AddBeer()
    {
        beerCount += 1;
        UpdateUI();
    }

    public void AddBread()
    {
        breadCount += 1;
        UpdateUI();
    }

    // Consumers
    public bool UseBeer()
    {
        if (beerCount > 0)
        {
            beerCount--;
            UpdateUI();
            return true; // Successfully used
        }
        return false; // No beer to use
    }

    public bool UseBread()
    {
        if (breadCount > 0)
        {
            breadCount--;
            UpdateUI();
            return true; // Successfully used
        }
        return false; // No bread to use
    }

    public bool UseCoin(int amount)
    {
        if (coinCount >= amount)
        {
            coinCount -= amount;
            UpdateUI();
            return true; // Successfully used
        }
        return false; // Not enough coins
    }

    private void UpdateUI()
    {
        coinText.text = coinCount.ToString();
        beerText.text = beerCount.ToString();
        breadText.text = breadCount.ToString();
    }
}
