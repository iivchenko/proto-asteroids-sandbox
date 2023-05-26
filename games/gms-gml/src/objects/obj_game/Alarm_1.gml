// Part of the game loop. 
// Each minute reduces asteroids spawn time by 1 sec until it is alreay 1 sec
next_asteroid -= 60;

if next_asteroid > 60
{
	alarm[1] = next_time_decrease;
}
else
{
	next_asteroid = 60;
}