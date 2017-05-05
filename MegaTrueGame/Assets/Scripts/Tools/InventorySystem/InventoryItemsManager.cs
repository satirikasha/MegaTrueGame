using UnityEngine;
using System.Collections;
using Tools;
using System.Collections.Generic;
using System.Linq;

public static class InventoryItemsManager {
    public static List<InventoryItemTemplate> Items {
        get {
            if (_Items == null)
                RefreshItems();
            return _Items;
        }
    }
    private static List<InventoryItemTemplate> _Items;

    private static Dictionary<int, InventoryItemTemplate> _ItemsCache;

    public static void RefreshItems() {
        _Items = Resources.LoadAll<InventoryItemTemplate>("Items").ToList();
    }

    public static List<string> GetItemNames() {
        return Items.Select(_ => _.Name).ToList();
    }

    public static InventoryItemTemplate GetItem(int ID) {
        if (_ItemsCache == null)
            _ItemsCache = new Dictionary<int, InventoryItemTemplate>();

        if (!_ItemsCache.ContainsKey(ID))
            _ItemsCache.Add(ID, Items.First(_ => _.ID == ID));

        return _ItemsCache[ID];
    }


}
