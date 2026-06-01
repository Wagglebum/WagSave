# WagSave — Simple Sample

A minimal 2D demo showing how to integrate WagSave into a Unity project. The player moves around a bounded arena, collecting enemies to score points. Save, load, and clear game state using the on-screen UI.

## Requirements

- Unity 6 or later
- WaggleBum WagSave package

## Setup

1. Install the WagSave package via the Unity Package Manager.
2. Open the project in Unity.
3. Open `Assets/Scenes/SampleScene`.
4. Press **Play**.

## Controls

| Key | Action |
|-----|--------|
| W / A / S / D | Move the player |
| Space | Pause / unpause |

## UI Buttons

| Button | Action |
|--------|--------|
| Save | Saves the current player position and score |
| Load | Restores the last saved player position and score |
| Reset | Resets score and respawns enemies (does not load a save) |
| Clear | Resets score and removes all enemies without saving |

## How it works

- **Scoring** — The player scores a point each time they touch an enemy. The enemy is destroyed on contact.
- **Enemies** — 5 enemies spawn at random positions within the arena and roam to random waypoints continuously.
- **Saving** — `GameController` calls `WagSave.Save()` / `WagSave.Load()`. The status label updates on `OnSaveCompleted` and `OnLoadCompleted` callbacks.
- **WagSave guard** — All WagSave calls are wrapped in `#if WAGGLEBUM_WAGSAVE` so the project compiles cleanly without the package installed (a console error is shown instead).
