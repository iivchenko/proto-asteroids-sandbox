obj_game.pause = false;
	
var _elements = layer_get_all_elements("Menu");
for (var _i = 0; _i < array_length(_elements); _i++;)
{
	layer_sequence_destroy(_elements[_i]);
}

instance_activate_all();	

