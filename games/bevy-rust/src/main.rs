mod star_sky_plugin;
mod game;
mod main_menu;

use bevy::{ prelude::*, window::PrimaryWindow };

use star_sky_plugin::SkyPlugin;
use main_menu::MainMenuPlugin;
use game::GamePlugin;

fn main() {
    App::new()
        .add_plugins(DefaultPlugins)
        .add_state::<AppState>()
        .add_plugins(SkyPlugin)
        .add_plugins(MainMenuPlugin)
        .add_plugins(GamePlugin)
        .add_systems(Startup, bootstrup_system)        
        .insert_resource(ClearColor(Color::BLACK))     
        .run();
}

#[derive(States, Debug, Clone, Copy, Eq, PartialEq, Hash, Default)]
pub enum AppState {
    #[default]
    Bootstrup,
    MainMenu,
    Game
}

pub fn bootstrup_system(
    window_query: Query<&Window, With<PrimaryWindow>>,
    mut commands: Commands,
    mut next_state: ResMut<NextState<AppState>>
) {
    let window = window_query.get_single().unwrap();    
    commands.spawn(Camera2dBundle {
        transform: Transform::from_xyz(window.width() / 2.0, window.height() / 2.0, 0.0),
        ..default()
    });
    next_state.set(AppState::MainMenu);
}