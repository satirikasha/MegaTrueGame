using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {

    public Weapon Owner;

    private List<Projectile> _Projectiles = new List<Projectile>();

    void Awake() {
        this.transform.SetParent(PoolHost.Instance.transform);
    }

    public Projectile GetProjectile() {
        var result = _Projectiles.FirstOrDefault(_ => !_.gameObject.activeSelf);
        if (result == null)
            result = AddProjectile();
        return result;
    }

    private Projectile AddProjectile() {
        var projectile = Owner.CreateProjectile();
        projectile.transform.SetParent(this.transform);
        projectile.gameObject.SetActive(false);
        _Projectiles.Add(projectile);
        return projectile;
    }
}
