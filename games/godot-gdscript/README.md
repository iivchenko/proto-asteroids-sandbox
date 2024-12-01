# Godot (GDScript)


Positives:
* You don't need to install engine it self, just download binaries
* The metadata files simple/easy to read/modify
* Easy/flexible UI system


Negatives:
* You have to install export sdks locally for each engine version
* Some of runtime exeptions doesn't fail the game (e.g. when can't set body disabled true)
* The pop up about reloading script every time I edit it in VS is annoying
* Godot constantly do something with newlines in none code files
* UI system not exaclty intuitive and sometimes frustrating
* String literals everywhere: load resource, create scene instance, get a node in script
* Particle system is not reach enough for my needs
* Exported version of a game works different from one runs in the editor

TODO: 
- [ ] Main Menu
  - [ ] Add Start Sky
  - [ ] Add and implement layout
  - [ ] Add Music
- [ ] Settings Menu
- [ ] Game Play
  - [ ] Add star sky
  - [ ] Add players ship
    - [X] Basics
    - [X] Add playses ship death particles
    - [X] Add lazer
    - [X] Add lazer sfx
    - [ ] Add death sound
  - [ ] Add asteroids
    - [X] Introduce ship to asteroids collisions
    - [ ] Add particle effects to Asteroids death
    - [ ] Add death sound
  - [ ] Add UFO
    - [ ] Implement basics
    - [ ] Implement collision with asteroid
    - [ ] Add particle effects to UFO death
    - [ ] Add Death sound
    - [ ] Add blaster
      - [ ] Collision with Player ship
      - [ ] Collision with Asteroids
      - [ ] Add sfx
  - [ ] Game Loop
    - [X] Add generation of small/medium/tiny steroids
    - [ ] Add ufo generation
  - [ ] Add game over screen
  - [ ] Add game play exit  
  - [ ] Add Music
  - [ ] Implement HUD
    - [ ] Add scores
    - [ ] Add lives
- [ ] Add controller support