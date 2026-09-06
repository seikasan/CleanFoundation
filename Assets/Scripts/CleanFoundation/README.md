# CleanFoundation

Unityでよく使う値型・数学APIを、Domain / Application から `UnityEngine` を直接参照せず使うための共通ライブラリです。

`Vector2`、`Vector3`、`Quaternion`、`Color`、`Mathf` などはUnityとほぼ同じAPIで利用でき、数学・幾何処理はPure C#で実装しています。Unity環境では `UNITY_5_3_OR_NEWER` が定義されている場合に限り、対応する `UnityEngine` 型との暗黙変換が有効になります。そのため同じRuntimeソースをUnity以外の.NET環境でも `UnityEngine` 参照なしでコンパイルできます。

デコンパイル結果からManaged実装を確認できる処理は、その挙動に合わせて実装しています。Unityネイティブ側にのみ実体がある補間、ノイズ、色温度変換などはPure C#の互換実装であり、極端な入力や浮動小数点の境界ではUnity本体と完全なビット一致にならない場合があります。

また、`Duration`、`Angle`、`Speed`、`Velocity` などの単位付きValue Objectを提供し、ゲームロジック上の値を型安全に扱えるようにします。

## 方針

- Unityの使いやすいAPIは維持する
- Domain / Application からの直接的な `UnityEngine` 参照を減らす
- Unityの実行環境やグローバル状態は持ち込まない
- `GameObject`、`Transform`、`Physics`、`Time` などは対象外
- 単なるラッパーではなく、ゲームロジックを安全に書くための最小限の共通基盤とする
