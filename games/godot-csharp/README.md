# Godot (C#)


Positives:
* You don't need to install engine it self,  just download binaries
* The metdata files siple/easy to read/modify

Negatives:
* You have to install export sdks locally for each engine version
* C# code editor in Godot IDE bad: not intelicense, default scripts are bad etc.
* Some of runtime exeptions doesn't fail the game (e.g. when can't set body disabled true)

TODO: 
- [ ] Main Menu
- [ ] Settings Menu
- [ ] Game Play
  - [X] Add star sky
  - [ ] Add players ship
    - [X] Basics
    - [X] Add playses ship death particles
    - [X] Add lazer
    - [ ] Add lazer sfx
    - [ ] Add death sound
  - [ ] Add asteroids
    - [X] Introduce ship to asteroids collisions
    - [X] Add particle effects to Asteroids death
    - [ ] Add death sound
  - [ ] Add UFO
    - [X] Implement basics
    - [X] Implement collision with asteroid
    - [X] Add particle effects to UFO death
    - [ ] Add Death sound
    - [ ] Add blaster
      - [X] Collision with Player ship
      - [X] Collision with Asteroids
      - [ ] Add sfx
  - [ ] Game Loop
    - [X] Add generation of small/medium/tiny steroids
    - [X] Add ufo generation
  - [ ] Add game over screen
  - [ ] Add game play exit  
  - [ ] Add Music
  - [X] Implement HUD
    - [X] Add scores
    - [X] Add lives
- [ ] Add controller support