package objects;

import ceramic.Quad;
import ceramic.Texture;

class Ship extends Quad {

    static public function Create(sprite: Texture, x: Int, y: Int) : Ship {
        var ship = new Ship();
        ship.texture = sprite;
        ship.x = x;
        ship.y = y;

        return ship;
    }
}