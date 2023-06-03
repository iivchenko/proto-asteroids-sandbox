var sys = part_system_create(ps_asteroid_explosion);
part_system_position(sys, x, y);

switch(sprite_index)
{
	case spr_asteroid_tiny_01: 
		global.score += 4;
		break;
	case spr_asteroid_small_01: 
		global.score += 3;
		break;
		
	case spr_asteroid_medium_01: 
		global.score += 2;
		break;
		
	case spr_asteroid_big_01: 
		global.score += 1;
		break;
}



audio_play_sound(snd_asteroid_explosion, 100, false);
instance_destroy();