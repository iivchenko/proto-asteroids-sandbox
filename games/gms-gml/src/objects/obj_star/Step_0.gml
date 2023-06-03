counter += delta;
if counter >=  1 
{
	delta *= -1;
	couter = 1;
}

if counter <= 0
{
	delta *= -1;
	couter = 0;
}

image_blend = color * sin(counter);
