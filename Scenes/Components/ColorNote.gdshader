shader_type canvas_item;

void fragment() {
	COLOR = vec4(UV.x * 0.35, (UV.y * 0.15) + 0.275, 0.75, 0.25);
}
