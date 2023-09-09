use bevy::{ prelude::*, window::PrimaryWindow };
use bevy::app::AppExit;
use rand::prelude::*;

use crate::components::*;
use crate::resources::*;
use crate::events::*;

pub const PLAYER_ACCELERATION: f32 = 50.0;
pub const PLAYER_MAX_SPEED: f32 = 600.0;
pub const PLAYER_SIZE: f32 = 32.0;

pub fn setup(mut commands: Commands, asset_server: Res<AssetServer>, window_query: Query<&Window, With<PrimaryWindow>>) {
    let window = window_query.get_single().unwrap();

    commands.spawn(Camera2dBundle {
        transform: Transform::from_xyz(window.width() / 2.0, window.height() / 2.0, 0.0),
        ..default()
    });

    // TODO: Improve code here
    let sprites = ["sprites/players_ships/ship-blue-01.png", "sprites/players_ships/ship-blue-02.png"];
    let mut rng = thread_rng();
    let sprite = sprites.choose(&mut rng).unwrap();

    commands.spawn((
        PlayerShip {},
        Movable {
            velocity: Vec3::ZERO,
            angular_velocity: 0.0
        },
        OnScreenEntity {},
        SpriteBundle {
            texture: asset_server.load(*sprite), 
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
        let mut rng = rand::thread_rng();
        let angular_velocity = f32::to_radians(rng.gen_range(-50..50) as f32);

        commands.spawn(
            (
                SpriteBundle {
                    transform: Transform::from_xyz(random_x, random_y, 0.0),
                    texture: asset_server.load("sprites/asteroids/asteroid-big-01.png"),
                    ..default() 
                },
                Enemy {},
                Movable {
                     velocity: velocity,
                     angular_velocity: angular_velocity
                },
                OnScreenEntity {}
            )
        );
    }
}

pub fn entity_movement(mut entity_query: Query<(&mut Transform, &Movable)>, time: Res<Time>) {
    for (mut transform, movable) in entity_query.iter_mut() {
        transform.translation += movable.velocity * time.delta_seconds();
        transform.rotate_z(movable.angular_velocity * time.delta_seconds());
    }
} 

pub fn entity_collision_system(mut commands: Commands, mut entity_query: Query<(Entity, &Transform, Option<&PlayerShip>), With<OnScreenEntity>>, mut game_over_event_writer: EventWriter<GameOver>, score: Res<Score>){
    for (entity, transform, maybe_player) in entity_query.iter() {
        for (entity2, transform2, maybe_player2) in entity_query.iter() {

            if entity == entity2
            {
                continue;
            }

            let distance = transform.translation.distance(transform2.translation);

            if distance < 32.0 { // TODO: make radius calculation dynimic
                commands.entity(entity).despawn();

                if maybe_player.is_some() {
                    game_over_event_writer.send(GameOver { score: score.value })

                }
            }

        }
    }
}

pub fn player_control (keyboard_input: Res<Input<KeyCode>>, mut player_query: Query<(&Transform, &mut Movable), With<PlayerShip>>) {
    if let Ok((transform, mut movable)) = player_query.get_single_mut(){

        let mut rotation_factor = 0.0;

        if keyboard_input.pressed(KeyCode::Left) || keyboard_input.pressed(KeyCode::A) {
            rotation_factor += 1.0;
        }

        if keyboard_input.pressed(KeyCode::Right) || keyboard_input.pressed(KeyCode::D) {
            rotation_factor -= 1.0;
        }

        if keyboard_input.pressed(KeyCode::Up) || keyboard_input.pressed(KeyCode::W) {

            let direction = (transform.rotation * Vec3::Y).normalize();
            let velocity = movable.velocity + direction * PLAYER_ACCELERATION;
            movable.velocity = if velocity.length() > PLAYER_MAX_SPEED { velocity.normalize() * PLAYER_MAX_SPEED } else { velocity };
        }

        movable.angular_velocity = rotation_factor * f32::to_radians(360.0);
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

pub fn update_score(score: Res<Score>) {
    if score.is_changed() {
        println!("Score: {}", score.value.to_string());
    }
}

pub fn initiate_asteroids_spawn(mut spawn_timer: ResMut<AsteroidSpawnTimer>, time: Res<Time>) {
    spawn_timer.timer.tick(time.delta());
}

pub fn spawn_asteroids(mut commands: Commands, window_query: Query<&Window, With<PrimaryWindow>>, asset_server: Res<AssetServer>, asteroids_timer: Res<AsteroidSpawnTimer>) {
    let window = window_query.get_single().unwrap();

    if asteroids_timer.timer.finished() {
        let random_x = random::<f32>() * window.width();
        let random_y = random::<f32>() * window.height();
        let direction_x = random::<f32>() * if random::<bool>() { 1.0 } else { -1.0 };
        let direction_y = random::<f32>() * if random::<bool>() { 1.0 } else { -1.0 };
        let speed = (random::<f32>() + 0.1) * 300.0;
        let velocity = Vec3::new(direction_x, direction_y, 0.0) * speed;
        let mut rng = rand::thread_rng();
        let angular_velocity = f32::to_radians(rng.gen_range(-50..50) as f32);

        commands.spawn(
            (
                SpriteBundle {
                    transform: Transform::from_xyz(random_x, random_y, 0.0),
                    texture: asset_server.load("sprites/asteroids/asteroid-big-01.png"),
                    ..default() 
                },
                Enemy {},
                Movable {
                     velocity: velocity,
                     angular_velocity: angular_velocity

                },
                OnScreenEntity {}
            )
        );
    }
}

pub fn exit_game(keyboard_input: Res<Input<KeyCode>>, mut app_exit_event_writer: EventWriter<AppExit> ) {
    if keyboard_input.just_pressed(KeyCode::Escape) {
        app_exit_event_writer.send(AppExit);
    }
}

pub fn handle_game_over(mut game_over_event_reader: EventReader<GameOver>) {
    for event in game_over_event_reader.iter() {
        print!("Game Over. Score: {}", event.score);
    }
}