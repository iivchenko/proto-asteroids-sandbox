package scenes;

import ceramic.Color;
import ceramic.Quad;
import ceramic.Scene;
import ceramic.Timer;

class StarScene extends Scene {

    var stars: Array<ceramic.AssetId<String>> = [
        Images.SPRITES__STARS__STAR_01,
        Images.SPRITES__STARS__STAR_02,
        Images.SPRITES__STARS__STAR_03,
        Images.SPRITES__STARS__STAR_04,
        Images.SPRITES__STARS__STAR_05
    ];

    override function preload() {
        for (star in stars) {
            assets.add(star);
        }
    }

    override function create() {

        log.info(Math.random());

        var block = 96;
        for (x in 0...Math.round(width/block))
            for (y in 0...Math.round(height/block)) {
                if (Math.random() > 0.8)
                    continue;

                var star = new Quad();
                star.texture = assets.texture(stars[Math.floor(Math.random() * 0.99999 * stars.length)]);
                star.color = Color.random();
                star.anchor(0.5, 0.5);
                star.pos(Math.random() * 96 + x * block, Math.random() * 96 + y * block);
                star.scale(Math.random() * 1.4);
                star.rotation = Math.random() * 360;
                star.alpha = 0.1;

                Timer.delay(this, Math.random() * 10, function () {
                    show(star);
                });                

                add(star);
            }
    }

    function show(star:Quad) {
        star.tween(SINE_EASE_IN_OUT, 5, 0.1, 0.75, (value, time) -> {
            star.alpha = value;
        })
        .onComplete(this, () -> {
            hide(star);
        });
    }

    function hide(star:Quad) {
        star.tween(SINE_EASE_IN_OUT, 5, 0.75, 0.1, (value, time) -> {
            star.alpha = value;
        })
        .onComplete(this, () -> {
            show(star);
        });
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
