using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public partial class Inventory : MonoBehaviour
    {
        [SerializeField] private List<ResourceData> _resources = new List<ResourceData>();
        public event Action Changed;

        [SerializeField] private List<ResourceIcon> _icons = new List<ResourceIcon>();
        private Dictionary<ResourceType, Sprite> _iconMap;

        private void Awake()
        {
            if (_iconMap == null)
            {
                _iconMap = new Dictionary<ResourceType, Sprite>(_icons.Count);
                foreach (var e in _icons) _iconMap[e.Type] = e.Sprite;
            }
        }
           public uint GetAmount(ResourceType type)
           {
             var existing = _resources.FirstOrDefault(x => x.ResourceType == type);
             return existing != null ? existing.Amount : 0;
           }
          public bool HasResources(IEnumerable<ResourceData> required)
          {
             foreach (var r in required)
              if (GetAmount(r.ResourceType) < r.Amount) return false;
              return true;
          }
          public IReadOnlyList<ResourceData> GetAll()
          {
             return _resources;
          }

        public Sprite GetSprite(ResourceType type)
        {
            if (_iconMap == null)
            {
                _iconMap = new Dictionary<ResourceType, Sprite>(_icons.Count);
                foreach (var e in _icons) _iconMap[e.Type] = e.Sprite;
            }
            return _iconMap.TryGetValue(type, out var s) ? s : null;
        }

        public void AddResource(ResourceType type, uint amount)
        {
            if (amount == 0) return;
            var existing = _resources.FirstOrDefault(x => x.ResourceType == type);
            if (existing != null) existing.Amount += amount;
            else _resources.Add(new ResourceData { ResourceType = type, Amount = amount });
            Changed?.Invoke();
        }

        public void AddResource(ResourceData data) => AddResource(data.ResourceType, data.Amount);

        public void AddResources(IEnumerable<ResourceData> data)
        {
            foreach (var d in data) AddResource(d.ResourceType, d.Amount);

        }

        public bool TryConsume(IEnumerable<ResourceData> required)
        {
            if (!HasResources(required)) return false;
            foreach (var r in required)
            {
                var ex = _resources.First(x => x.ResourceType == r.ResourceType);
                ex.Amount -= r.Amount;
                if (ex.Amount == 0) _resources.Remove(ex);
            }
            Changed?.Invoke();
            return true;
        }
    }


