<div align="center">

<img src="docs/roboy.png" alt="Roboy" width="160">

# Roboy

**A challenging micro Metroidvania platformer**

Guide Roboy through the factory, collect upgrades and scrap, rescue Prize,<br>
and confront the factory owner, Rob Robotson.

[![Play on itch.io](https://img.shields.io/badge/Play%20on-itch.io-FA5C5C?style=for-the-badge&logo=itchdotio&logoColor=white)](https://redrookinteractive.itch.io/roboy)

![Unity](https://img.shields.io/badge/Unity-2020.3.28f1-000000?logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![Platforms](https://img.shields.io/badge/Platforms-WebGL%20%7C%20Windows%20%7C%20macOS-blue)
![Status](https://img.shields.io/badge/Status-Alpha%20%E2%80%94%20on%20hold-orange)

<!-- Add a gameplay GIF here: ![Roboy gameplay](docs/roboy-gameplay.gif) -->

</div>

---

> [!NOTE]
> **Unfinished.** Roboy is a playable alpha, and development has been on hold since 2023, with plans to pick it back up. What's below is what works today.

## ✨ Features

- **Four upgrades that change how you move**
  - **Mover**: sprint
  - **Grappler**: wall jumps
  - **Battery**: higher jumps
  - **Rocket**
- **50+ pieces of scrap** to find across three areas
- **Place your own spawn point** mid-level, with zones where you can't
- **New Game+** carries your upgrades into the next run
- **Speedrun timer** tracks your fastest and total time, plus a death counter
- **Controller support**: keyboard, Xbox, PlayStation, and generic gamepads *(an analog stick plays best)*

## 👥 Credits

| Name | Role |
|---|---|
| **Clint Wagoner** | Programming, Music & Sound, Character & Game Design |
| **Jason Stark** | Sprite Art |

A **Red Rook Interactive** game.

## 🛠️ Tech

- **Engine:** Unity 2020.3.28f1 (LTS), C#
- **Physics:** 2D `Rigidbody2D`, with raycast-based ground and wall detection
- **Animation:** Unity Animator state machines for the player and enemies
- **Builds:** WebGL, Windows, macOS

## 🧱 How It's Built

All gameplay code lives in `Assets/`, hand-written in C#.

| Area | Scripts | What they do |
|---|---|---|
| **Player** | `PlayerController` `PlayerGrounded` `PlayerHealth` `PlayerAnimation` | Movement, jump charge economy, spawn-point placement, death and respawn, three-ray ground check, wall detection |
| **Upgrades** | `ComponentMover` `ComponentGrappler` `ComponentBattery` `ComponentRocket` `ItemManager` | Pickups that unlock abilities and manage the charge meter |
| **Items** | `BasicItemSmallCharge` `BasicItemLargeCharge` `BasicItemOverCharge` `Scrap` | Charge refills, the temporary overcharge state, collectibles |
| **World** | `Piston` `PistonSide` `DoorButton` `MainDoor` `Portal` `SpawnPoint` | Hazards, doors, and level flow |
| **Enemies** | `ArmorBot` `DozerController` | Enemy behavior |
| **Game State** | `GameManager` `GameData` | Upgrades, scrap, deaths, and times held in a static class for the session, so they carry across scenes and into New Game+ *(nothing is saved to disk)* |
| **UI** | `TimerController` `ScrapCounter` `HoverText` `Pause` + more | HUD, speedrun timer, tutorial prompts, pause screen |
| **Audio** | `AudioManager` `Music` `Sound` | Music and sound effect playback |

## ⚠️ Known Limitations

An early project, and the code shows it:

- `PlayerController` handles input, movement, death, respawn, and debug tools in one class. It would be split up today.
- Many fields are `public` where `[SerializeField] private` is the better choice.
- Systems find each other with `FindObjectOfType` at startup, and health is polled every frame instead of raised as an event.
- Animation states and tags are string literals, so a typo fails silently.

## ▶️ Running the Project

1. Install **Unity 2020.3.28f1** through Unity Hub.
2. Clone this repo and open the folder in Unity Hub.
3. Open **`01-Start`** from ``Assets/`Scenes`` and press **Play**.

> [!NOTE]
> The scenes folder name starts with a backtick so it sorts first. Scene order is set in **File → Build Settings**. A controller is recommended.

## 📜 Version History

Development ran from early 2022 into 2023, and the project has been on hold since, with more planned. It used **Unity Version Control (Plastic SCM)**. This repository was created afterward, so its commit history doesn't reflect that work. Devlogs covering development are on the [itch.io page](https://redrookinteractive.itch.io/roboy).

---

<div align="center">

© 2022–2023 **Red Rook Interactive**. All rights reserved.

</div>
