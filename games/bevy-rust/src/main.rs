//! Renders a 2D scene containing a single, moving sprite.

use bevy::prelude::*;

fn main() {
    App::new()
        .add_plugins(DefaultPlugins)
        .add_systems(Startup, setup)
        .run();
}

#[derive(Component)]
pub struct PlayerShip {}

fn setup(mut commands: Commands, asset_server: Res<AssetServer>) {
    commands.spawn(Camera2dBundle::default());
    commands.spawn((
        PlayerShip {},
        SpriteBundle {
            texture: asset_server.load("sprites/players_ships/ship-blue-01.png"),
            ..default()
        }
    ));
}