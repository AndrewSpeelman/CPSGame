# Oracle: A Cyber Physical Systems Security Game
Oracle is a game designed for young children, in order to help them develop
an intuition as to the workings of cyber physical systems and the attacks they
face. By gamifying the experience of defending a CPS from an attacker, we hope
to assist in developing critical thinking and deduction skills, which
are crucial in the world of security, and are valuable life skills.
We hope to inspire curiosity and interest in cyber physical systems, and to
bring more attention to what is often cited as the most important sector of
security today.

The aim of the mechanics in Oracle is to show that successful operation of a
cyber-physical system is an exercise in endurance. There will always be an
attacker, and while the defender can be taken offline, the attacker cannot;
another attacker can always replace a defeated one. We hope that making this
apparent will make the nature of security more obvious to those who have not
experienced the field (e.g. children) and inspire interest.

# Unity 
## How To Install
For this project, the program used was Unity3d and was coded in C#. Unity 3D can
be downloaded [here](https://unity3d.com/get-unity/download).
This will download the unity hub were you can download the
latest build of unity. We all used the Personal version of unity as it is free.
When installing a new version of unity, make sure in the `Platforms` to install
the WebGL Build Support. (We do not currently have anything that should be
broken if new updates are released, but if the project appears to have problems,
our latest working build we used was Unity 2018.3.12f1).
 
## How to Load Project
Once you have installed unity and have the project code, using the Unity Hub,
you can open the project. There will be an icon that says “Open” with an up
arrow. Click on that and it will bring open the file explorer to select a
project. The Project is the **parent folder** of the code. It should be named
`CPSGame`.

This file contains directly in it all the code for our project and is what
should be selected to open the project. Once the project is loaded up there will
be a blank scene. To load up our game, in the 
Project tab under the display window, go to `Assets/Scenes` and drag `GameScene`
to the Hierarchy tab (To ensure it works properly, make sure GameScene is
the only scene loaded into the hierarchy).
Another thing to make sure the game works properly is to set a set resolution
for the game to run in. Our games resolution is expected to run at 1920x1080 and
if it is not, the UI will not work properly. (See image below on how to set it).

# Technical Overview
## The `Assets` Folder
Assets Folder holds all of the code logic that we built.
Within the Assets folder we have 3 other folders:GameLogic,Modules,and Scenes

Scenes Generally work with how our screen notifies that it is the Start of the
Game, End of the Game, Attacker’s Turn, Defender’s Turn, and other small
changes outside of the actual game board.

GameLogic generally manages the turns, be it the defender’s turn or the
attacker’s turn. Think of it as a turn manager. `GameController.cs` manages the
turns and does some backend calculation depending on the previous turns,
attacker and defender. `TickModules()`, when called, allows the game know that
the flow of water should be moved down the pipes or prefabs in this case.
When you go up to the programming hierarchy you could see that inside the
`WaterFlowController.cs` within the `TickModules()` function, the
ReservoirTick() function is the one that initiates the movement of
water.

## The `Defender` Folder
Defender Folder houses Owl Logic.
`Oracle.cs` handles owl logic and calls upon `Valuation.cs` when
using the Owl pointers. It then applies the rule specified and checks whether or
not the rule is broken, if the rule is broken, `Valuation.cs` will handle the
problem.

The owl also fixes the attack _**if**_ the attack is cornered by the owl with a
set of 3 objects (i.e. `Sensor1` `Filter1` `Sensor2`). The `ApplyRule()`
function looks at the 2 owl points, which is called by the
`ModuleMatchesExpected()`, and compares the expected and actual values between
the sensor/filter data and the water contained
in the object.

## The `Attacker` Folder
The `Attacker` Folder just houses some of the prefabs used within gameplay.
Modules houses the prefabs (i.e. objects on screen like a `Filter`) and scripts
attached to those prefabs. These scripts are triggered by users' action during
gameplay.

`Module.cs` handles the user actions on the game screen like clicking on a
prefab during the attackers turn.

## The `Script` Folder
Script Folder holds all the scripts that we attach to the prefabs to manage
data/object management given a specific event handler.

## Key points to remember
  1. Water is ticked from the end of the line (reservoir) to the front.
  Basically water is dumped into the reservoir from the last pipe, then it goes
  up the line up to the first pipe. This is to negate the necessity of adding a
  `CopyWaterObject`, as we would just need to bring the past `WaterObject`
  down the list.
  2. Generally managing object creation and script attachment requires some
  programming knowledge of Unity, and learning the basics of prefab creation and
  script writing will be greatly useful before attempting to update or work with
  the given code.

# Current Bugs
  - One Bug
  - Two Bug
  - Red Bug
  - Blue Bug

# Future Work
## User Interface
  - Eye color of owl matches what he can see
  - Have an object to represent the attacker and display what the attacker can/can't see
  - Make UI ADA compliant (specifically in regard to colorblindness)
  - When sensor is attacked, it should only be affected for a set number of turns

## Game Mechanics
  - Implement a Help system
  - Defender should receive points for water in reservoir, cleanliness of water, etc.
    + currently, Defender receives points for water flow and thwarting attacks
  - Add ability to destroy objects on map (ie. burst pipe, burst water tank…)
  - Pump should have variation of flow. (ie. 50% open)
  - Attacker should be able to attack owls

## Misc Features
  - Add more invariants to better mimic a real Cyber Physical System
  - Add random map generation
