# Godot (C#)


Positives:
* You don't need to install engine it self, just download binaries
* The metadata files simple/easy to read/modify
* C# Support
* Easy/flexible UI system


Negatives:
* You have to install export sdks locally for each engine version
* C# code editor in Godot IDE bad: no intelicense, default scripts are bad etc.
* Some of runtime exeptions doesn't fail the game (e.g. when can't set body disabled true)
* The pop up about reloading script every time I edit it in VS is annoying
* Godot constantly do something with newlines in none code files
* UI system not exaclty intuitive and sometimes frustrating
* String literals everywhere: load resource, create scene instance, get a node in script
* Particle system is not reach enough for my needs
* Exported version of a game works different from one runs in the editor

TODO: 
- [X] Main Menu
  - [X] Add Start Sky
  - [X] Add and implement layout
  - [X] Add Music
- [_] Settings Menu
- [X] Game Play
  - [X] Add star sky
  - [X] Add players ship
    - [X] Basics
    - [X] Add playses ship death particles
    - [X] Add lazer
    - [X] Add lazer sfx
    - [X] Add death sound
  - [X] Add asteroids
    - [X] Introduce ship to asteroids collisions
    - [X] Add particle effects to Asteroids death
    - [X] Add death sound
  - [X] Add UFO
    - [X] Implement basics
    - [X] Implement collision with asteroid
    - [X] Add particle effects to UFO death
    - [X] Add Death sound
    - [X] Add blaster
      - [X] Collision with Player ship
      - [X] Collision with Asteroids
      - [X] Add sfx
  - [X] Game Loop
    - [X] Add generation of small/medium/tiny steroids
    - [X] Add ufo generation
  - [X] Add game over screen
  - [X] Add game play exit  
  - [X] Add Music
  - [X] Implement HUD
    - [X] Add scores
    - [X] Add lives
- [X] Add controller support