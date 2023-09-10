use bevy::prelude::*;

#[derive(Component)]
pub struct Movable {
    pub velocity: Vec3,
    pub angular_velocity: f32
}

#[derive(Component)]
pub struct OnScreenEntity { }


#[derive(Component)]
pub struct OutScreenEntity { }

pub enum EntityDescriptorType {
    PlayerShip,
    PlayerBullet,
    Asteroid,
}

#[derive(Component)]
pub struct EntityDescriptor { pub entity_type: EntityDescriptorType }

#[derive(Component)]
pub struct PlayerShip { pub fire_delay: f32} // TODO: Think on moving fire delay to a different place

#[derive(Component)]
pub struct PlayerBullet {}

#[derive(Component)]
pub struct Enemy {}
