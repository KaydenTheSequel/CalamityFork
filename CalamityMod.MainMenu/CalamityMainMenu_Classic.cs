using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.MainMenu;

public class CalamityMainMenu_Classic : ModMenu
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

	public override string DisplayName => CalamityUtils.GetTextValue("UI.MainMenuClassic");

	public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>("CalamityMod/MainMenu/Logo", (AssetRequestMode)2);

	public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>("CalamityMod/Backgrounds/BlankPixel", (AssetRequestMode)2);

	public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>("CalamityMod/Backgrounds/BlankPixel", (AssetRequestMode)2);

	public override int Music => CalamityMod.Instance.GetMusicFromMusicMod("CalamityTitle") ?? 6;

	public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullSurfaceBackground>();

	public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/MainMenu/ClassicMenuBackground", (AssetRequestMode)2).Value;
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
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			if (Main.rand.NextBool(3))
			{
				return Color.Lerp(Color.DarkGray, Color.LightGray, Main.rand.NextFloat());
			}
			return Color.Lerp(Color.Red, Color.Yellow, Main.rand.NextFloat(0.9f));
		}
	}
}
