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
  - [X] Add players ship
    - [X] Basics
    - [X] Add playses ship death particles
    - [X] Add lazer SFX
  - [X] Add asteroids
    - [X] Introduce ship to asteroids collisions
    - [X] Add particle effects to Asteroids death
  - [ ] Add UFO
    - [ ] Implement basics
    - [ ] Implement collision with asteroid
    - [ ] Add particle effects to UFO death
    - [ ] Add blaster SFX
      - [ ] Collision with Player ship
      - [ ] Collision with Asteroids
  - [ ] Game Loop
    - [X] Add generation of small/medium/tiny steroids 
    - [ ] Add hazards
  - [ ] Add game over screen
  - [ ] Add game play exit  
  - [ ] Add Music
  - [ ] Implement HUD
    - [ ] Add scores
    - [ ] Add lives
- [ ] Add controller support