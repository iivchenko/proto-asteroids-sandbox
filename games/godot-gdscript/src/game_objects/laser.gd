class_name Laser
extends Area2D

@export var speed : int = 750

var velocity : Vector2
var direction : Vector2

func _ready() -> void:
    
    body_entered.connect(on_collide)
    area_entered.connect(on_collide)
    
    velocity = direction * speed
        
func _process(delta: float) -> void:
    position = position + velocity * delta
    
func on_collide(_body: Node2D) -> void:    
    body_entered.disconnect(on_collide)
    queue_free()
