using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldChunk : MonoBehaviour {

    public int ID {
        get {
            return _ID;
        }
    }
    [ReadOnly]
    [SerializeField]
    private int _ID;

    public ChunkData Data {
        get {
            if(_Data == null)
                _Data = ChunkConfig.Instance.GetChunkData(ID);
            return _Data;
        }
    }
    private ChunkData _Data;

    public Transform EntranceRoot;
    public Transform ExitRoot;

    public void SetID(int id) {
        _ID = id;
    }

    public List<WorldChunk> Generate() {
        var requiredType = ChunkType.Room;
        var selection = ChunkConfig.Instance.Chunks.Where(_ => _.Type == requiredType && _.ID != Data.ID).ToList();
        var result = new List<WorldChunk>();
        for (int i = 0; i < ExitRoot.childCount; i++) {
            var data = selection.ElementAt(Random.Range(0, selection.Count()));
            var exit = ExitRoot.GetChild(i);
            if (Physics.OverlapSphere(exit.position + exit.forward * (data.BoundingRadius), data.BoundingRadius, LayerMask.GetMask("Chunk"), QueryTriggerInteraction.Collide).Count(_ => _.gameObject != this.gameObject) > 0)
                continue;
            var chunk = Instantiate(data.Prefab);
            var entrance = chunk.EntranceRoot.GetChild(0);
            chunk.transform.RotateAround(entrance.position, Vector3.up, Vector3.Angle(entrance.forward, exit.forward) * Mathf.Sign(Vector3.Dot(exit.forward, entrance.right)));
            chunk.transform.position = exit.position + (chunk.transform.position - entrance.position);
            selection.RemoveAll(_ => _.ID == data.ID);
            result.Add(chunk);
        }
        return result;
    }

#if UNITY_EDITOR
    void OnDrawGizmos() {
        if (EntranceRoot != null) {
            var data = ChunkConfig.Instance.GetChunkData(ID);
            var entrance = EntranceRoot.GetChild(0);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(entrance.position + entrance.forward * data.BoundingRadius, data.BoundingRadius);
        }
    }
#endif
}
