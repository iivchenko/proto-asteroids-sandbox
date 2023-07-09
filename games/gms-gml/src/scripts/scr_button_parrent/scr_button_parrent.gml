// Script assets have changed for v2.3.0 see
// https://help.yoyogames.com/hc/en-us/articles/360005277377 for more information
function select_button(){

	if (is_selected)
	{
		image_alpha = 0.6;
	}
	else
	{
		image_alpha = 1.0;
		if (mouse_check_button(mb_left))
		{
			y = y_initial;
		}
	}	
}

function press_button(){
	if (is_selected)
	{		
		y_initial = y;
		y = y_initial + 1;
	}
	else
	{
		y = y_initial;
	}
}

function action_button(){
	event_user(0);
}