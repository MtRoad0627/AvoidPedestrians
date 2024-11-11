using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SortGameObject : MonoBehaviour {
    [ContextMenu("Sort game object")]
    private void Sort() {
        List<Transform> objList = new List<Transform>();

        // 子階層のGameObject取得
        var childCount = transform.childCount;
        for ( int i = 0 ; i < childCount ; i++ ) {
            objList.Add(transform.GetChild(i));
        }

        // オブジェクトを名前で昇順ソート
        // ★ここを用途に合わせて変更してください
        objList.Sort((obj1, obj2) => string.Compare(obj1.name, obj2.name));

        // ソート結果順にGameObjectの順序を反映
        foreach ( var obj in objList ) {
            obj.SetSiblingIndex(childCount - 1);
        }
    }
}