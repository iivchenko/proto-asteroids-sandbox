draw_self();

draw_set_font(fnt_score);

//draw_set_halign(fa_center);
draw_set_valign(fa_middle);

draw_text(x, y, $"  X {global.score}");

//draw_set_halign(fa_left);
draw_set_valign(fa_top);