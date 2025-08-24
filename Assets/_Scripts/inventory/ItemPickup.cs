using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item; 
    public float pickupRadius = 2f; 
    private GameObject player;
    private Inventory inventory;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }

    void Update()
    {
      
        if (Vector3.Distance(transform.position, player.transform.position) <= pickupRadius)
        {
            
            Debug.Log("Нажмите F чтобы подобрать: " + item.itemName);

            
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUp();
            }
        }
    }

    void PickUp()
    {
      
        if (inventory.AddItem(item))
        {
            Debug.Log("Подобран предмет: " + item.itemName);
            Destroy(gameObject); 
        }
    }

   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}