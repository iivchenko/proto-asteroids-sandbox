next_asteroid -= 60;

if next_asteroid > 60
{
	alarm[1] = next_time_decrease;
}
else
{
	next_asteroid = 60;
}