effect_create_above(ef_explosion, x, y, 1, c_white);

instance_destroy();

var dead = instance_create_layer(x, y, "Instances", obj_player_dead);

dead.sprite = sprite_index;