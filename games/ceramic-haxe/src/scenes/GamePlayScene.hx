package scenes;

import ceramic.AssetId;
import objects.Ship;
import ceramic.Scene;

class GamePlayScene extends Scene {

    var shipSprites: Array<AssetId<String>> = [
        Images.SPRITES__PLAYER_SHIPES__SHIP_BLUE_01,
        Images.SPRITES__PLAYER_SHIPES__SHIP_BLUE_02
    ];

    var shipSprite: AssetId<String>;

    override function preload() {
        assets.add(Images.SPRITES__PLAYER_SHIPES__SHIP_BLUE_01);
        assets.add(Images.SPRITES__PLAYER_SHIPES__SHIP_BLUE_02);
    }

    override function create() {

        shipSprite = Random.fromArray(shipSprites);
        var ship = Ship.Create(assets.texture(shipSprite), Math.round(width / 2), Math.round(height / 2));

        add(ship);
    }

    override function update(delta:Float) {

        // Here, you can add code that will be executed at every frame

    }

    override function resize(width:Float, height:Float) {

        // Called everytime the scene size has changed

    }

    override function destroy() {

        // Perform any cleanup before final destroy

        super.destroy();

    }

}
