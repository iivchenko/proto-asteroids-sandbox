package scenes;

import ceramic.Quad;
import ceramic.Scene;

class MainMenuScene extends Scene {


    override function preload() {

        // Add any asset you want to load here

    }

    override function create() {

        // Called when scene has finished preloading

        // Print some log
        log.success('Hello from ceramic :)');

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
