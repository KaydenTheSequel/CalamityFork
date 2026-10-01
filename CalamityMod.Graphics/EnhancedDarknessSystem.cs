using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Light;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Graphics;

public class EnhancedDarknessSystem : ModSystem
{
	public class LightSource
	{
		public Texture2D texture;

		public float scale;

		public Vector2 vectorScale;

		public Vector2 center;

		public float rotation;

		public float opacity;

		public Color color;

		public int lifetime;

		public Rectangle? frame;

		public LightSource()
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			texture = _defaultTexture.Value;
			scale = 1f;
			vectorScale = Vector2.One;
			center = Main.LocalPlayer.Center;
			opacity = 1f;
			color = Color.White;
			lifetime = 1;
			base._002Ector();
		}

		public LightSource(Vector2? center = null, Texture2D texture = null, float scale = 1f, float rotation = 0f, Vector2? vectorScale = null, float opacity = 1f)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0091: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			//IL_0096: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			this.texture = _defaultTexture.Value;
			this.scale = 1f;
			this.vectorScale = Vector2.One;
			this.center = Main.LocalPlayer.Center;
			this.opacity = 1f;
			color = Color.White;
			lifetime = 1;
			base._002Ector();
			this.texture = texture ?? ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			this.scale = scale;
			this.vectorScale = (Vector2)(((_003F?)vectorScale) ?? Vector2.One);
			this.center = (Vector2)(((_003F?)center) ?? Main.LocalPlayer.Center);
			this.rotation = rotation;
			this.opacity = opacity;
		}
	}

	private static Asset<Texture2D> _defaultTexture;

	public static List<LightSource> lights = new List<LightSource>();

	private const float VanillaWaterLightMult = 0.91f;

	public override void Load()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		On_OverlayManager.Draw += new hook_Draw(DrawShadowOverlay);
		On_LightingEngine.UpdateLightDecay += new hook_UpdateLightDecay(AdjustTransmissiveness);
		_defaultTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
	}

	private void DrawShadowOverlay(orig_Draw orig, OverlayManager self, SpriteBatch spriteBatch, RenderLayers layer, bool beginSpriteBatch)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(self, spriteBatch, layer, beginSpriteBatch);
		if (Main.gameMenu || layer != RenderLayers.All || Main.LocalPlayer.Calamity().darknessIntensity <= 0f)
		{
			return;
		}
		CalamityPlayer mp = Main.LocalPlayer.Calamity();
		GraphicsDevice device = ((Game)Main.instance).GraphicsDevice;
		using RenderTargetLease lease = RenderTargetPool.Shared.Rent(device, Main.screenWidth, Main.screenHeight, RenderTargetDescriptor.Default);
		using (lease.Scope(preserveContents: true, Color.Black))
		{
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.Default, Main.Rasterizer, (Effect)null, Matrix.Identity);
			foreach (LightSource item in lights)
			{
				SpriteBatch spriteBatch2 = Main.spriteBatch;
				Texture2D texture = item.texture;
				Vector2 val = item.center - Main.screenPosition;
				Rectangle? frame = item.frame;
				Color val2 = item.color * item.opacity;
				float rotation = item.rotation;
				Rectangle? frame2 = item.frame;
				spriteBatch2.Draw(texture, val, frame, val2, rotation, (!frame2.HasValue) ? (item.texture.Size() * 0.5f) : item.frame.Value.Size(), item.vectorScale * item.scale, (SpriteEffects)0, 0f);
			}
			Main.spriteBatch.End();
		}
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			MiscShaderData miscShaderData = GameShaders.Misc["CalamityMod:DozeLightingShader"];
			miscShaderData.UseOpacity(mp.darknessIntensity);
			miscShaderData.Apply();
			Main.spriteBatch.Draw((Texture2D)(object)lease.Target, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			spriteBatch.End();
		}
	}

	private void AdjustTransmissiveness(orig_UpdateLightDecay orig, LightingEngine self)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		orig.Invoke(self);
		CalamityPlayer mp = Main.LocalPlayer.Calamity();
		LightMap map = self._workingLightMap;
		if (mp.ZoneAbyss)
		{
			map.LightDecayThroughWater = Vector3.Lerp(map.LightDecayThroughWater, map.LightDecayThroughWater / 0.91f * 0.95f, MathHelper.Clamp(mp.darknessIntensity, 0f, 1f));
		}
	}

	public override void OnWorldUnload()
	{
		lights.Clear();
	}

	public override void PreUpdateEntities()
	{
		for (int i = 0; i < lights.Count; i++)
		{
			LightSource item = lights[i];
			item.lifetime--;
			if (item.lifetime <= 0)
			{
				lights.Remove(item);
				i--;
			}
		}
	}
}
