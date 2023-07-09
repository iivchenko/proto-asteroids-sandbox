if(instance_number(obj_player) > 0)
{
    var _player = instance_find(obj_player, 0);	
	var _bullet = instance_create_layer(x, y, "Instances",  obj_blaster);
	
	_bullet.direction = point_direction(x, y, _player.x, _player.y);
	_bullet.image_angle = point_direction(x, y, _player.x, _player.y);
}

alarm[0] = 5 * 60;