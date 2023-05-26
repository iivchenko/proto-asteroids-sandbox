// Script assets have changed for v2.3.0 see
// https://help.yoyogames.com/hc/en-us/articles/360005277377 for more information

// TODO: Finish with macroses

#macro debug_info false
#macro Debug:debug_info true


function show_debug_info()
{
	if debug_info
	{
		draw_text(10, 10, $"Hext asteroid: {next_asteroid}/{alarm[0]}");
		draw_text(10, 30, $"Next decrease: {next_time_decrease}/{alarm[1]}");
	}
}