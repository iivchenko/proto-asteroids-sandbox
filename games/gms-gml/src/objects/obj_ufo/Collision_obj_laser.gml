var sys = part_system_create(ps_player_ship_explosion);
part_system_position(sys, x, y);

global.score += 10;

instance_destroy();