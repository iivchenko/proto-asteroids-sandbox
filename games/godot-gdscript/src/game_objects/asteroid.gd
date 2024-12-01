class_name Asteroid
extends Area2D

@export var min_speed : int = 0
@export var max_speed : int = 0
@export var min_rotation : int = 0
@export var max_rotation : int = 0

@onready var visual: Sprite2D = $Visual
@onready var death_sound : AudioStreamPlayer = $DeathSound
@onready var death_particles: CPUParticles2D = $DeathParticles

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
    
func on_collide(_body: Node2D) -> void:  
    
    set_deferred("monitoring", false)
    set_deferred("monitorable", false)
    visual.visible = false
    

    death_sound.play()
    death_particles.emitting = true
    await death_sound.finished
    await death_particles.finished
    
    queue_free()
