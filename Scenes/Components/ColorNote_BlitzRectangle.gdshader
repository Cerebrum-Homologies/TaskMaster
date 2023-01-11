shader_type canvas_item;

void fragment() {
	vec4 texture_color = texture(TEXTURE, UV);
	bool band_change = ( ( int(UV.y * 512.0) / 8) % 2) == 1;
	if (band_change)
		//COLOR = vec4((UV.x * 0.35 + 0.35) + texture_color.r,
		//(UV.y * 0.15) + 0.275 + texture_color.g, 0.75 + texture_color.b, 0.225);
		COLOR = vec4((UV.x * 0.125) + texture_color.r,
		(UV.y * 0.125) + texture_color.g, (UV.x * 0.125) + texture_color.b, 0.825);
	else
		//COLOR = texture_color;
		//COLOR = vec4((UV.x * 0.35 + 0.175) + texture_color.r,
		//(UV.y * 0.425) + 0.175 + texture_color.g, 0.625 + texture_color.b, 0.175);
		COLOR = vec4((UV.x * 0.175) + texture_color.r,
		(UV.y * 0.175) + texture_color.g, (UV.x * 0.175) + texture_color.b, 0.725);
}
