# CleanFoundation

Unityでよく使う値型・数学APIを、Domain / Application から `UnityEngine` を直接参照せず使うための共通ライブラリです。

`Vector2`、`Vector3`、`Quaternion`、`Color`、`Mathf` などはUnityとほぼ同じAPIで利用でき、内部処理は `UnityEngine` に委譲します。

また、`Duration`、`Angle`、`Speed`、`Velocity` などの単位付きValue Objectを提供し、ゲームロジック上の値を型安全に扱えるようにします。

## 方針

- Unityの使いやすいAPIは維持する
- Domain / Application からの直接的な `UnityEngine` 参照を減らす
- Unityの実行環境やグローバル状態は持ち込まない
- `GameObject`、`Transform`、`Physics`、`Time` などは対象外
- 単なるラッパーではなく、ゲームロジックを安全に書くための最小限の共通基盤とする
