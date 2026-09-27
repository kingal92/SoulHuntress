# Soul Huntress

A dark-fantasy 2D action prototype built for Unity.

Features:
- Top-down player movement and melee attack
- Projectile magic attack
- Enemy wave system
- Boss wave every third round
- Soul pickup and reward loop
- Upgrade shop with damage, health, and speed buffs
- Simple UI for health, souls, and wave progression

How to set up in Unity:
1. Create a new 2D project in Unity.
2. Create folders: `Assets/Scripts`.
3. Copy every script from this repo into `Assets/Scripts`.
4. Create a Player object with a `Rigidbody2D`, `Collider2D`, and `PlayerController`.
5. Add a child object named `AttackPoint` for melee range.
6. Create enemy prefab with `Rigidbody2D`, `Collider2D`, and `EnemyController`.
7. Create boss prefab with `BossController`.
8. Create a `GameManager` object with `GameManager` script attached.
9. Create a `ShopManager` object with `ShopManager` script attached and assign UI panel references.
10. Create `UIManager` and assign text fields.
11. Tag the Player as `Player` and Enemy prefabs as `Enemy`.

Tip:
- Use a simple circle/square sprite for enemies.
- Give the player and enemies a basic 2D collider and set `Is Trigger` as needed.
- For the shop, create a UI canvas with buttons wired to `BuyDamageUpgrade`, `BuyHealthUpgrade`, and `BuySpeedUpgrade`.
