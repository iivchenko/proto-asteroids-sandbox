package objects;

import ceramic.Quad;
import ceramic.Texture;

class Ship extends Quad {

    static public function Create(sprite: Texture, x: Int, y: Int) : Ship {
        var ship = new Ship();
        ship._texture = sprite;
        ship.anchor(0.5, 0.5);
        ship.pos(x, y);

        return ship;
    }
}