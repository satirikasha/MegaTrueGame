using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WindController : MonoBehaviour {

    public Texture2D OffsetTexture;
    [Range(1,50)]
    public float WindScale = 25;
    [Range(0, 1)]
    public float WindSpeed = 0.15f;

    void Start () {
        Apply();	
	}
	
	void Update () {
        if (!Application.isPlaying) {
            Apply();
        }
	}

    public void Apply() {
        Shader.SetGlobalVector("_WindDir", this.transform.forward);
        Shader.SetGlobalFloat("_WindScale", WindScale);
        Shader.SetGlobalFloat("_WindSpeed", WindSpeed);
        Shader.SetGlobalTexture("_WindOffsetTex", OffsetTexture);
    }
}
