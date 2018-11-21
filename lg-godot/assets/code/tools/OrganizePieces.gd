tool
extends EditorScript

func _run():
	var children = get_scene().get_children()
	for i in range(0, children.size()):
		children[i].translation = Vector3(
			i % 10,
			0,
			floor(i / 10)
		)