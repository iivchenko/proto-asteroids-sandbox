class_name Star
extends Sprite2D


var star1_visual: CompressedTexture2D = preload("res://assets/sprites/stars/star-01.png")
var star2_visual: CompressedTexture2D = preload("res://assets/sprites/stars/star-02.png")
var star3_visual: CompressedTexture2D = preload("res://assets/sprites/stars/star-03.png")
var star4_visual: CompressedTexture2D = preload("res://assets/sprites/stars/star-04.png")
var star5_visual: CompressedTexture2D = preload("res://assets/sprites/stars/star-05.png")

var time: float = 0.0
var speed: float = 0.0
var color: Color

func _ready() -> void:
    match randi_range(1, 5):
        1:        
            texture = star1_visual
        2:        
            texture = star2_visual
        3:        
            texture = star3_visual
        4:        
            texture = star4_visual
        5:        
            texture = star5_visual
    
    color = Color(randf(), randf(), randf())
    time = randi_range(0, 360)
    speed = randf()
    
    modulate = color

func _process(delta: float) -> void:
    var modulation = abs(sin(time))
    
    modulate = Color(
            color.r * modulation,
            color.g * modulation,
            color.b * modulation)

    time += speed * delta
