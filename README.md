# Clean Foundation

Unityでクリーンアーキテクチャに基づいた設計をしていると、DomainやApplicationで`UnityEngine.Vector3`や`Debug.Log()`などを使いたい場面が出てきます。というか出てきました。

それだけのためにUnity依存を入れるのもな～、アセンブリ切ってるからな～、という困りごとを解決します。

`CleanFoundation.asmdef`を参照すればもうUnity依存を入れる必要はありません！

## 導入方法

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力する

```
https://github.com/seikasan/CleanFoundation.git?path=Assets/Scripts/CleanFoundation
```
