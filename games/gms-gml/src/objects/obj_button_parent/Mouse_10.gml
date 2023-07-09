for (var i = 0; i < instance_number(obj_button_parent); ++i;)
{
	with(instance_find(obj_button_parent, i))
	{
		is_selected = false;		
		select_button();
	}	
}


is_selected = true;
select_button();

