using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

    public WorldChunk Base;
    private List<WorldChunk> _LeafChunks = new List<WorldChunk>();

	void Start () {
        _LeafChunks.Add(Base);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G)) {
            _LeafChunks = _LeafChunks.SelectMany(_ => _.Generate()).ToList();
        }
	}
}
