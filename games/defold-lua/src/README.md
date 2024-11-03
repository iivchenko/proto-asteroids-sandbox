# Defold (Lua)


Positives:
* Main focus on the code and less focus on the visual composing
* You don't need to install engine it self, just download binaries
* Game engine have a small size
* The metadata files simple/easy to read/modify


Negatives:
* Identifier in the code to objects and components presented as strings so typos will fail during runtime
* I can't concatenate id (of an go created with factory) with component string name
* I can't define public properties/methods in go to be awailable from parents
* Editor doesn't support checkboxes in MD files
* Editor doesn't support MD files editing when in text mode
* I have my doubts about messaging system

TODO: 
- [ ] Main Menu
  - [ ] Add Start Sky
  - [ ] Add and implement layout
  - [ ] Add Music
- [ ] Settings Menu
- [ ] Game Play
  - [ ] Add star sky
  - [ ] Add players ship
    - [x] Basics
    - [ ] Add playses ship death particles
    - [ ] Add lazer
    - [ ] Add lazer sfx
    - [ ] Add death sound
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