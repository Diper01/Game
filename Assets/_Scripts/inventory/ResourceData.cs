using System;
using UnityEngine;

[Serializable]
public class ResourceData
{
    public ResourceType ResourceType;
    public uint Amount;

    public ResourceData(ResourceType type, uint amount)
    {
        ResourceType = type;
        Amount = amount;
    }
    public ResourceData()
    {
        
    }
}

public enum ResourceType
{
   resours1, resours2, resours3, resours4, resours5, resours6
}

[Serializable]
public struct ResourceIcon
{
    public ResourceType Type;
    public Sprite Sprite;
}