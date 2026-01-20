# 🐸 Frog's Adventure

> **A Pixel-Art Action Platformer developed in Unity.**
> *Jump, Dash, and Morph to save the Lilypad Kingdom!*

![Game Banner](https://via.placeholder.com/800x400?text=Frog%27s+Adventure+Banner)
## 🚧 Project Status: Work in Progress (WIP)
**Current State:** Development / Portfolio Project
* **Content:** 3 Playable Levels (World 1) + 1 Special Demo Level.
* **Goal:** This project is currently in active development. The "Demo Level" is designed to showcase all unlockable mechanics immediately.

---

## 📖 About The Game

**Frog's Adventure** is a 2D side-scrolling platformer where players control **Frobert**, a brave frog on a quest to rescue **Princess Lily** from the evil **Dragon Lord Drako**.

Unlike typical platformers, Frobert evolves throughout the journey. Starting with a simple jump, players collect **Cherries** 🍒 to buy permanent upgrades from a **Skill Tree** and use magical **Fruit Power-Ups** to solve environmental puzzles.

### 📜 The Story
The Lilypad Kingdom was peaceful until Drako kidnapped Princess Lily to steal her magic. Frobert, the kingdom's only hope, must travel through the **Whispering Forest**, the tricky **Pink Lands**, and finally infiltrate **Drako's Castle**. But rumors say Frobert is no ordinary frog... hidden **Pineapples** 🍍 scattered across the world might reveal his true form!

---

## 🎮 Gameplay Features

### ⚔️ Progression & Skills ("The Frog's Path")
Managed via `GameManager.cs`, players use collected Cherries to unlock abilities permanently:
* **Wall Jump & Double Jump:** Access new heights.
* **Dash:** Dodge attacks and cross wide gaps.
* **Shuriken Mastery:** Unlock ranged combat (Finite or Infinite ammo).
* **Extra Health:** Survive longer against bosses.

### 🍌 Puzzle-Solving Power-Ups
Temporary abilities (handled by `PlayerPowerUps.cs`) add a puzzle layer to the platforming:
* **Banana (Shrink):** 🍌 Shrink down to fit into tiny tunnels.
* **Strawberry (Invisibility):** 🍓 Bypass security cameras and enemies.
* **Kiwi (Magnet):** 🥝 Attract all nearby collectibles automatically.
* **Watermelon (Heavy Weight):** 🍉 Smash through cracked floors and activate heavy pressure plates.

### 🏆 The "True Ending" Secret
A unique mechanic for completionists:
* Find all **9 Hidden Pineapples** throughout the game.
* Unlock the secret door at the start.
* *Completing the game with all pineapples triggers a special cutscene revealing Frobert's human prince form!* 🤴

---

## 📸 Screenshots

| **Classic Platforming** | **Combat & Traps** |
|:---:|:---:|
| ![Level 1](https://via.placeholder.com/400x250?text=Whispering+Forest) | ![Combat](https://via.placeholder.com/400x250?text=Combat+System) |
| *Navigating the Whispering Forest* | *Fighting Slimes and dodging Traps* |

| **Skill Tree UI** | **Puzzle Solving** |
|:---:|:---:|
| ![Skill Tree](https://via.placeholder.com/400x250?text=Skill+Tree+System) | ![PowerUps](https://via.placeholder.com/400x250?text=Fruit+Power+Ups) |
| *Upgrading Frobert's abilities* | *Using Banana to shrink* |

### 🎥 Gameplay Video
[![Watch the Video](https://via.placeholder.com/600x300?text=Watch+Gameplay+Demo)](LINK_TO_YOUR_YOUTUBE_VIDEO_HERE)

---

## 🛠️ Technical Implementation

This project demonstrates solid **Object-Oriented Programming (OOP)** principles and Unity best practices:

* **Singleton Pattern:** Used in `GameManager`, `SoundManager`, and `UIManager` for centralized global access to game state and audio.
* **Save/Load System:** Utilizing `PlayerPrefs` to persist Skill Tree unlocks, collected Pineapples (`PineappleCollectible.cs`), and high scores between sessions.
* **Coroutines:** Extensively used for timed Power-Up effects (e.g., `BananaRoutine` inside `PlayerPowerUps.cs`) and smooth UI transitions.
* **Object Pooling:** Implemented for projectiles (`Projectile.cs`) and traps (`ArrowTrap.cs`) to minimize garbage collection and improve performance.
* **Finite State Logic:** Player states (Idle, Run, Jump, Fall, Attack) are managed via Animator parameters driven by `PlayerMovement.cs`.

---

## 💻 How to Run the Project

Since the game is currently in development, there is no pre-built `.exe` file. You can run the project directly in the Unity Editor:

1.  **Clone the Repository:**
    ```bash
    git clone [https://github.com/YOUR_USERNAME/FrogsAdventure.git](https://github.com/batusalcan/Frogs-Adventure-Game.git)
    ```
2.  **Open in Unity:**
    * Launch **Unity Hub**.
    * Click **"Add"** and select the cloned project folder.
    * Open the project (Recommended Unity Version: **2022.3 LTS** or newer).
3.  **Play:**
    * Go to `Assets/Scenes`.
    * Open **"MainMenu"** scene.
    * Press the **Play** button at the top.
    * *Tip: You can also open "DemoLevel" to test all abilities instantly.*

---

## 🕹️ Controls

| Key | Action |
| :--- | :--- |
| **A / D** (or Arrows) | Move Left / Right |
| **W / Space** | Jump (Hold for higher jump) |
| **Left Shift** | Dash (Unlockable) |
| **Mouse Left Click** | Shuriken Attack (Unlockable) |
| **ESC** | Pause Menu |

---

## 👨‍💻 Credits

**Developer:** Batuhan Salcan  
**Engine:** Unity (C#)  
**Art Assets:** Pixel Adventure (Unity Asset Store)

---

*Thank you for visiting my portfolio project! Detailed documentation is available in the `GameDesignDocument.pdf` included in the repo.*
