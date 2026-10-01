using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.MainMenu;

public class CalamityMainMenu : ModMenu
{
	public class Cinder
	{
		public int Time;

		public int Lifetime;

		public int IdentityIndex;

		public float Scale;

		public float Depth;

		public Color DrawColor;

		public Vector2 Velocity;

		public Vector2 Center;

		public Cinder(int lifetime, int identity, float depth, Color color, Vector2 startingPosition, Vector2 startingVelocity)
		{
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Lifetime = lifetime;
			IdentityIndex = identity;
			Depth = depth;
			DrawColor = color;
			Center = startingPosition;
			Velocity = startingVelocity;
		}
	}

	public float remixLogoRotation;

	public static List<Cinder> Cinders { get; internal set; } = new List<Cinder>();

	public override string DisplayName => CalamityUtils.GetTextValue("UI.MainMenu");

	public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("CalamityMod/MainMenu/Logo", (AssetRequestMode)2);

	public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("CalamityMod/Backgrounds/BlankPixel", (AssetRequestMode)2);

	public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("CalamityMod/Backgrounds/BlankPixel", (AssetRequestMode)2);

	public override int Music => CalamityMod.Instance.GetMusicFromMusicMod("CalamityTitle") ?? 6;

	public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullSurfaceBackground>();

	public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/MainMenu/ModernMenuBackground", (AssetRequestMode)2).Value;
		Vector2 drawOffset = Vector2.Zero;
		float xScale = (float)Main.screenWidth / (float)texture.Width;
		float yScale = (float)Main.screenHeight / (float)texture.Height;
		float scale = xScale;
		if (xScale != yScale)
		{
			if (yScale > xScale)
			{
				scale = yScale;
				drawOffset.X -= ((float)texture.Width * scale - (float)Main.screenWidth) * 0.5f;
			}
			else
			{
				drawOffset.Y -= ((float)texture.Height * scale - (float)Main.screenHeight) * 0.5f;
			}
		}
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(texture, drawOffset, (Rectangle?)null, Color.White, 0f, Vector2.Zero, scale, (SpriteEffects)0, 0f);
		Vector2 startingPosition = default(Vector2);
		for (int i = 0; i < 5; i++)
		{
			if (Main.rand.NextBool(4))
			{
				int lifetime = Main.rand.Next(200, 300);
				float depth = Main.rand.NextFloat(1.8f, 5f);
				((Vector2)(ref startingPosition))._002Ector((float)Main.screenWidth * Main.rand.NextFloat(-0.1f, 1.1f), (float)Main.screenHeight * 1.05f);
				Vector2 startingVelocity = -Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-0.9f, 0.9f)) * 4f;
				Color cinderColor = selectCinderColor();
				Cinders.Add(new Cinder(lifetime, Cinders.Count, depth, cinderColor, startingPosition, startingVelocity));
			}
		}
		for (int j = 0; j < Cinders.Count; j++)
		{
			Cinders[j].Scale = Utils.GetLerpValue(Cinders[j].Lifetime, Cinders[j].Lifetime / 3, Cinders[j].Time, clamped: true);
			Cinders[j].Scale *= MathHelper.Lerp(0.6f, 0.9f, (float)Cinders[j].IdentityIndex % 6f / 6f);
			if (Cinders[j].IdentityIndex % 13 == 12)
			{
				Cinders[j].Scale *= 2f;
			}
			float flySpeed = MathHelper.Lerp(3.2f, 14f, (float)Cinders[j].IdentityIndex % 21f / 21f);
			Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-0.44f, 0.44f, (float)Math.Sin((float)Cinders[j].Time / 16f + (float)Cinders[j].IdentityIndex) * 0.5f + 0.5f));
			idealVelocity = (idealVelocity + Vector2.UnitX).SafeNormalize(Vector2.UnitY) * flySpeed;
			float movementInterpolant = MathHelper.Lerp(0.01f, 0.08f, Utils.GetLerpValue(45f, 145f, Cinders[j].Time, clamped: true));
			Cinders[j].Velocity = Vector2.Lerp(Cinders[j].Velocity, idealVelocity, movementInterpolant);
			Cinders[j].Time++;
			Cinder cinder = Cinders[j];
			cinder.Center += Cinders[j].Velocity;
		}
		Cinders.RemoveAll((Cinder c) => c.Time >= c.Lifetime);
		Texture2D cinderTexture = ModContent.Request<Texture2D>("CalamityMod/Skies/CalamitasCinder", (AssetRequestMode)2).Value;
		for (int i2 = 0; i2 < Cinders.Count; i2++)
		{
			Vector2 drawPosition = Cinders[i2].Center;
			spriteBatch.Draw(cinderTexture, drawPosition, (Rectangle?)null, Cinders[i2].DrawColor, 0f, cinderTexture.Size() * 0.5f, Cinders[i2].Scale, (SpriteEffects)0, 0f);
		}
		drawColor = Color.White;
		Main.time = 27000.0;
		Main.dayTime = true;
		if (WorldGen.remixWorldGen)
		{
			remixLogoRotation += (float)Math.PI / 50f;
			if (remixLogoRotation >= (float)Math.PI && !WorldGen.everythingWorldGen)
			{
				remixLogoRotation = (float)Math.PI;
			}
		}
		else
		{
			remixLogoRotation = 0f;
		}
		float rotationSecretSeedAdjusted = (WorldGen.remixWorldGen ? remixLogoRotation : (WorldGen.drunkWorldGen ? logoRotation : 0f));
		Vector2 drawPos = default(Vector2);
		((Vector2)(ref drawPos))._002Ector((float)Main.screenWidth / 2f, 100f);
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(Logo.Value, drawPos, (Rectangle?)null, drawColor, rotationSecretSeedAdjusted, Logo.Value.Size() * 0.5f, WorldGen.drunkWorldGen ? logoScale : 1f, (SpriteEffects)0, 0f);
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.UIScaleMatrix);
		return false;
		static Color selectCinderColor()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			return Color.Lerp(Color.Lerp(Color.Crimson, Color.PaleVioletRed, 0.3f), Color.Lerp(Color.PaleGoldenrod, Color.LightSalmon, Main.rand.NextFloat()), Main.rand.NextFloat());
		}
	}
}
