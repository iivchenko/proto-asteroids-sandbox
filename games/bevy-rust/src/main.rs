mod components;
mod events;
mod resources;
mod systems;

use events::*;
use resources::*;
use systems::*;

use bevy::app::AppExit; 
use bevy::{ prelude::*, window::PrimaryWindow };
use rand::prelude::*;

pub const PLAYER_ACCELERATION: f32 = 50.0;
pub const PLAYER_MAX_SPEED: f32 = 600.0;
pub const PLAYER_SIZE: f32 = 32.0;

fn main() {
    App::new()
        .add_plugins(DefaultPlugins)
        .init_resource::<Score>()
        .init_resource::<AsteroidSpawnTimer>()
        .add_event::<GameOver>()
        .add_systems(Startup, setup)
        .add_systems(Update, entity_movement)
        .add_systems(Update, player_control)
        .add_systems(Update, wrap_screen)
        .add_systems(Update, entity_collision_system)
        .add_systems(Update, update_score)
        .add_systems(Update, initiate_asteroids_spawn)
        .add_systems(Update, spawn_asteroids)
        .add_systems(Update, exit_game)
        .add_systems(Update, handle_game_over)
        .run();
}
