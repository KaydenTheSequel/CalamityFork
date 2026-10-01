using System;
using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Systems.Mechanic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class SCalSky : CustomSky
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

		public float Opacity;

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

	private bool isActive;

	private float intensity;

	private int SCalIndex = -1;

	public List<Cinder> Cinders = new List<Cinder>();

	private Asset<Texture2D> backgroundTex;

	private Asset<Texture2D> cinderTex;

	public static float OverridingIntensity;

	public static bool RitualDramaProjectileIsPresent { get; internal set; }

	public static int CinderReleaseChance
	{
		get
		{
			if (!Main.npc.IndexInRange(CalamityGlobalNPC.SCal) || Main.npc[CalamityGlobalNPC.SCal].type != ModContent.NPCType<SupremeCalamitas>())
			{
				return int.MaxValue;
			}
			NPC scal = Main.npc[CalamityGlobalNPC.SCal];
			float lifeRatio = (float)scal.life / (float)scal.lifeMax;
			if (scal.ModNPC<SupremeCalamitas>().bulletHellCounter2 % 900 != 0)
			{
				if (!(lifeRatio <= 0.1f))
				{
					return 11;
				}
				return 8;
			}
			if (lifeRatio < 0.1f)
			{
				if (lifeRatio <= 0.01f)
				{
					return 25;
				}
				return (int)Math.Round(MathHelper.Lerp(2f, 11f, Utils.GetLerpValue(0.03f, 0.1f, lifeRatio, clamped: true)));
			}
			if (scal.ModNPC<SupremeCalamitas>().postMusicHit)
			{
				return 6;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>()) || NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>()) || NPC.AnyNPCs(ModContent.NPCType<SepulcherHead>()))
			{
				return 10;
			}
			if ((!NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>()) && NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>())) || (!NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>()) && NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>())))
			{
				return 7;
			}
			return 18;
		}
	}

	public static float CinderSpeed
	{
		get
		{
			if (!Main.npc.IndexInRange(CalamityGlobalNPC.SCal) || Main.npc[CalamityGlobalNPC.SCal].type != ModContent.NPCType<SupremeCalamitas>())
			{
				return 0f;
			}
			NPC scal = Main.npc[CalamityGlobalNPC.SCal];
			float lifeRatio = (float)scal.life / (float)scal.lifeMax;
			if (lifeRatio < 0.1f)
			{
				if (lifeRatio <= 0.01f)
				{
					return 4.5f;
				}
				return MathHelper.Lerp(10.75f, 6.7f, Utils.GetLerpValue(0.03f, 0.1f, lifeRatio, clamped: true));
			}
			if (NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>()) || NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>()) || NPC.AnyNPCs(ModContent.NPCType<SepulcherHead>()))
			{
				return 7.4f;
			}
			if (scal.ModNPC<SupremeCalamitas>().postMusicHit)
			{
				return 25f * MathHelper.Clamp(Utils.GetLerpValue(-60f, 0f, scal.ModNPC<SupremeCalamitas>().musicSyncCounter), 0.5f, 1f);
			}
			return 5.6f;
		}
	}

	public override void Update(GameTime gameTime)
	{
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		intensity = GetIntensity();
		if (SCalIndex == -1)
		{
			UpdateSCalIndex();
		}
		if (!Main.npc.IndexInRange(CalamityGlobalNPC.SCal) || Main.npc[CalamityGlobalNPC.SCal].type != ModContent.NPCType<SupremeCalamitas>() || SCalIndex == -1)
		{
			intensity -= 0.1f;
			if (intensity <= 0f)
			{
				isActive = false;
			}
			return;
		}
		if (Main.rand.NextBool(CinderReleaseChance) && CalamityGlobalNPC.SCal >= 0)
		{
			ArenaWallSystem.Box box = Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().ArenaBox;
			int lifetime = Main.rand.Next(485, 645);
			float depth = Main.rand.NextFloat(1.8f, 5f);
			Vector2 startingPosition = Main.screenPosition + new Vector2((float)Main.screenWidth * Main.rand.NextFloat(-0.1f, 1.1f), (float)Main.screenHeight * Main.rand.NextFloat(-0.1f, 1.1f));
			startingPosition = Main.rand.NextVector2FromRectangle(box.HitboxRectangle) + new Vector2(0f, 180f);
			startingPosition = Vector2.Lerp(box.BottomLeft, box.BottomRight, Main.rand.NextFloat());
			Vector2 startingVelocity = -Vector2.UnitY.RotatedByRandom(0.9100000262260437);
			Cinders.Add(new Cinder(lifetime, Cinders.Count, depth, selectCinderColor(), startingPosition, startingVelocity));
		}
		else if (Main.npc.IndexInRange(CalamityGlobalNPC.SCal) && Main.npc[CalamityGlobalNPC.SCal].type == ModContent.NPCType<SupremeCalamitas>() && Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().musicSyncCounter == 0)
		{
			for (int i = 0; i < 70; i++)
			{
				int lifetime2 = Main.rand.Next(285, 445);
				float depth2 = Main.rand.NextFloat(1.8f, 5f);
				Vector2 startingPosition2 = Main.screenPosition + new Vector2((float)Main.screenWidth * Main.rand.NextFloat(-0.1f, 1.1f), (float)Main.screenHeight * Main.rand.NextFloat(1.05f, 1.45f));
				Vector2 startingVelocity2 = -Vector2.UnitY.RotatedByRandom(0.9100000262260437) * Main.rand.NextFloat(0.5f, 2.5f);
				Cinders.Add(new Cinder(lifetime2, Cinders.Count, depth2, SupremeCalamitas.LamentColor, startingPosition2, startingVelocity2));
			}
		}
		for (int j = 0; j < Cinders.Count; j++)
		{
			if (Cinders[j].Opacity < 1f)
			{
				Cinders[j].Opacity += 0.1f;
			}
			Cinders[j].Scale = Utils.GetLerpValue(Cinders[j].Lifetime, Cinders[j].Lifetime / 3, Cinders[j].Time, clamped: true) * 1.5f;
			Cinders[j].Scale *= MathHelper.Lerp(0.6f, 0.9f, (float)Cinders[j].IdentityIndex % 6f / 6f);
			Vector2 idealVelocity = -Vector2.UnitY.RotatedBy(MathHelper.Lerp(-0.94f, 0.94f, (float)Math.Sin((float)Cinders[j].Time / 36f + (float)Cinders[j].IdentityIndex) * 0.5f + 0.5f)) * CinderSpeed;
			float movementInterpolant = MathHelper.Lerp(0.01f, 0.08f, Utils.GetLerpValue(45f, 145f, Cinders[j].Time, clamped: true));
			Cinders[j].Velocity = Vector2.Lerp(Cinders[j].Velocity, idealVelocity, movementInterpolant);
			Cinders[j].Velocity = Cinders[j].Velocity.SafeNormalize(-Vector2.UnitY) * CinderSpeed;
			Cinders[j].Time++;
			Cinder cinder = Cinders[j];
			cinder.Center += Cinders[j].Velocity;
		}
		Cinders.RemoveAll((Cinder c) => c.Time >= c.Lifetime);
		static Color selectCinderColor()
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return SupremeCalamitas.CurrentColor;
		}
	}

	private float GetIntensity()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (RitualDramaProjectileIsPresent)
		{
			return OverridingIntensity;
		}
		OverridingIntensity = 0f;
		if (UpdateSCalIndex() && SCalIndex > -1)
		{
			float x = 0f;
			if (SCalIndex != -1)
			{
				x = Vector2.Distance(Main.LocalPlayer.Center, Main.npc[SCalIndex].Center);
			}
			float intensityFactor = (BossRushEvent.BossRushActive ? (-0.2f) : 1f);
			return (1f - Utils.SmoothStep(4500f, 9000f, x)) * intensityFactor;
		}
		return intensity;
	}

	public override Color OnTileColor(Color inColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), ((Color)(ref inColor)).ToVector4(), 1f - GetIntensity()));
	}

	private bool UpdateSCalIndex()
	{
		int SCalType = ModContent.NPCType<SupremeCalamitas>();
		if (SCalIndex >= 0 && Main.npc[SCalIndex].active && Main.npc[SCalIndex].type == SCalType)
		{
			return true;
		}
		SCalIndex = NPC.FindFirstNPC(SCalType);
		return SCalIndex != -1;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		if (maxDepth >= 0f && minDepth < 0f)
		{
			Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
			Texture2D tex = CalamityUtils.GetTextureEfficient(ref backgroundTex, "CalamityMod/Skies/CalamitasBackground").Value;
			float repeatX = (float)((Viewport)(ref viewport)).Width / (float)tex.Width + 2f;
			float repeatY = (float)((Viewport)(ref viewport)).Height / (float)tex.Height + 2f;
			float intensity = GetIntensity();
			Vector2 ViewportToGamesizeRatio = new Vector2((float)((Viewport)(ref viewport)).Width, (float)((Viewport)(ref viewport)).Height) / new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * Main.GameZoomTarget;
			Point posOffset = Utils.ToPoint(new Vector2(MathF.Sin(Main.GlobalTimeWrappedHourly * 0.03f + (float)Math.PI / 2f) * 10f * (float)tex.Width, MathF.Cos(Main.GlobalTimeWrappedHourly * 0.1f + (float)Math.PI / 2f) * 17f * (float)tex.Height)) - (Main.LocalPlayer.position * ViewportToGamesizeRatio * Main.caveParallax).ToPoint();
			posOffset.X %= tex.Width;
			posOffset.Y %= tex.Height;
			Rectangle dest = default(Rectangle);
			((Rectangle)(ref dest))._002Ector(posOffset.X - tex.Width, posOffset.Y - tex.Height, (int)((float)tex.Width * repeatX), (int)((float)tex.Height * repeatY));
			spriteBatch.End();
			spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.None, RasterizerState.CullCounterClockwise);
			spriteBatch.Draw(tex, dest, (Rectangle?)new Rectangle(0, 0, (int)((float)tex.Width * repeatX), (int)((float)tex.Height * repeatY)), Color.White * intensity);
		}
		Texture2D cinderTexture = CalamityUtils.GetTextureEfficient(ref cinderTex, "CalamityMod/ExtraTextures/SmallGreyscaleCircle").Value;
		for (int i = 0; i < Cinders.Count; i++)
		{
			float OpacityMult = 0.1f + 0.3f * ((MathF.Sin((float)Cinders[i].Time * 0.1f + (float)Cinders[i].IdentityIndex) + 1f) * 0.5f);
			Vector2 drawPosition = Cinders[i].Center - Main.screenPosition;
			Vector2 cinderOrigin = cinderTexture.Size() * 0.5f;
			spriteBatch.Draw(cinderTexture, drawPosition, (Rectangle?)null, Cinders[i].DrawColor * Cinders[i].Opacity * OpacityMult, 0f, cinderOrigin, Cinders[i].Scale * 0.15f, (SpriteEffects)0, 0f);
		}
	}

	public override float GetCloudAlpha()
	{
		return 0f;
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		isActive = true;
	}

	public override void Deactivate(params object[] args)
	{
		isActive = false;
	}

	public override void Reset()
	{
		isActive = false;
	}

	public override bool IsActive()
	{
		if (!isActive)
		{
			return intensity > 0f;
		}
		return true;
	}
}
