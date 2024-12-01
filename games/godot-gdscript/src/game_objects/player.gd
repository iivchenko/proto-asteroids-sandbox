class_name Player
extends Area2D

signal destroyed(Player)

const ROTATION_SPEED = 10.0
const MAX_SPEED = 500.0
const MAX_ACCELERATION = 15.0
const FIRE_RATE = 0.75

@onready var visual: Sprite2D = $Visual
@onready var weapon: Marker2D = $Weapon
@onready var death_sound: AudioStreamPlayer = $DeathSound
@onready var death_particles: CPUParticles2D = $DeathParticles

var objects: Node2D
var velocity : Vector2
var can_fire: float = 0.0

var laser: PackedScene = preload("res://game_objects/laser.tscn")

func _ready() -> void:
    area_entered.connect(_on_collide)

func _process(delta: float) -> void:
    var turn = Input.get_action_strength("player_turn_left") - Input.get_action_strength("player_turn_right")
    
    if turn > 0: 
        rotation = rotation - ROTATION_SPEED * delta
    elif turn < 0:
        rotation = rotation + ROTATION_SPEED * delta
        
    if Input.is_action_pressed("player_accelerate"):
        var vel: Vector2 = velocity + Vector2.from_angle(rotation) * MAX_ACCELERATION;
        velocity = vel.normalized() * MAX_SPEED if(vel.length() > MAX_SPEED) else vel
        
    if Input.is_action_just_pressed("player_fire") and can_fire <= 0.0:
        can_fire = FIRE_RATE     
        var bullet = laser.instantiate()        
        bullet.global_position = weapon.global_position
        bullet.rotation = rotation
        bullet.direction = Vector2.from_angle(rotation)
        objects.add_child(bullet)
        
    if can_fire > 0.0:
        can_fire -= delta
        
    position = position + velocity * delta
    
func _on_collide(_body: Node2D) -> void:
    set_deferred("monitoring", false)
    set_deferred("monitorable", false)
    visual.visible = false

    death_sound.play()
    death_particles.emitting = true
    await death_sound.finished
    await death_particles.finished
    
    destroyed.emit(self)
    queue_free()
