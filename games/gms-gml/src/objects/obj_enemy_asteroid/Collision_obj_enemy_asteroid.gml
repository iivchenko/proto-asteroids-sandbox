var sys = part_system_create(ps_asteroid_explosion);
part_system_position(sys, x, y);
audio_play_sound(snd_asteroid_explosion, 100, false);
instance_destroy();