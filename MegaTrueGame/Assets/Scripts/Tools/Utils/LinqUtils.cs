using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public static partial class Utils {

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
        foreach (var item in collection) {
            action(item);
        }
        return collection;
    }
}
