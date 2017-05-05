using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public abstract class InstancedItemTemplate : InventoryItemTemplate {
    [Header("Prefab")]
    public GameObject ItemPrefab;
}

[Serializable]
public abstract class InstancedItem<T> : InventoryItem<T> where T : InstancedItemTemplate { }

