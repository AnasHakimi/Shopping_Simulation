using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartItem
{
    public ShopItem shopItem;
    public int quantity;

    public CartItem(ShopItem item, int qty)
    {
        shopItem = item;
        quantity = qty;
    }
}
