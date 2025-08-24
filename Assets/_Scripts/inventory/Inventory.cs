using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public Item[] slots = new Item[2]; // 2 слота инвентаря
    public Image[] slotIcons; // Иконки слотов в UI

    void Start()
    {
       
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = null;
            UpdateSlotUI(i);
        }
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem(0);
        }

       
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItem(1);
        }
    }

   
    public bool AddItem(Item newItem)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = newItem;
                newItem.gameObject.SetActive(false); 
                UpdateSlotUI(i);
                Debug.Log("Предмет добавлен в слот: " + i);
                return true;
            }
        }

        Debug.Log("Инвентарь полон!");
        return false;
    }

    
    public void DropItem(int slotIndex)
    {
        if (slots[slotIndex] != null)
        {
            Item itemToDrop = slots[slotIndex];

          
            Vector3 dropPosition = transform.position + transform.forward * 2f;
            Item droppedItem = Instantiate(itemToDrop, dropPosition, Quaternion.identity);
            droppedItem.gameObject.SetActive(true);

           
            Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
            }

          
            slots[slotIndex] = null;
            UpdateSlotUI(slotIndex);

            Debug.Log("Предмет выброшен из слота: " + slotIndex);
        }
        else
        {
            Debug.Log("Слот " + slotIndex + " пустой");
        }
    }

    private void UpdateSlotUI(int slotIndex)
    {
        if (slotIcons[slotIndex] != null)
        {
            if (slots[slotIndex] != null)
            {
                slotIcons[slotIndex].sprite = slots[slotIndex].itemIcon;
                slotIcons[slotIndex].color = Color.white;
            }
            else
            {
                slotIcons[slotIndex].sprite = null;
                slotIcons[slotIndex].color = Color.clear;
            }
        }
    }

    public bool IsInventoryFull()
    {
        foreach (Item item in slots)
        {
            if (item == null) return false;
        }
        return true;
    }

    public bool HasItem(string itemName)
    {
        foreach (Item item in slots)
        {
            if (item != null && item.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }

    public Item GetItem(int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            return slots[slotIndex];
        }
        return null;
    }
}

