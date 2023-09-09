use bevy::prelude::*;

#[derive(Component)]
pub struct Movable {
    pub velocity: Vec3,
    pub angular_velocity: f32
}

#[derive(Component)]
pub struct OnScreenEntity { }

#[derive(Component)]
pub struct PlayerShip {}

#[derive(Component)]
pub struct Enemy {}
