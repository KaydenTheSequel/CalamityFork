using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class MonolithSky : CustomSky
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

	private bool skyActive;

	private float opacity;

	public List<Cinder> Cinders = new List<Cinder>();

	public override void Deactivate(params object[] args)
	{
		skyActive = false;
	}

	public override void Reset()
	{
		skyActive = false;
	}

	public override bool IsActive()
	{
		if (!skyActive)
		{
			return opacity > 0f;
		}
		return true;
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		skyActive = true;
	}

	public override void Update(GameTime gameTime)
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.Calamity().monolithAccursedShader < 1 || Main.gameMenu)
		{
			skyActive = false;
		}
		if (skyActive && opacity < 1f)
		{
			opacity += 0.02f;
		}
		else if (!skyActive && opacity > 0f)
		{
			opacity -= 0.02f;
		}
		if (Main.rand.NextBool(12) && skyActive)
		{
			int lifetime = Main.rand.Next(285, 445);
			float depth = Main.rand.NextFloat(1.8f, 5f);
			Vector2 startingPosition = Main.screenPosition + new Vector2((float)Main.screenWidth * Main.rand.NextFloat(-0.1f, 1.1f), (float)Main.screenHeight * 1.05f);
			Vector2 startingVelocity = -Vector2.UnitY.RotatedByRandom(0.9100000262260437);
			Cinders.Add(new Cinder(lifetime, Cinders.Count, depth, selectCinderColor(), startingPosition, startingVelocity));
		}
		if (skyActive)
		{
			float cinderSpeed = 5.6f;
			for (int i = 0; i < Cinders.Count; i++)
			{
				Cinders[i].Scale = Utils.GetLerpValue(Cinders[i].Lifetime, Cinders[i].Lifetime / 3, Cinders[i].Time, clamped: true);
				Cinders[i].Scale *= MathHelper.Lerp(0.6f, 0.9f, (float)Cinders[i].IdentityIndex % 6f / 6f);
				Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-0.94f, 0.94f, (float)Math.Sin((float)Cinders[i].Time / 36f + (float)Cinders[i].IdentityIndex) * 0.5f + 0.5f)) * cinderSpeed;
				float movementInterpolant = MathHelper.Lerp(0.01f, 0.08f, Utils.GetLerpValue(45f, 145f, Cinders[i].Time, clamped: true));
				Cinders[i].Velocity = Vector2.Lerp(Cinders[i].Velocity, idealVelocity, movementInterpolant);
				Cinders[i].Velocity = Cinders[i].Velocity.SafeNormalize(-Vector2.UnitY) * cinderSpeed;
				Cinders[i].Time++;
				Cinder cinder = Cinders[i];
				cinder.Center += Cinders[i].Velocity;
			}
		}
		Cinders.RemoveAll((Cinder c) => c.Time >= c.Lifetime);
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

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer.Calamity().monolithAccursedShader < 1)
		{
			return;
		}
		if (maxDepth >= float.MaxValue && minDepth < float.MaxValue && Main.LocalPlayer.Calamity().monolithAccursedShader > 21)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/MainMenu/ClassicMenuBackground", (AssetRequestMode)2).Value;
			int offset = (Main.BackgroundEnabled ? 200 : 0);
			spriteBatch.Draw(texture, new Rectangle(0, Math.Max(0, (int)((Main.worldSurface * 16.0 - (double)Main.screenPosition.Y - (double)(texture.Height * 2)) * 0.10000000149011612)) - offset, Main.screenWidth, Main.screenHeight), Color.White * Math.Min(1f, (Main.screenPosition.Y - 800f) / 1000f * opacity));
		}
		Texture2D cinderTexture = ModContent.Request<Texture2D>("CalamityMod/Skies/CalamitasCinder", (AssetRequestMode)2).Value;
		Color offsetDrawColor = Color.Red * 0.56f;
		((Color)(ref offsetDrawColor)).A = 0;
		Vector2 origin = cinderTexture.Size() * 0.5f;
		for (int i = 0; i < Cinders.Count; i++)
		{
			Vector2 drawPosition = Cinders[i].Center - Main.screenPosition;
			for (int j = 0; j < 3; j++)
			{
				Vector2 offsetDrawPosition = drawPosition + ((float)Math.PI * 2f * (float)j / 3f).ToRotationVector2() * 1.4f;
				spriteBatch.Draw(cinderTexture, offsetDrawPosition, (Rectangle?)null, offsetDrawColor, 0f, origin, Cinders[i].Scale * 1.5f, (SpriteEffects)0, 0f);
			}
			spriteBatch.Draw(cinderTexture, drawPosition, (Rectangle?)null, Cinders[i].DrawColor, 0f, origin, Cinders[i].Scale, (SpriteEffects)0, 0f);
		}
	}

	public override Color OnTileColor(Color color)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(color, new Color(205, 100, 100), opacity);
	}

	public override float GetCloudAlpha()
	{
		return (1f - opacity) * 0.3f + 0.7f;
	}
}
