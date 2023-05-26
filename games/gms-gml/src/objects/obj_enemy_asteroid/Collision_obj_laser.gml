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

effect_create_above(ef_explosion, x, y, 1, c_white);
audio_play_sound(snd_asteroid_explosion, 100, false);
instance_destroy();