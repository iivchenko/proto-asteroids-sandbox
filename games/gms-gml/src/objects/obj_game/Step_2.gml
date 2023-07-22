var _is_pause_pressed = 
	keyboard_check_pressed(vk_escape) || 
	gamepad_button_check_pressed(0, gp_start);
	

if (_is_pause_pressed == true && pause == true)
{
	pause = false;
	
	var _elements = layer_get_all_elements("Menu");
	for (var _i = 0; _i < array_length(_elements); _i++;)
	{
		layer_sequence_destroy(_elements[_i]);
	}
	
	instance_destroy(obj_menu_controller);
	instance_activate_all();	
}
else if (_is_pause_pressed == true)
{
	pause = true;
	
	instance_deactivate_all(true);
	layer_sequence_create("Menu", room_width / 2 , room_height / 2, seq_pause);	
	instance_create_layer(0, 0, "Instances", obj_menu_controller);	
}

