// Part of the game loop. 
// Each minute reduces asteroids spawn time by 1 sec until it is alreay 1 sec
if (pause == false)
{	
	next_asteroid -= 60;
	next_ufo -= 60;

	if next_asteroid > 60
	{
		alarm[1] = next_time_decrease;
	}
	else
	{
		next_asteroid = 60;
		next_ufo = 5 * 60;
	}
}