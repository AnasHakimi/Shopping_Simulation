using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerInteraction : MonoBehaviour
{
    public Text interactionText;
    public GameObject itemDetailPanel;
    public Text itemNameText, itemPriceText, itemDescriptionText;
    public Image itemImageUI;
    public Button addToCartButton;

    public GameObject cartSummaryPanel;
    public Transform cartContent;
    public GameObject cartItemPrefab;
    public Text totalPriceText;
    public Button proceedToBuyButton;
    public Text mainCanvasCoinText;


    public Text playerWalletText;
    public Text playerWalletText2;

    private List<CartItem> cart = new List<CartItem>();
    private int totalPrice = 0;

    private ShopItem currentItem;
    private bool nearItem = false;
    private bool nearCashier = false;

    void Start()
    {
        interactionText.gameObject.SetActive(false);
        itemDetailPanel.SetActive(false);
        cartSummaryPanel.SetActive(false);
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; 
        UpdatePlayerWallet();
    }

    void Update()
    {
        if (nearItem && Input.GetKeyDown(KeyCode.E) && !itemDetailPanel.activeSelf)
        {
            ShowItemDetails(currentItem);
        }

        if (nearCashier && Input.GetKeyDown(KeyCode.E))
        {
            if (cartSummaryPanel.activeSelf)
            {
                CloseCartSummary();
            }
            else
            {
                ShowCartSummary();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllPanels();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            currentItem = other.GetComponent<ItemBehavior>().shopItem;
            interactionText.text = "Press [E] to view item detail";
            interactionText.gameObject.SetActive(true);
            nearItem = true;
        }

        if (other.CompareTag("Cashier"))
        {
            interactionText.text = "Press [E] to pay";
            interactionText.gameObject.SetActive(true);
            nearCashier = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            interactionText.gameObject.SetActive(false);
            nearItem = false;
        }

        if (other.CompareTag("Cashier"))
        {
            interactionText.gameObject.SetActive(false);
            nearCashier = false;
        }
    }

    void ShowItemDetails(ShopItem item)
    {
        itemDetailPanel.SetActive(true);
        itemNameText.text = item.itemName;
        itemPriceText.text = "Price: " + item.itemPrice + " Coins";
        itemDescriptionText.text = item.itemDescription;

        if (item.itemImage != null)
        {
            itemImageUI.sprite = item.itemImage;
            itemImageUI.gameObject.SetActive(true);
        }
        else
        {
            itemImageUI.gameObject.SetActive(false);
        }

        addToCartButton.onClick.RemoveAllListeners();
        addToCartButton.onClick.AddListener(() => AddToCart(item));

        EnableCursor();
    }

    public void CloseItemDetails()
    {
        itemDetailPanel.SetActive(false);
        DisableCursor();
    }

    void AddToCart(ShopItem item)
    {
        CartItem existingCartItem = cart.Find(ci => ci.shopItem == item);
        if (existingCartItem != null)
        {
            existingCartItem.quantity++;
        }
        else
        {
            cart.Add(new CartItem(item, 1));
        }

        totalPrice += item.itemPrice;
        CloseItemDetails();
    }

    void ShowCartSummary()
    {
        cartSummaryPanel.SetActive(true);

        foreach (Transform child in cartContent)
        {
            Destroy(child.gameObject);
        }

        foreach (CartItem cartItem in cart)
        {
            GameObject newCartItem = Instantiate(cartItemPrefab, cartContent);
            Text cartItemText = newCartItem.GetComponentInChildren<Text>();

            cartItemText.text = $"{cartItem.shopItem.itemName} : {cartItem.quantity} x {cartItem.shopItem.itemPrice} = {cartItem.quantity * cartItem.shopItem.itemPrice}";
        }

        totalPriceText.text = "Total: " + totalPrice + " Coins";

        proceedToBuyButton.onClick.RemoveAllListeners();
        proceedToBuyButton.onClick.AddListener(ProceedToBuy);

        EnableCursor();
    }

    void CloseCartSummary()
    {
        cartSummaryPanel.SetActive(false);
        DisableCursor();
    }

    void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
    }

    void DisableCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
    }


    void ProceedToBuy()
    {
        if (GlobalCoinData.coinCount >= totalPrice)
        {
            GlobalCoinData.coinCount -= totalPrice;
            cart.Clear();
            totalPrice = 0;
            UpdatePlayerWallet();
            CloseCartSummary();
            Debug.Log("Purchase successful!");
        }
        else
        {
            Debug.Log("Not enough coins to complete the purchase.");
        }
    }

    void UpdatePlayerWallet()
    {
        string coinText = "Coins: "+ GlobalCoinData.coinCount;

        if (playerWalletText != null)
        {
            playerWalletText.text = coinText;
        }

        if (playerWalletText2 != null)
        {
            playerWalletText2.text = coinText;
        }

        if (mainCanvasCoinText != null)
        {
            mainCanvasCoinText.text = coinText; 
        }
    }

    void CloseAllPanels()
    {
        itemDetailPanel.SetActive(false);
        cartSummaryPanel.SetActive(false);
        DisableCursor(); 
    }
}
