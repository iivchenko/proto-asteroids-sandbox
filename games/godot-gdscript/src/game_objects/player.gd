class_name Player
extends Area2D

signal destroyed(Player)

const ROTATION_SPEED = 10.0
const MAX_SPEED = 500.0
const MAX_ACCELERATION = 15.0

@onready var visual: Sprite2D = $Visual
@onready var death_particles: CPUParticles2D = $DeathParticles

var velocity : Vector2

func _ready() -> void:
    area_entered.connect(_on_collide)

func _process(delta: float) -> void:
    var turn = Input.get_action_strength("player_turn_left") - Input.get_action_strength("player_turn_right")
    
    if turn > 0:
        rotation = rotation - ROTATION_SPEED * delta
    elif turn < 0:
        rotation = rotation + ROTATION_SPEED * delta
        
    if Input.is_action_pressed("player_accelerate"):
        var vel: Vector2 = velocity + Vector2.UP.rotated(rotation) * MAX_ACCELERATION;
        velocity = vel.normalized() * MAX_SPEED if(vel.length() > MAX_SPEED) else vel
        
    position = position + velocity * delta
    
func _on_collide(_body: Node2D) -> void:
    set_deferred("monitoring", false)
    set_deferred("monitorable", false)
    visual.visible = false

    death_particles.emitting = true
    await death_particles.finished
    
    destroyed.emit(self)
    queue_free()
