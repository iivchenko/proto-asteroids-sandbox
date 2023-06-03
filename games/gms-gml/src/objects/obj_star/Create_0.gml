color = make_color_rgb(irandom(255), irandom(255), irandom(255));
counter = random(1);
delta = 0.0000005;
var sprites = tag_get_asset_ids(["star"], asset_sprite);

sprite_index = sprites[irandom(array_length(sprites) - 1)];
image_blend = color;