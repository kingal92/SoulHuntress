# Soul Huntress - Unity 2D Prototype

## Quick Start

### What You Get
- Complete dark-fantasy action RPG prototype
- Top-down combat with melee + ranged attacks
- Enemy waves with scaling difficulty
- Boss fights every 3rd wave
- Soul currency system
- Upgrade shop (damage, health, speed)
- Simple but functional UI

### Minimum Setup (5 minutes)

#### 1. Create Player
```
1. GameObject > 2D Object > Sprites > Circle
2. Rename to "Player"
3. Add components:
   - Rigidbody2D (Gravity Scale: 0)
   - Circle Collider 2D
   - PlayerController script
4. Create child empty object "AttackPoint" (position 0.5 units forward)
5. Tag as "Player"
6. In PlayerController inspector, assign AttackPoint
```

#### 2. Create Enemy Prefab
```
1. GameObject > 2D Object > Sprites > Circle (red color)
2. Add components:
   - Rigidbody2D (Gravity Scale: 0)
   - Circle Collider 2D
   - EnemyController script
3. Set layer to "Enemy"
4. Drag into Assets/Prefabs/Enemy.prefab
```

#### 3. Create Boss Prefab
```
1. Duplicate Enemy
2. Rename to "Boss"
3. Scale up (1.5x)
4. Add BossController script
5. Set maxHealth to 150 in inspector
6. Drag into Assets/Prefabs/Boss.prefab
```

#### 4. Create Projectile Prefab
```
1. GameObject > 2D Object > Sprites > Circle (yellow)
2. Add components:
   - Circle Collider 2D (Is Trigger: ON)
   - Projectile script
3. Drag into Assets/Prefabs/Projectile.prefab
4. In PlayerController, assign this prefab
```

#### 5. Create Spawn Points
```
1. Create 4 empty GameObjects
2. Position at corners: (-8,5), (8,5), (-8,-5), (8,-5)
```

#### 6. Create GameManager
```
1. Empty GameObject named "GameManager"
2. Add GameManager script
```

#### 7. Create EnemySpawner
```
1. Empty GameObject named "EnemySpawner"
2. Add EnemySpawner script
3. Assign Enemy Prefab, Boss Prefab, and Spawn Points
4. In GameManager, assign this Spawner
```

#### 8. Create UI Canvas
```
1. GameObject > UI > Text - TextMeshPro
2. Create 3 Text objects:
   - HealthText (top-left)
   - SoulText (top-center)
   - WaveText (top-right)
3. Empty GameObject "UIManager"
4. Add UIManager script and assign texts
```

#### 9. Create Shop UI
```
1. Create Panel named "ShopPanel"
2. Add 4 Buttons inside:
   - Damage Button (onClick: ShopManager.BuyDamageUpgrade)
   - Health Button (onClick: ShopManager.BuyHealthUpgrade)
   - Speed Button (onClick: ShopManager.BuySpeedUpgrade)
   - Continue Button (onClick: ShopManager.CloseShop)
3. Empty GameObject "ShopManager"
4. Add ShopManager script and assign ShopPanel
5. In GameManager, assign ShopManager reference
```

#### 10. Create Layers
```
1. Edit layers, add "Enemy" layer
2. Assign all enemies to this layer
3. In PlayerController, set Enemy Layer Mask to "Enemy"
```

#### 11. Press Play
```
WASD = Move
Mouse Click / J = Melee Attack
Space / K = Cast Projectile
```

## Game Loop

1. **Wave Starts**: Enemies spawn gradually
2. **Combat**: Kill enemies, collect souls
3. **Wave Clear**: Shop opens
4. **Upgrade**: Buy damage/health/speed
5. **Continue**: Next wave begins
6. **Every 3rd Wave**: Boss fight instead
7. **Die**: Run resets

## Scaling

- Enemy count: +2 per wave
- Enemy health: +5 per wave
- Upgrade costs: +5 per wave
- Boss every 3 waves

## Controls

| Key | Action |
|-----|--------|
| WASD / Arrows | Move |
| Left Click / J | Melee |
| Space / K | Projectile |

## Customization Ideas

**Easy wins:**
- Change enemy/boss colors
- Adjust moveSpeed, attackDamage in inspectors
- Add more upgrade types (crit, attackSpeed)
- Add SFX to attacks/deaths
- Scale difficulty up/down

**Medium:**
- Add 2-3 enemy types with different AI
- Add passive abilities
- Add item drops
- Add particle effects

**Advanced:**
- Implement skill tree
- Add boss patterns
- Add procedural loot
- Add leaderboards
- Add mobile touch input

## Project Structure

```
Assets/
├── Scripts/
│   ├── GameManager.cs
│   ├── PlayerController.cs
│   ├── EnemyController.cs
│   ├── BossController.cs
│   ├── EnemySpawner.cs
│   ├── ShopManager.cs
│   ├── UIManager.cs
│   ├── Projectile.cs
│   └── SoulPickup.cs
├── Prefabs/
│   ├── Enemy.prefab
│   ├── Boss.prefab
│   ├── Projectile.prefab
│   └── Soul.prefab
└── Scenes/
    └── GameScene.unity
```

## Tips

1. **Test early**: Play after each component
2. **Use inspector**: Tweak numbers without recompiling
3. **Spawn points matter**: Place them off-screen
4. **Colliders**: Make sure all objects have colliders
5. **Layers**: Set Player on default, Enemies on "Enemy" layer
6. **Physics**: Keep Gravity Scale = 0 for 2D top-down
7. **UI**: Use Anchors to keep text in place

## Common Issues

**Enemies not spawning**: Check EnemySpawner references in GameManager
**Can't hurt enemies**: Set Enemy Layer Mask in PlayerController
**Shop won't open**: Make sure ShopManager is assigned in GameManager
**Movement broken**: Rigidbody2D Body Type should be Dynamic, not Kinematic
**Projectiles vanish**: Make sure Projectile has Circle Collider 2D set as Trigger

## Next Steps

Once the prototype is working:
1. Add animations
2. Add sound effects
3. Add more enemy types
4. Add visual feedback (screen shake, particles)
5. Add persistent progression
6. Polish UI and menus
7. Add mobile support

Have fun building! 🎮
