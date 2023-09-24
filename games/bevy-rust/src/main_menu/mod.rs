use bevy::prelude::*;
use bevy::app::AppExit;

use crate::AppState;

pub struct MainMenuPlugin;

impl Plugin for MainMenuPlugin {
    fn build(&self, app: &mut App) {
        app
            .add_systems(OnEnter(AppState::MainMenu), setup_system)
            .add_systems(
                Update,
                (
                    interact_with_play_button,
                    interact_with_exit_button
                ).run_if(in_state(AppState::MainMenu))
            );
            //  .add_systems(
            //      Update, 
            //      (start_game_system).run_if(in_state(AppState::MainMenu)));
    }
}

#[derive(Component)]
pub struct PlayButton {}

#[derive(Component)]
pub struct ExitButton {}

fn setup_system (
    mut commands: Commands,
    asset_server: Res<AssetServer>
) {
    print!("Main Menu");
    let main_menu_entity = commands.spawn((
        NodeBundle {
                style: Style {
                flex_direction: FlexDirection::Column,
                justify_content: JustifyContent::Center,
                align_items: AlignItems::Center,                
                width: Val::Percent(100.0),
                height: Val::Percent(100.0),
                column_gap: Val::Px(8.0),
                row_gap: Val::Px(8.0),
                ..default()
            },
            ..default()
        },
        //MainMenu {}
    ))
    .with_children(|parent|{
        // === Title ===
        parent.spawn(
            NodeBundle {
                style: Style {
                    flex_direction: FlexDirection::Row,
                    justify_content: JustifyContent::Center,
                    align_items: AlignItems::Center,
                    width: Val::Px(300.0),
                    ..default()
                },
                ..default()
            }).with_children(|parent|{
                parent.spawn(
                    TextBundle {
                        text: Text {  
                            sections: vec![
                                TextSection::new(
                                    "Proto Asteroids", 
                                    TextStyle { 
                                        font: asset_server.load("fonts/kenney-future.font.ttf"),  
                                        font_size: 128.0,
                                        color: Color::WHITE
                                    })
                            ],
                            alignment: TextAlignment::Center, 
                            ..default()
                        },
                        ..default()
                    });
            });

        // === Play Button ===
        parent.spawn((
            ButtonBundle {
                style: Style {
                    justify_content: JustifyContent::Center,
                    align_items: AlignItems::Center,
                    width: Val::Px(200.0),
                    height: Val::Px(80.0),
                    ..default()
                },
                background_color: Color::rgb(0.15, 0.15, 0.15).into(),
                ..default()
            },
            PlayButton {}
        ))
        .with_children(|parent| {
            parent.spawn(
                TextBundle {
                    text: Text {  
                        sections: vec![
                            TextSection::new(
                                "Play", 
                                TextStyle { 
                                    font: asset_server.load("fonts/kenney-future.font.ttf"),  
                                    font_size: 32.0,
                                    color: Color::WHITE
                                })
                        ],
                        alignment: TextAlignment::Center, 
                        ..default()
                    },
                    ..default()
                });
        });

         // === Exit Buttion ===
         parent.spawn((
            ButtonBundle {
                style: Style {
                    justify_content: JustifyContent::Center,
                    align_items: AlignItems::Center,
                    width: Val::Px(200.0),
                    height: Val::Px(80.0),
                    ..default()
                },
                background_color: Color::rgb(0.15, 0.15, 0.15).into(),
                ..default()
            },
            ExitButton {}
        ))
        .with_children(|parent| {
            parent.spawn(
                TextBundle {
                    text: Text {  
                        sections: vec![
                            TextSection::new(
                                "Exit", 
                                TextStyle { 
                                    font: asset_server.load("fonts/kenney-future.font.ttf"),  
                                    font_size: 32.0,
                                    color: Color::WHITE
                                })
                        ],
                        alignment: TextAlignment::Center, 
                        ..default()
                    },
                    ..default()
                });
        });
    })
    .id();
}


fn interact_with_play_button (
    mut button_query: Query<(&Interaction, &mut BackgroundColor), (Changed<Interaction>, With<PlayButton>)>,
    mut app_state_next_state: ResMut<NextState<AppState>>
){
    if let Ok((interaction, mut background_color)) = button_query.get_single_mut(){
        match *interaction {
            Interaction::Pressed => {
                *background_color = Color::rgb(0.20,  0.20, 0.20).into();
                app_state_next_state.set(AppState::Game);
            }, 
            Interaction::Hovered => {
                *background_color = Color::rgb(0.35,  0.35, 0.35).into();
            }, 
            Interaction::None => {
                *background_color = Color::rgb(0.25,  0.25, 0.25).into();
            }
        }
    }
}

fn interact_with_exit_button (
    mut button_query: Query<(&Interaction, &mut BackgroundColor), (Changed<Interaction>, With<ExitButton>)>,
    mut app_exit_event_writer: EventWriter<AppExit>
){
    if let Ok((interaction, mut background_color)) = button_query.get_single_mut(){
        match *interaction {
            Interaction::Pressed => {
                *background_color = Color::rgb(0.20,  0.20, 0.20).into();
                app_exit_event_writer.send(AppExit);
            }, 
            Interaction::Hovered => {
                *background_color = Color::rgb(0.35,  0.35, 0.35).into();
            }, 
            Interaction::None => {
                *background_color = Color::rgb(0.25,  0.25, 0.25).into();
            }
        }
    }
}