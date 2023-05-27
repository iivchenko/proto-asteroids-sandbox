/// @description Resurect player

instance_destroy();

var player = instance_create_layer(room_width / 2, room_height / 2, "Instances", obj_player);

player.sprite_index  = tmp;