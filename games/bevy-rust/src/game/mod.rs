mod components;
mod events;
mod resources;
mod systems;

use bevy::prelude::*;
use events::*;
use resources::*;
use systems::*;

use crate::AppState;

pub struct GamePlugin;

#[derive(States, Debug, Clone, Copy, Eq, PartialEq, Hash, Default)]
pub enum GameState {
    #[default]
    Running, 
    Paused                                                  
}

impl Plugin for GamePlugin {
    fn build(&self, app: &mut App) {
        app
            .init_resource::<Score>()
            .init_resource::<AsteroidSpawnTimer>()
            .add_state::<GameState>()
            .add_event::<GameOver>()
            .add_systems(OnEnter(AppState::Game), setup)
            .add_systems(
                Update,
                    (
                        entity_movement,
					    player_control,
					    clear_out_screen.before(wrap_screen),
					    wrap_screen,
					    entity_collision_system,
					    update_score,
					    initiate_asteroids_spawn,
					    spawn_asteroids,
					    exit_game,
					    handle_game_over,
                        toggle_state
                    ).run_if(in_state(AppState::Game))
            );
    }
}