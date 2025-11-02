# dddcs
ドメイン駆動設計入門

```mermaid
graph TD
subgraph ドメインモデル (Domain Model)
        %% 'subgraph'の宣言の次の行に 'direction' を書きます
        direction TB

        DS[ドメインサービス<br>(Domain Service)]

        %% 内部のsubgraph（エンティティと値オブジェクトをまとめる枠）
        subgraph " "
            direction LR
            E[エンティティ<br>(Entity)]
            VO[値オブジェクト<br>(Value Object)]
        end

        %% 関連性
        DS -->|複数の...を操作| E
        DS -->|複数の...を利用| VO
        E ---|属性として保持| VO
    end

 %% スタイル指定
 style ドメインモデル fill:#f9f9f9,stroke:#333,stroke-width:2px
 style " " fill:#f9f9f9,stroke:#f9f9f9
 style E fill:#e6f7ff,stroke:#0056b3,stroke-width:1px
 style VO fill:#fffbe6,stroke:#b38600,stroke-width:1px
 style DS fill:#e6ffed,stroke:#006400,stroke-width:1px
```
