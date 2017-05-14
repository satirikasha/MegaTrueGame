using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class Weapon {

    //Debug
    public WeaponChasisItemTemplate ChasisTemplate;
    public WeaponMagazineItemTemplate MagazineTemplate;
    public WeaponAmplifierItemTemplate AmplifierTemplate;
    public WeaponBarrelItemTemplate BarrelTemplate;
    public WeaponStockItemTemplate StockTemplate;
    public WeaponScopeItemTemplate ScopeTemplate;

    public WeaponChasisItem Chasis { get; set; }
    public WeaponMagazineItem Magazine { get; set; }
    public WeaponAmplifierItem Amplifier { get; set; }
    public WeaponBarrelItem Barrel { get; set; }
    public WeaponStockItem Stock { get; set; }
    public WeaponScopeItem Scope { get; set; }

    public WeaponData WeaponData { get; private set; }

    public void Rebuild() {
        //Debug
        GenerateItems();

        Clear();
        Build();
    }

    private void Build() {
        BuildData();
        BuildView();
    }

    private void BuildData() {
        WeaponData = new WeaponData();
        WeaponData.WeaponType = Chasis.Data.WeaponType;
        WeaponData.BarrelType = Barrel.Data.BarrelType;
        WeaponData.BulletType = Magazine.Data.BulletType;
        WeaponData.AmplifierType = Amplifier.Data.AmplifierType;
        WeaponData.Damage = Chasis.Data.Damage;
        WeaponData.ClipSize = Magazine.Data.ClipSize;
        WeaponData.RechargeTime = StandardRechargeTime / Chasis.Data.RechargeSpeed;
        WeaponData.CooldownTime = StandardCooldownTime / Chasis.Data.FireRate;
        WeaponData.ProjectileSpeed = Chasis.Data.ProjectileSpeed;
    }

    private void BuildView() {
        Transform rifleSocket = this.transform;
        Transform stockSocket = this.transform;
        Transform scopeSocket = this.transform;
        List<WeaponSocket> sockets = new List<WeaponSocket>();

        var chasis = Instantiate(Chasis.Data.ItemPrefab, rifleSocket.position, rifleSocket.rotation, this.transform);
        chasis.GetComponentsInChildren(sockets);
        rifleSocket = sockets.First(_ => _.SocketType == WeaponSocketType.Rifle).transform;
        stockSocket = sockets.First(_ => _.SocketType == WeaponSocketType.Stock).transform;
        scopeSocket = sockets.First(_ => _.SocketType == WeaponSocketType.Scope).transform;

        var magazine = Instantiate(Magazine.Data.ItemPrefab, rifleSocket.position, rifleSocket.rotation, this.transform);
        magazine.GetComponentsInChildren(sockets);
        rifleSocket = sockets.First(_ => _.SocketType == WeaponSocketType.Rifle).transform;

        var amplifier = Instantiate(Amplifier.Data.ItemPrefab, rifleSocket.position, rifleSocket.rotation, this.transform);
        amplifier.GetComponentsInChildren(sockets);
        rifleSocket = sockets.First(_ => _.SocketType == WeaponSocketType.Rifle).transform;

        var barrel = Instantiate(Barrel.Data.ItemPrefab, rifleSocket.position, rifleSocket.rotation, this.transform);
        FirePivot = barrel.GetComponentInChildren<FirePivot>().transform;

        Instantiate(Stock.Data.ItemPrefab, stockSocket.position, stockSocket.rotation, this.transform);
        Instantiate(Scope.Data.ItemPrefab, scopeSocket.position, scopeSocket.rotation, this.transform);

    }

    private void Clear() {
        for (int i = 0; i < this.transform.childCount; i++) {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    public Projectile CreateProjectile() {
        var projectile = Instantiate(Projectile);
        projectile.Initialize(WeaponData);
        return projectile;
    }

    //Debug
    private void GenerateItems() {
        Chasis = (WeaponChasisItem)ChasisTemplate.GenerateItem();
        Magazine = (WeaponMagazineItem)MagazineTemplate.GenerateItem();
        Amplifier = (WeaponAmplifierItem)AmplifierTemplate.GenerateItem();
        Barrel = (WeaponBarrelItem)BarrelTemplate.GenerateItem();
        Stock = (WeaponStockItem)StockTemplate.GenerateItem();
        Scope = (WeaponScopeItem)ScopeTemplate.GenerateItem();
    }
}
