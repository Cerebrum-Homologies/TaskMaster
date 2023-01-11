shader_type canvas_item;

void fragment() {
	bool band_change = ( ( int(UV.y * 512.0) / 8) % 2) == 1;
	if (band_change)
		COLOR = vec4(UV.x * 0.35 + 0.45, (UV.y * 0.15) + 0.275, 0.75, 0.115);
	else
		COLOR = vec4(UV.x * 0.35 + 0.55, (UV.y * 0.425) + 0.275, 0.625, 0.0875);
}
