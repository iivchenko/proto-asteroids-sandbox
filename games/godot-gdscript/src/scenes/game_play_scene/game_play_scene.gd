extends Node2D

@onready var objects = $Objects
@onready var next_asteroid_timer = $NextAsteroidTimer
@onready var player_factory = preload("res://game_objects/player.tscn")
@onready var view_size : Vector2 = get_viewport().get_visible_rect().size
@onready var asteroid_factory = AsteroidsFactory.new()

func _ready() -> void:
    create_player()
    next_asteroid_timer.timeout.connect(_on_next_asteroid)

func _process(_delta: float) -> void:
    screen_wrap()

func create_player() -> void:
    var player = player_factory.instantiate()
    player.position = Vector2(view_size.x / 2.0, view_size.y / 2.0)    
    player.destroyed.connect(_on_player_destroyed)
    objects.add_child(player)
    
func screen_wrap() -> void:
    for object: Node2D in objects.get_children():
        var obj_size = object.get_node("Visual").texture.get_size()
        
        if object.position.x + obj_size.x / 2.0 < 0:
            object.position.x = view_size.x + obj_size.x / 2.0
        elif object.position.x - obj_size.x / 2.0 > view_size.x:
            object.position.x = -obj_size.x / 2.0

        if object.position.y + obj_size.y / 2.0 < 0:
            object.position.y = view_size.y + obj_size.y / 2.0
        elif object.position.y - obj_size.y / 2.0 > view_size.y:
            object.position.y = -obj_size.y / 2.0

func _on_next_asteroid() -> void:
    var asteroid = asteroid_factory.create()

    match randi_range(0, 4):
        0: #UP
            asteroid.position = Vector2(randi_range(0, int(view_size.x)), 0)
        1: #RIGHT
            asteroid.position = Vector2(view_size.x, randi_range(0, int(view_size.y)))
        2: #DOWN
            asteroid.position = Vector2(randi_range(0, int(view_size.x)), int(view_size.y))
        3: # LEFT
            asteroid.position = Vector2(0, randi_range(0, int(view_size.y)))

    objects.add_child(asteroid)

func _on_player_destroyed(player: Player) -> void:
    player.destroyed.disconnect(_on_player_destroyed)
    await player.tree_exited
    create_player()
