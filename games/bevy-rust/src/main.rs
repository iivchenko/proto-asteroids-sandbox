mod star_sky_plugin;
mod components;
mod events;
mod resources;
mod systems;

use star_sky_plugin::SkyPlugin;
use bevy::prelude::*;
use events::*;
use resources::*;
use systems::*;

fn main() {
    App::new()
        .add_plugins(DefaultPlugins)
        .add_plugins(SkyPlugin)
        .insert_resource(ClearColor(Color::BLACK))
        .init_resource::<Score>()
        .init_resource::<AsteroidSpawnTimer>()
        .add_event::<GameOver>()
        .add_systems(Startup, setup)
        .add_systems(Update, entity_movement)
        .add_systems(Update, player_control)
        .add_systems(Update, clear_out_screen)
        .add_systems(Update, wrap_screen)
        .add_systems(Update, entity_collision_system)
        .add_systems(Update, update_score)
        .add_systems(Update, initiate_asteroids_spawn)
        .add_systems(Update, spawn_asteroids)
        .add_systems(Update, exit_game)
        .add_systems(Update, handle_game_over)
        .run();
}
