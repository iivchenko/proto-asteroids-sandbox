var block = 96;
for (var ix = 0; ix < room_width / block; ix += 1)
{
    for (var iy = 0; iy < room_height / block; iy += 1)
    {		
        if (irandom(1) = 0)
		{		
            continue;
		}
		
        var scale = irandom_range(30, 85) / 100.0;
		
		var star = instance_create_layer(ix, iy, "Stars", obj_star);
		
		with(star)
		{	
			x = irandom(block) + ix * block;
			y = irandom(block) + iy * block;
			image_angle = irandom(360);
			image_xscale = scale;
			image_yscale = scale;
		}

    }
}
