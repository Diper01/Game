using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;

    public virtual void Drop()
    {
        Debug.Log("Бросаем предмет: " + itemName);
       
        Instantiate(this, transform.position, Quaternion.identity);
    }
}
