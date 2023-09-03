use bevy::{prelude::*, window::PrimaryWindow, audio::* };
use rand::prelude::*;

pub const PLAYER_ACCELERATION: f32 = 50.0;
pub const PLAYER_MAX_SPEED: f32 = 1000.0;
pub const PLAYER_SIZE: f32 = 32.0;

fn main() {
    App::new()
        .add_plugins(DefaultPlugins)
        .add_systems(Startup, setup)
        .add_systems(Update, entity_movement)
        .add_systems(Update, player_control)
        .add_systems(Update, wrap_screen)
        .add_systems(Update, entity_collision_system)
        .run();
}

#[derive(Component)]
pub struct Movable {
    velocity: Vec3
}

#[derive(Component)]
pub struct OnScreenEntity { }

#[derive(Component)]
pub struct PlayerShip {}

#[derive(Component)]
pub struct Enemy {}

fn setup(mut commands: Commands, asset_server: Res<AssetServer>, window_query: Query<&Window, With<PrimaryWindow>>) {
    let window = window_query.get_single().unwrap();

    commands.spawn(Camera2dBundle {
        transform: Transform::from_xyz(window.width() / 2.0, window.height() / 2.0, 0.0),
        ..default()
    })
    ;
    commands.spawn((
        PlayerShip {},
        Movable {
            velocity: Vec3::ZERO
        },
        OnScreenEntity {},
        SpriteBundle {
            texture: asset_server.load("sprites/players_ships/ship-blue-01.png"), 
            transform: Transform::from_xyz(window.width() / 2.0, window.height() / 2.0, 0.0),
            ..default()
        }
    ));

    for _ in 0..4 {
        let random_x = random::<f32>() * window.width();
        let random_y = random::<f32>() * window.height();
        let direction_x = random::<f32>() * if random::<bool>() { 1.0 } else { -1.0 };
        let direction_y = random::<f32>() * if random::<bool>() { 1.0 } else { -1.0 };
        let speed = (random::<f32>() + 0.1) * 300.0;
        let velocity = Vec3::new(direction_x, direction_y, 0.0) * speed;


        commands.spawn(
            (
                SpriteBundle {
                    transform: Transform::from_xyz(random_x, random_y, 0.0),
                    texture: asset_server.load("sprites/asteroids/asteroid-big-01.png"),
                    ..default() 
                },
                Enemy {},
                Movable {
                     velocity: velocity
                },
                OnScreenEntity {}
            )
        );
    }
}

pub fn entity_movement(mut entity_query: Query<(&mut Transform, &Movable)>, time: Res<Time>) {
    for (mut transform, movable) in entity_query.iter_mut() {
        transform.translation += movable.velocity * time.delta_seconds();
    }
} 

pub fn entity_collision_system(mut commands: Commands, mut entity_query: Query<(Entity, &Transform), With<OnScreenEntity>>){
    for (entity, transform) in entity_query.iter() {
        for (entity2, transform2) in entity_query.iter() {

            if (entity == entity2)
            {
                continue;
            }

            let distance = transform.translation.distance(transform2.translation);

            if distance < 32.0 { // TODO: make radius calculation dynimic
                commands.entity(entity).despawn();
            }

        }
    }
}

pub fn player_control (keyboard_input: Res<Input<KeyCode>>, mut player_query: Query<&mut Movable, With<PlayerShip>>) {
    if let Ok(mut movable) = player_query.get_single_mut(){
        let mut direction = Vec3::ZERO;

        if keyboard_input.pressed(KeyCode::Left) || keyboard_input.pressed(KeyCode::A){
            direction += Vec3::new(-1.0, 0.0, 0.0);
        }

        if keyboard_input.pressed(KeyCode::Right) || keyboard_input.pressed(KeyCode::D){
            direction += Vec3::new(1.0, 0.0, 0.0);
        }

        if keyboard_input.pressed(KeyCode::Up) || keyboard_input.pressed(KeyCode::W){
            direction += Vec3::new(0.0, 1.0, 0.0);
        }

        if keyboard_input.pressed(KeyCode::Down) || keyboard_input.pressed(KeyCode::S){
            direction += Vec3::new(0.0, -1.0, 0.0);
        }

        if direction.length() > 0.0{
            direction = direction.normalize();
        }

        let velocity = movable.velocity + direction * PLAYER_ACCELERATION;
        movable.velocity = if velocity.length() > PLAYER_MAX_SPEED { velocity.normalize() * PLAYER_MAX_SPEED } else { velocity };
    }
}

pub fn wrap_screen(mut query: Query<&mut Transform, With<OnScreenEntity>>, window_query: Query<&Window, With<PrimaryWindow>>) {

    let window = window_query.get_single().unwrap();

    for mut transform in query.iter_mut() {

        if transform.translation.x < 0.0 {
            transform.translation.x = window.width();
        }

        if transform.translation.x > window.width() {
            transform.translation.x = 0.0;
        }

        if transform.translation.y < 0.0 {
            transform.translation.y = window.height();
        }

        if transform.translation.y > window.height() {
            transform.translation.y = 0.0;
        }
    }
}