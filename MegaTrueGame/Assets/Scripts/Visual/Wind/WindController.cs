using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WindController : MonoBehaviour {

    public Texture2D OffsetTexture;

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
        Shader.SetGlobalTexture("_WindOffsetTex", OffsetTexture);
    }
}
