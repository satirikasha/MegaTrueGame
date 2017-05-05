using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class InventoryItemTemplate : ScriptableObject {

    public int ID { get { return _ID; } }

    [Header("Item Template")]
    [ReadOnly]
    [SerializeField]
    private int _ID;

    [Header("Info")]
    public string Name;
    [Multiline]
    public string Description;

    public virtual InventoryItem GenerateItem() {
        var type = Type.GetType(this.GetType().Name.Replace("Template",""));
        var item = (InventoryItem)Activator.CreateInstance(type);
        type.GetField("DataID", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(item, ID);
        return item;
    }

#if UNITY_EDITOR
    void OnValidate() {
        RefreshID();
    }

    public void RefreshID() {
        if (_ID == 0) {
            _ID = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this)).GetHashCode();
        }
    }
#endif
}

[Serializable]
public abstract class InventoryItem<T> : InventoryItem where T : InventoryItemTemplate {
    public T Data { get { return (T)RawData; } }
}

[Serializable]
public abstract class InventoryItem {

    public virtual InventoryItemTemplate RawData {
        get {
            if (_Data == null)
                _Data = InventoryItemsManager.GetItem(DataID);
            return _Data;
        }
    }
    protected InventoryItemTemplate _Data;

    protected int DataID;
}
