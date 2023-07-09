var sys = part_system_create(ps_player_ship_explosion);
part_system_position(sys, x, y);

instance_destroy();

var dead = instance_create_layer(x, y, "Instances", obj_player_dead);

dead.sprite = sprite_index;

