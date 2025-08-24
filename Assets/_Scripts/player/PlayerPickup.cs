using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public float pickupRadius = 2f;
    public KeyCode pickupKey = KeyCode.F;

    [Header("Visual Feedback")]
    public GameObject pickupIndicator;
    public AudioClip pickupSound;

    private List<Item> nearbyItems = new List<Item>();
    private AudioSource audioSource;

    void Start()
    {
      
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

       
        if (pickupIndicator != null)
        {
            pickupIndicator.SetActive(false);
        }
    }

    void Update()
    {
        CheckNearbyItems();
        HandlePickupInput();
        UpdatePickupIndicator();
    }

    void CheckNearbyItems()
    {
        nearbyItems.Clear();

      
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach (var hitCollider in hitColliders)
        {
            Item item = hitCollider.GetComponent<Item>();
            if (item != null && !nearbyItems.Contains(item))
            {
                nearbyItems.Add(item);
            }
        }
    }

    void HandlePickupInput()
    {
        if (Input.GetKeyDown(pickupKey) && nearbyItems.Count > 0)
        {
            
            PickupItem(nearbyItems[0]);
        }
    }

    void UpdatePickupIndicator()
    {
        if (pickupIndicator != null)
        {
            
            pickupIndicator.SetActive(nearbyItems.Count > 0);

           
            if (pickupIndicator.activeInHierarchy && Camera.main != null)
            {
                pickupIndicator.transform.LookAt(Camera.main.transform);
                pickupIndicator.transform.Rotate(0, 180, 0); 
            }
        }
    }

    public void PickupItem(Item item)
    {
        Inventory inventory = GetComponent<Inventory>();
        if (inventory != null && item != null)
        {
            if (inventory.AddItem(item))
            {
                
                if (pickupSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(pickupSound);
                }

                
                Destroy(item.gameObject);

                Debug.Log("Подобран предмет: " + item.itemName);
            }
            else
            {
                Debug.Log("Инвентарь полон! Не могу подобрать: " + item.itemName);
            }
        }
    }

    
    void OnCollisionEnter(Collision collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        if (item != null)
        {
           PickupItem(item);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null)
        {
           
            if (!nearbyItems.Contains(item))
            {
                nearbyItems.Add(item);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && nearbyItems.Contains(item))
        {
            nearbyItems.Remove(item);
        }
    }

   
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }

   
    public bool CanPickup()
    {
        return nearbyItems.Count > 0;
    }

    public Item GetClosestItem()
    {
        if (nearbyItems.Count == 0) return null;

        Item closestItem = null;
        float closestDistance = Mathf.Infinity;

        foreach (Item item in nearbyItems)
        {
            float distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }

        return closestItem;
    }
}
