using UnityEngine;
using System.Collections;
using System;
using UnityEngine.AI;

namespace Tools {


    public static partial class Utils {

        public static void DrawDebug(this NavMeshPath path, Color color) {
            for (int i = 1; i < path.corners.Length; i++) {
                Debug.DrawLine(path.corners[i - 1], path.corners[i], color);
            }
        }
    }
}
