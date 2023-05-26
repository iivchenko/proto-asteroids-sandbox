var sprites = tag_get_asset_ids(["asteroid"], asset_sprite);
sprite_index = sprites[irandom(array_length(sprites) - 1)];

speed = max_speed;
direction = random(360);
image_angle = random(360);