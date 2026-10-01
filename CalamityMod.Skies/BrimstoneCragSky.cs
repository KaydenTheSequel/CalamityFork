using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class BrimstoneCragSky : CustomSky
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

	public int skyActiveLeeway;

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
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.LocalPlayer.Calamity().ZoneCalamity || Main.gameMenu)
		{
			skyActive = false;
			if (skyActiveLeeway > 0)
			{
				skyActiveLeeway--;
			}
		}
		else if (skyActiveLeeway < 60)
		{
			skyActiveLeeway++;
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
		if (skyActive || skyActiveLeeway > 0)
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
		if (!Main.LocalPlayer.Calamity().ZoneCalamity)
		{
			Filters.Scene["CalamityMod:BrimstoneCrag"].Deactivate();
		}
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
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.LocalPlayer.Calamity().ZoneCalamity && skyActiveLeeway == 0)
		{
			return;
		}
		Texture2D cinderTexture = ModContent.Request<Texture2D>("CalamityMod/Skies/CalamitasCinder", (AssetRequestMode)2).Value;
		float scaleFade = (float)skyActiveLeeway / 60f;
		Color offsetDrawColor = Color.Red * 0.56f;
		((Color)(ref offsetDrawColor)).A = 0;
		Vector2 origin = cinderTexture.Size() * 0.5f;
		float cinderScale = 1.5f * scaleFade;
		for (int i = 0; i < Cinders.Count; i++)
		{
			Vector2 drawPosition = Cinders[i].Center - Main.screenPosition;
			for (int j = 0; j < 3; j++)
			{
				Vector2 offsetDrawPosition = drawPosition + ((float)Math.PI * 2f * (float)j / 3f).ToRotationVector2() * 1.4f;
				spriteBatch.Draw(cinderTexture, offsetDrawPosition, (Rectangle?)null, offsetDrawColor, 0f, origin, Cinders[i].Scale * cinderScale, (SpriteEffects)0, 0f);
			}
			spriteBatch.Draw(cinderTexture, drawPosition, (Rectangle?)null, Cinders[i].DrawColor, 0f, origin, Cinders[i].Scale * scaleFade, (SpriteEffects)0, 0f);
		}
	}
}
