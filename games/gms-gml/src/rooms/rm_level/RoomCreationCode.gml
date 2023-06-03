audio_stop_all();

var song = choose(snd_game1_song, snd_game2_song, snd_game3_song, snd_game4_song, snd_game5_song, snd_game6_song, snd_game7_song, snd_game8_song, snd_game9_song);

audio_play_sound(song, 100, true);


// Generate first asteroids

for(var i = 0; i < irandom(15); i++)
{
	instance_create_layer(irandom(room_width), irandom(room_height), "Instances", obj_enemy_asteroid);
}