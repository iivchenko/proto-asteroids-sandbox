use bevy::{ prelude::*, window::PrimaryWindow };
use rand::prelude::*;

use crate::AppState;

pub struct SkyPlugin;

impl Plugin for SkyPlugin {
    fn build (&self, app: &mut App) {
        app
            .add_systems(OnEnter(AppState::MainMenu), setup_sky_system)
            .add_systems(Update, update_sky_system.run_if(in_state(AppState::MainMenu)));
    }
}

#[derive(Component)]
struct Star { initial_color: Color, speed: f32 }

fn setup_sky_system(window_query: Query<&Window, With<PrimaryWindow>>, mut commands: Commands, asset_server: Res<AssetServer>) {
    let window = window_query.get_single().unwrap();
    let width = window.width();
    let height = window.height();
    let block = 96.0;

    let mut rnd: ThreadRng = thread_rng();

    let sprites = [
        "sprites/stars/star-01.png", 
        "sprites/stars/star-02.png",
        "sprites/stars/star-03.png",
        "sprites/stars/star-04.png",
        "sprites/stars/star-05.png"
    ];

    for x  in 0..(width/block) as i32 {
        for y  in 0..(height/block) as i32 {

            if rand::random() {
                continue;
            }
            
            let scale = (rand::random::<f32>() * 0.55 + 0.3) * 2.0;
            let sprite: &&str = sprites.choose(&mut rnd).unwrap();
            let pos_x = rand::random::<f32>() * block + (x as f32) * block; 
            let pos_y = rand::random::<f32>() * block + (y as f32) * block; 
            let rotation = rand::random::<f32>() * 360.0;
            let color = Color::rgb(rand::random::<f32>(), rand::random::<f32>(), rand::random::<f32>());
            let speed = rand::random::<f32>() * 0.65 + 0.1;

            commands.spawn(
                (SpriteBundle {
                    sprite: Sprite {
                        color: color,
                        ..default()
                    },
                    texture: asset_server.load(*sprite), 
                    transform: Transform { 
                        translation: Vec3 { x: pos_x, y: pos_y, z: 0.0 }, 
                        rotation: Quat::from_rotation_z(rotation), 
                        scale: Vec3 { x: scale, y: scale, z: 0.0 }  },
                    ..default()
                },
                Star { 
                    initial_color: color,
                    speed: speed
                })
            );
        }
    }
} 

fn update_sky_system(mut stars_query: Query<(&mut Sprite, &Star), With<Star>>, time: Res<Time>) {
    for (mut sprite, star) in stars_query.iter_mut() {
        sprite.color = star.initial_color * ((time.elapsed_seconds() + star.speed * 5000.0).sin() * 0.75).abs();
    }
}
