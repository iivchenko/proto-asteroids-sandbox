function player_destroy()
{
	var _sys = part_system_create(ps_player_ship_explosion);
	part_system_position(_sys, x, y);

	instance_destroy();	
	
	global.life -= 1;
	
	if (global.life == 0)
	{
		layer_sequence_create("Menu", room_width / 2, room_height / 2, seq_game_over);
		instance_create_layer(0, 0, "Instances", obj_menu_controller);
	}
	else
	{
		var _dead = instance_create_layer(x, y, "Instances", obj_player_dead);

		_dead.sprite = sprite_index;
	}
}