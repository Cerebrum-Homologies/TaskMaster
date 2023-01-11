using Godot;
using System;
//using Svg;

namespace TaskMaster
{
	public partial class SpriteTexture : Sprite2D
	{
		private ImageTexture workArtTexture;
		private BitMap storeBitmap;
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			workArtTexture = new ImageTexture();
		}

		public void UpdateTextureImage(String imageResource)
		{
			if (workArtTexture != null)
			{
				String docsPath = OS.GetSystemDir(OS.SystemDir.Documents);
				Image textureImage = GD.Load<Image>(imageResource);
				if (textureImage != null)
				{
					workArtTexture.SetImage(textureImage);
				}
			}
		}

		public Image CreateNoiseImage(int width, int height, String imagePath, Vector3 offset,
		FastNoiseLite.NoiseTypeEnum noiseType = FastNoiseLite.NoiseTypeEnum.SimplexSmooth, FastNoiseLite.FractalTypeEnum fractalType = FastNoiseLite.FractalTypeEnum.Fbm,
		float freq = 0.035f, float lacunarity = 2.0f, int noiseSeed = 0)
		{
			Image noiseDrawImage = null;
			if ((width >= 8) && (height >= 8))
			{
				noiseDrawImage = Image.Create(width, height, false, Image.Format.Rgba8);
				FastNoiseLite noise = new FastNoiseLite();
				noise.NoiseType = noiseType;
				noise.FractalGain = 0.5f;
				noise.Offset = offset;
				noise.FractalType = fractalType;//FastNoiseLite.FractalTypeEnum.Fbm;
				noise.Frequency = freq; /*0.035f;*/
				noise.FractalLacunarity = lacunarity;
				if (noiseType == FastNoiseLite.NoiseTypeEnum.Cellular) {
					noise.CellularDistanceFunction = FastNoiseLite.CellularDistanceFunctionEnum.Euclidean;
					noise.CellularReturnType = FastNoiseLite.CellularReturnTypeEnum.Distance2;
					noise.CellularJitter = 1.0f;
				}
				if (noiseSeed != 0)
					noise.Seed = noiseSeed;
				else
					noise.Seed = (int)GD.Randi();
				float[] noiseData = new float[width * height];
				int index = 0;
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						noiseData[index++] = noise.GetNoise2d(x, y);
					}
				}
				for (int pixelIndex = 0; pixelIndex < width * height; pixelIndex++)
				{
					Color pixelColour;
					if (noiseData[pixelIndex] >= 0.0175f)
						pixelColour = new Color(0.5f * noiseData[pixelIndex], 0.625f * noiseData[pixelIndex], 0.75f * noiseData[pixelIndex]);
					else
						pixelColour = new Color(0.0f, 0.0f, 0.0f);
					noiseDrawImage.SetPixel(pixelIndex % width, pixelIndex / width, pixelColour);
				}
				noiseDrawImage.SavePng(imagePath);
			}
			return noiseDrawImage;
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}
	}
}
