using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : VisualEffect {

    private ParticleSystem _ParticleSystem;

    void Awake() {
        _ParticleSystem = this.GetComponent<ParticleSystem>();
    }

    protected override IEnumerator PlayTask() {
        _ParticleSystem.Play(true);
        yield return new WaitWhile(() => _ParticleSystem.IsAlive(true));
        this.gameObject.SetActive(false);
    }
}
