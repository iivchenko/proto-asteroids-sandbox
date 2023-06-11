package;

import ceramic.Entity;
import ceramic.Color;
import ceramic.InitSettings;

class Project extends Entity {

    function new(settings:InitSettings) {

        super();

        settings.antialiasing = 0;
        settings.background = Color.BLACK;
        settings.targetWidth = 1920;
        settings.targetHeight = 1080;
        settings.scaling = FIT;
        settings.resizable = true;
        settings.fullscreen = true;

        app.onceReady(this, ready);
    }

    function ready() {

        app.scenes.set("stars", new scenes.StarScene());
        app.scenes.main = new scenes.GamePlayScene();
    }
}
