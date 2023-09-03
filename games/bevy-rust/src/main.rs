use bevy::{prelude::*, window::PrimaryWindow, audio::* };
use rand::prelude::*;

pub const PLAYER_ACCELERATION: f32 = 50.0;
pub const PLAYER_MAX_SPEED: f32 = 600.0;
pub const PLAYER_SIZE: f32 = 32.0;

fn main() {
    App::new()
        .add_plugins(DefaultPlugins)
        .init_resource::<Score>()
        .init_resource::<AsteroidSpawnTimer>()
        .add_systems(Startup, setup)
        .add_systems(Update, entity_movement)
        .add_systems(Update, player_control)
        .add_systems(Update, wrap_screen)
        .add_systems(Update, entity_collision_system)
        .add_systems(Update, update_score)
        .add_systems(Update, initiate_asteroids_spawn)
        .add_systems(Update, spawn_asteroids)
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

#[derive(Resource)]
pub struct Score {
    pub value: u32
}

impl Default for Score {
    fn default() -> Score {
        Score { value: 0 }
    }
} 

#[derive(Resource)]
pub struct AsteroidSpawnTimer { 
    pub timer: Timer
}

impl Default for AsteroidSpawnTimer {
    fn default() -> AsteroidSpawnTimer {
        AsteroidSpawnTimer { timer: Timer::from_seconds(5.0, TimerMode::Repeating) }
    }
} 

fn setup(mut commands: Commands, asset_server: Res<AssetServer>, window_query: Query<&Window, With<PrimaryWindow>>) {
    let window = window_query.get_single().unwrap();

    commands.spawn(Camera2dBundle {
        transform: Transform::from_xyz(window.width() / 2.0, window.height() / 2.0, 0.0),
        ..default()
    });

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

pub fn player_control (keyboard_input: Res<Input<KeyCode>>, mut player_query: Query<(&mut Transform, &mut Movable), With<PlayerShip>>, timer: Res<Time>) {
    if let Ok((mut transform, mut movable)) = player_query.get_single_mut(){

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

        transform.rotate_z(rotation_factor * f32::to_radians(360.0) * timer.delta_seconds());
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