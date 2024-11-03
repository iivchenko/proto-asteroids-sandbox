class_name AsteroidsFactory

var asteroid_big = preload("res://game_objects/asteroid_big.tscn")
var asteroid_medium = preload("res://game_objects/asteroid_medium.tscn")
var asteroid_small = preload("res://game_objects/asteroid_small.tscn")
var asteroid_tiny = preload("res://game_objects/asteroid_tiny.tscn")

func create () -> Node2D:
    
    var asteroid : Node2D
    
    match randi_range(0, 3):
        0: #BIG
            asteroid = asteroid_big.instantiate()
        1: #MEDIUM
            asteroid = asteroid_medium.instantiate()
        2: #SMALL
            asteroid = asteroid_small.instantiate()
        3: #TINY
            asteroid = asteroid_tiny.instantiate()           
            
    return asteroid
