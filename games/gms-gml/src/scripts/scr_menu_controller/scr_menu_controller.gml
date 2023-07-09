function menu_up(){
	var buttons;
	var selected_index = -1;


	for (var i = 0; i < instance_number(obj_button_parent); ++i;)
	{
	    buttons[i] = instance_find(obj_button_parent, i);
	
		if (buttons[i].is_selected)
		{
			selected_index = i;
			break;
		}
	}

	var next_to_select = selected_index - 1;

	if (next_to_select <> -1)
	{
		if (selected_index <> -1)
		{		
			with (instance_find(obj_button_parent, selected_index))
			{
				is_selected = false;
				select_button();
			}		
		}
	
		with(instance_find(obj_button_parent, next_to_select))
		{		
			is_selected = true;
			select_button();
		}
	}
}

function menu_down(){
	var buttons;
	var selected_index = -1;


	for (var i = 0; i < instance_number(obj_button_parent); ++i;)
	{
	    buttons[i] = instance_find(obj_button_parent, i);
	
		if (buttons[i].is_selected)
		{
			selected_index = i;
			break;
		}
	}

	var next_to_select = selected_index + 1;

	if (next_to_select <> -1 &&  next_to_select < instance_number(obj_button_parent))
	{
		if (selected_index <> -1)
		{		
			with (instance_find(obj_button_parent, selected_index))
			{
				is_selected = false;
				select_button();
			}		
		}
	
		with(instance_find(obj_button_parent, next_to_select))
		{		
			is_selected = true;
			select_button();
		}
	}
}

function menu_select(){
	for (var i = 0; i < instance_number(obj_button_parent); ++i;)
	{
		buttons[i] = instance_find(obj_button_parent, i);
	
		if (buttons[i].is_selected)
		{
			with(buttons[i])
			{
				action_button();
			}
		}
	}
}