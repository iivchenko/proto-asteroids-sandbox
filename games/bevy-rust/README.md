# Bevy (Rust)


Positives:
* You don't need to install engine to your mahince, it will be part of the your poject dependency

Negatives:
* You have to install rust/cargo locally
* Very slow on initial compile
* Slow on next compiles 
* UI is painful, very flexible but very painful.

TODO: 
- [ ] Main Menu
  - [X] Add Start Sky
  - [ ] Add and implement layout
  - [ ] Add Music
- [ ] Settings Menu
- [ ] Game Play
  - [X] Add star sky
  - [ ] Add players ship
    - [X] Basics (Keyboard control, Movement, Sprite Selection)
    - [ ] Add playses ship death particles
    - [X] Add lazer
    - [ ] Add lazer sfx
    - [ ] Add death sound (sfx https://www.youtube.com/watch?v=4TjEo-gDgAg)
  - [ ] Add asteroids
    - [ ] Introduce ship to asteroids collisions
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
    - [ ] Add generation of small/medium/tiny steroids
    - [ ] Add ufo generation
  - [ ] Add game over screen
  - [ ] Add game play exit  
  - [ ] Add Music
  - [ ] Implement HUD
    - [ ] Add scores
    - [ ] Add lives
- [ ] Add controller support