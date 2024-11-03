class_name Asteroid
extends Area2D

@export var min_speed : int = 0
@export var max_speed : int = 0
@export var min_rotation : int = 0
@export var max_rotation : int = 0

var rotation_speed : float
var velocity : Vector2

func _ready() -> void:
    
    body_entered.connect(on_collide)
    area_entered.connect(on_collide)
    
    rotation_speed = deg_to_rad(randi_range(min_rotation, max_rotation))
    velocity = Vector2(
        randi_range(min_speed, max_speed) * (1 if (randf() > 0.5) else -1),
        randi_range(min_speed, max_speed) * (1 if (randf() > 0.5) else -1)
    )
        
func _process(delta: float) -> void:
    rotation = rotation + rotation_speed * delta
    position = position + velocity * delta
    
func on_collide(body: Node2D) -> void:
    
    if typeof(body) == typeof(Player) or typeof(body) == typeof(Asteroid):
        body_entered.disconnect(on_collide)
        queue_free()
