using System.Collections.Generic;
using UnityEngine;


public class InventoryUIGrid : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [SerializeField] private Transform _gridParent;
    [SerializeField] private Slot _slotPrefab;

    private readonly Dictionary<ResourceType, Slot> _byType = new();

    private void OnEnable()
    {
        if (_inventory != null) _inventory.Changed += OnInventoryChanged;
        FullRebuild();
    }
  

    private void OnDisable()
    {
        if (_inventory != null) _inventory.Changed -= OnInventoryChanged;
    }

    private void OnInventoryChanged()
    {
        SyncDiff();
    }

    private void FullRebuild()
    {
        foreach (Transform child in _gridParent) Destroy(child.gameObject);
        _byType.Clear();

        var all = _inventory.GetAll();
        for (int i = 0; i < all.Count; i++)
            Upsert(all[i]);
    }

    private void SyncDiff()
    {
        var seen = new HashSet<ResourceType>();
        var all = _inventory.GetAll();
        for (int i = 0; i < all.Count; i++)
        {
            Upsert(all[i]);
            seen.Add(all[i].ResourceType);
        }

        var toRemove = new List<ResourceType>();
        foreach (var kv in _byType)
            if (!seen.Contains(kv.Key))
                toRemove.Add(kv.Key);

        for (int i = 0; i < toRemove.Count; i++)
        {
            Destroy(_byType[toRemove[i]].gameObject);
            _byType.Remove(toRemove[i]);
        }
    }

    private void Upsert(ResourceData r)
    {
        if (_byType.TryGetValue(r.ResourceType, out var slot))
        {
            slot.SetAmount(r.Amount);
        }
        else
        {
            var s = Instantiate(_slotPrefab, _gridParent);
            s.name = $"Slot_{r.ResourceType}";
            s.Set(r.ResourceType, _inventory.GetSprite(r.ResourceType), r.Amount);
            _byType[r.ResourceType] = s;
        }
    }
}



