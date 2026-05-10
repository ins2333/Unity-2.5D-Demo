# 2.5D 射击游戏 Demo

> 这是一个完整的 2.5D 射击游戏示例项目，展示了包括怪物 AI（有限状态机）、对象池管理、SQLite 数据持久化、UI 系统等核心功能。该项目由我独立开发，用于求职作品展示。

## 🎮 游戏玩法

- 控制角色在场景中移动，鼠标控制角色面向方向
- 点击鼠标左键射击，击败敌人可获得分数
- 敌人的 AI 状态有：巡逻（静止）、追逐、攻击、死亡
- 游戏内按 `Esc` 键打开设置菜单，可保存当前分数、退出游戏等
- 主菜单支持新游戏、读取存档（展示历史分数列表）、退出游戏

## 🔧 技术亮点

| 技术点 | 实现说明 |
|--------|----------|
| **有限状态机 (FSM)** | `EnemyAI` 使用枚举定义 Patrol/Chase/Attack/Dead 四种状态，通过 `UpdateState` 和 `EnemyState` 实现清晰的状态切换 |
| **对象池** | `EnemyManager` 使用 Unity 内建 `ObjectPool` 管理敌人的创建、回收和复用，减少 GC |
| **SQLite 数据持久化** | 集成 `SQLite4Unity3d` 插件，实现玩家分数的保存与读取；支持多条存档记录，按时间排序 |
| **动态 UI 生成** | `SaveListManager` 根据数据库记录动态生成存档按钮，点击后加载对应分数并切换场景 |
| **单例模式** | `PlayerScoreManager`、`ConnectSQLite`、`SettingManager` 确保全局唯一访问点 |
| **NavMesh 导航** | 敌人使用 `NavMeshAgent` 追踪玩家，移动流畅 |
| **跨场景数据保持** | 使用 `DontDestroyOnLoad` 保证分数单例在场景切换时不丢失，并通过 `SceneManager.sceneLoaded` 事件动态绑定 UI |
| **性能优化** | `EnemyAI` 中每 0.2 秒更新一次状态，避免每帧计算；对象池降低内存开销 |

## 📁 项目结构
Assets/
├── Scripts/
│ ├── Camera/ # 相机跟随
│ ├── Enemy/ # 敌人 AI、攻击、健康、移动（FSM + 对象池）
│ ├── Managers/ # 游戏管理（分数、存档列表、主菜单、设置）
│ ├── Player/ # 玩家移动、射击、健康
│ └── SQLite/ # SQLite 连接与数据模型
├── Prefabs/ # 预制体（敌人、存档条目等）
├── Scenes/ # 场景（主菜单、游戏场景）
└── StreamingAssets/ # 数据库文件
text

## 🛠️ 运行说明

1. **环境要求**：Unity 2022.3.22f1 或更高版本（支持 .NET 4.x）
2. **打开项目**：项目已集成 `SQLite4Unity3d` 插件，打开后 Unity 会自动加载依赖，无需额外配置。数据库文件会在首次运行时自动创建在 `Application.persistentDataPath` 路径下。
3. **首次运行**：进入游戏场景后会自动创建数据库文件（位于 `Application.persistentDataPath`）
4. **操作指南**：
   - 移动：WASD 键
   - 转向：鼠标移动（指向地面位置）
   - 射击：鼠标左键
   - 设置菜单：Esc 键（游戏内）

## 📞 联系我

- 邮箱：1992676628@qq.com
- GitHub：https://github.com/ins2333
- 求职意向：Unity 开发实习生（工业仿真/虚拟仿真/游戏开发方向）

## 🏷️ 标签

`Unity` `C#` `FSM` `ObjectPool` `SQLite` `NavMesh` `游戏开发` `工业仿真` `存档系统`
