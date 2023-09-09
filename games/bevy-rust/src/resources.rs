use bevy::prelude::*;

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