var is_thrust = 
	keyboard_check(vk_up) || 
	keyboard_check(ord("W")) ||
	gamepad_button_check(0, gp_shoulderrb);
	
var is_left = 
	keyboard_check(vk_left) || 
	keyboard_check(ord("A")) || 
	gamepad_button_check(0, gp_padl);
	
var is_right = 
	keyboard_check(vk_right) || 
	keyboard_check(ord("D")) ||
	gamepad_button_check(0, gp_padr);
	
var is_fire = 
	(keyboard_check_pressed(vk_space) || 
	gamepad_button_check_pressed(0, gp_face1)) &&
	can_fire;

if is_thrust
{	
	motion_add(image_angle, max_acceleration);	
	speed = speed > max_speed ? max_speed : speed;
}

if is_left
{
	image_angle += max_rotation;	
}


if is_right
{
	image_angle -= max_rotation;	
}

if is_fire
{
	// TODO: Thinks on moving "Instances" name to global constants
	var bullet = instance_create_layer(x, y, "Instances",  obj_laser, );
	
	bullet.image_angle = image_angle;
	bullet.direction = image_angle;
	can_fire = false;
	alarm[0] = 20;
}
