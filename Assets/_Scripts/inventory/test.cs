using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public Inventory inventory;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            inventory.AddResource(ResourceType.resours1, 1);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            List<ResourceData> NeedResorses = new List<ResourceData>
            {
             new ResourceData(ResourceType.resours1, 1)
            };

            inventory.TryConsume(NeedResorses);
        }
    }
}
