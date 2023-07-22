pause = false;

next_asteroid = 5 * 60;
next_time_decrease = 1 * 60 * 60;
next_ufo = 1 * 60 * 60;

alarm[0] = next_asteroid;
alarm[1] = next_time_decrease
alarm[2] = next_ufo;

layer_sequence_create("Transition", 0, 0, seq_transition_end);