extends Node2D

@onready var objects = $Objects
@onready var view_size : Vector2 = get_viewport().get_visible_rect().size

func _ready() -> void:
    pass

func _process(delta: float) -> void:
    screen_wrap()
    
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
