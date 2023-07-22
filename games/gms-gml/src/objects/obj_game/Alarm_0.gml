// Part of game loop.
// Generates new asteroids

if (pause == false)
{	
	var h = window_get_height();
	var w = window_get_width();
	var _x = 0;
	var _y = 0;

	if irandom(1) = 0
	{
		_y = irandom(h);
	}
	else
	{
		_x = irandom(w);
	}

	instance_create_layer(_x, _y, "Instances", obj_enemy_asteroid);
	alarm[0] = next_asteroid;
}