extends CharacterBody2D

const ROTATION_SPEED = 10.0
const MAX_SPEED = 500.0
const MAX_ACCELERATION = 15.0

func _process(delta: float) -> void:
    var turn =  Input.get_action_strength("player_turn_left") - Input.get_action_strength("player_turn_right")
    
    if turn > 0:
        rotation = rotation - ROTATION_SPEED * delta
    elif turn < 0:
        rotation = rotation + ROTATION_SPEED * delta
        
    if Input.is_action_pressed("player_accelerate"):
        var vel: Vector2 = velocity + to_direction(rotation) * MAX_ACCELERATION;
        velocity = vel.normalized() * MAX_SPEED if(vel.length() > MAX_SPEED) else vel
        
    move_and_slide()
        
func to_direction(angle: float) -> Vector2:
    return Vector2(sin(angle), -cos(angle))
