using System;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SHPB : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public enum SoulType
	{
		Light,
		Night,
		Flight,
		Might,
		Sight,
		Fright
	}

	public GeneralDrawLayer LayerToRenderTo => GeneralDrawLayer.BeforeProjectiles;

	public ref float ExplodeTimer => ref base.Projectile.ai[2];

	public bool CanExplodeFromProximity
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 0;
		base.Projectile.scale = 0f;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public static SoulType GetSoulEffects(int projai)
	{
		return projai switch
		{
			0 => SoulType.Light, 
			1 => SoulType.Night, 
			2 => SoulType.Flight, 
			3 => SoulType.Might, 
			4 => SoulType.Sight, 
			5 => SoulType.Fright, 
			_ => SoulType.Light, 
		};
	}

	public static Color FindColorForSoul(int projai)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(projai switch
		{
			0 => new Color(240, 29, 196), 
			1 => new Color(123, 29, 220), 
			2 => new Color(106, 240, 250), 
			3 => new Color(4, 51, 222), 
			4 => new Color(79, 255, 124), 
			5 => new Color(255, 96, 20), 
			_ => new Color(0, 0, 0), 
		});
	}

	public override void AI()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0782: Unknown result type (might be due to invalid IL or missing references)
		//IL_0787: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0669: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		float lights = (float)Main.rand.Next(90, 111) * 0.01f;
		lights *= Main.essScale;
		Lighting.AddLight(base.Projectile.Center, 1f * lights, 0.2f * lights, 0.75f * lights);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 3)
			{
				base.Projectile.frame = 0;
			}
		}
		if (GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Sight)
		{
			float npcDistCheck = 320f;
			int index = -1;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					float currentNPCDist = Vector2.Distance(n.Center, base.Projectile.Center);
					if (currentNPCDist < npcDistCheck)
					{
						npcDistCheck = currentNPCDist;
						index = n.whoAmI;
					}
				}
			}
			if (index != -1)
			{
				base.Projectile.velocity = (base.Projectile.velocity * 17f + base.Projectile.Center.DirectionTo(Main.npc[index].Center) * 20f) / 18f;
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.9875f;
			}
		}
		else if (GetSoulEffects((int)base.Projectile.ai[0]) != SoulType.Flight)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.9875f;
		}
		float explodeRange = 250f;
		ActiveEntityIterator<NPC>.Enumerator enumerator2 = Main.ActiveNPCs.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			NPC n2 = enumerator2.Current;
			if (n2.CanBeChasedBy(base.Projectile) && Collision.CanHit(base.Projectile.Center, 1, 1, n2.Center, 1, 1))
			{
				float npcX = n2.position.X + (float)(n2.width / 2);
				float npcY = n2.position.Y + (float)(n2.height / 2);
				float npcDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY);
				if (npcDist < explodeRange)
				{
					explodeRange = npcDist;
					CanExplodeFromProximity = true;
				}
			}
		}
		if (CanExplodeFromProximity)
		{
			ExplodeTimer++;
			if (ExplodeTimer >= 60f)
			{
				base.Projectile.Kill();
			}
			bool lightSoul = GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Light;
			float minScale = 0.5f * (lightSoul ? 1.5f : 1f);
			float maxScale = 1.9f * (lightSoul ? 1.5f : 1f);
			base.Projectile.scale = MathHelper.Clamp(base.Projectile.scale - 0.075f, minScale, maxScale);
		}
		else if (base.Projectile.timeLeft >= 285)
		{
			bool lightSoul2 = GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Light;
			float minScale2 = 1.5f * (lightSoul2 ? 1.5f : 1f);
			base.Projectile.scale += 0.125f;
			if (base.Projectile.scale > minScale2)
			{
				base.Projectile.scale = minScale2;
			}
		}
		else if (base.Projectile.timeLeft <= 285 && base.Projectile.timeLeft >= 45)
		{
			bool lightSoul3 = GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Light;
			float minScale3 = 1.5f * (lightSoul3 ? 1.5f : 1f);
			float maxScale2 = 1.9f * (lightSoul3 ? 1.5f : 1f);
			base.Projectile.localAI[0]++;
			base.Projectile.scale = MathHelper.Lerp(minScale3, maxScale2, CalamityUtils.SineBumpEasing(base.Projectile.localAI[0] / 75f, 1));
		}
		else
		{
			if (base.Projectile.localAI[1] == 0f)
			{
				base.Projectile.localAI[1] = base.Projectile.scale;
			}
			base.Projectile.scale = Utils.Remap(base.Projectile.timeLeft, 45f, 0f, base.Projectile.localAI[1], 0.35f);
		}
		base.Projectile.ExpandHitboxBy((int)(24f * base.Projectile.scale));
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float deathInterpolant = ((ExplodeTimer > 0f) ? Utils.GetLerpValue(0f, 50f, ExplodeTimer, clamped: true) : Utils.GetLerpValue(60f, 10f, base.Projectile.timeLeft, clamped: true));
		if (Main.rand.NextBool(3) && deathInterpolant <= 0f)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular((float)base.Projectile.width * 0.75f, (float)base.Projectile.height * 0.75f);
				Vector2 dustVelocity = base.Projectile.velocity * -1.2f;
				Dust dust = Dust.NewDustDirect(position, 1, 1, 43, dustVelocity.X, dustVelocity.Y);
				dust.scale = Main.rand.NextFloat(0.8f, 1.2f);
				dust.noGravity = true;
				dust.noLight = false;
				dust.color = FindColorForSoul((int)base.Projectile.ai[0]);
				dust.velocity *= 0.9f;
			}
		}
		if (deathInterpolant > 0f)
		{
			float speedMultiplier = MathHelper.Lerp(1f, 3f, deathInterpolant);
			float scaleMultiplier = MathHelper.Lerp(1f, 1.65f, deathInterpolant);
			for (int j = 0; j < 3; j++)
			{
				Vector2 dustVelocity2 = Main.rand.NextVector2Circular(1f, 1f) * 2.5f * speedMultiplier;
				Dust dust2 = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity2.X, dustVelocity2.Y);
				dust2.scale = Main.rand.NextFloat(1.4f, 1.8f) * scaleMultiplier;
				dust2.color = FindColorForSoul((int)base.Projectile.ai[0]);
				dust2.noGravity = true;
				dust2.noLight = false;
			}
			Vector2 plasmaVelocity = Main.rand.NextVector2Circular(1f, 1f) * 1.35f * speedMultiplier;
			Color plasmaColor = Color.Lerp(FindColorForSoul((int)base.Projectile.ai[0]), Color.White, deathInterpolant);
			float plasmaScale = Main.rand.NextFloat(0.2f, 0.4f) * scaleMultiplier;
			int plasmaLifetime = Main.rand.Next(30, 45);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, plasmaVelocity, plasmaScale, plasmaColor, plasmaLifetime));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		if (GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Flight)
		{
			if (owner.wingTime < (float)owner.wingTimeMax)
			{
				owner.wingTime += 25f;
			}
			if (owner.wingTime > (float)owner.wingTimeMax)
			{
				owner.wingTime = owner.wingTimeMax;
			}
		}
		if (GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Might && target.CanBeMoved())
		{
			Vector2 launchVel = owner.Center.DirectionTo(owner.Calamity().mouseWorld) - Vector2.UnitY * 5f;
			target.MoveNPC(launchVel, 20f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Fright)
		{
			modifiers.SourceDamage.Flat += 20f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item105, base.Projectile.Center);
		float screenshake = ((GetSoulEffects((int)base.Projectile.ai[0]) == SoulType.Light) ? 5f : 3.5f);
		Main.LocalPlayer.SetScreenshake(screenshake);
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SHPExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0]);
			for (int i = 0; i < 8; i++)
			{
				bool pickup = i >= 5;
				Vector2 soulVelocity = -Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (pickup ? 2.75f : Main.rand.NextFloat(6f, 9f));
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, soulVelocity, ModContent.ProjectileType<SHPS>(), (int)((float)base.Projectile.damage * 0.33f), 0f, base.Projectile.owner, Main.rand.Next(6), 0f, pickup ? 1f : 0f);
				if (pickup)
				{
					Main.projectile[p].timeLeft *= 3;
				}
			}
		}
		for (int j = 0; j < 25; j++)
		{
			Vector2 plasmaVelocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(6f, 11f);
			Color plasmaColor = Color.Lerp(FindColorForSoul((int)base.Projectile.ai[0]), Color.White, Main.rand.NextFloat());
			float plasmaScale = Main.rand.NextFloat(0.8f, 1.4f);
			int plasmaLifetime = Main.rand.Next(30, 45);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, plasmaVelocity, plasmaScale, plasmaColor, plasmaLifetime));
		}
		for (int k = 0; k < 15; k++)
		{
			Vector2 dustVelocity = Main.rand.NextVector2Circular(1f, 1f) * 6f;
			float dustScale = Main.rand.NextFloat(1.8f, 2.4f);
			Color dustColor = Color.Lerp(FindColorForSoul((int)base.Projectile.ai[0]), Color.White, Main.rand.NextFloat());
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity.X, dustVelocity.Y, 0, dustColor, dustScale);
			dust.noGravity = true;
			dust.noLight = false;
			dust.noLightEmittence = false;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D bloomCircleTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		float deathInterpolant = ((ExplodeTimer > 0f) ? Utils.GetLerpValue(0f, 50f, ExplodeTimer, clamped: true) : Utils.GetLerpValue(60f, 10f, base.Projectile.timeLeft, clamped: true));
		float shakeStrength = MathHelper.Lerp(0f, 6f, deathInterpolant);
		Vector2 drawPosition = base.Projectile.Center + Main.rand.NextVector2Circular(shakeStrength, shakeStrength) - Main.screenPosition;
		Color drawColor = Color.Lerp(FindColorForSoul((int)base.Projectile.ai[0]), Color.White, deathInterpolant);
		Color bloomColor = Color.Lerp(drawColor, Color.White, 0.25f);
		Main.spriteBatch.SetBlendState(BlendState.Additive);
		Main.EntitySpriteDraw(bloomCircleTexture, drawPosition, null, base.Projectile.GetAlpha(bloomColor) * 0.85f, base.Projectile.rotation, bloomCircleTexture.Size() / 2f, base.Projectile.scale * 0.65f, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(drawColor), base.Projectile.rotation, frame.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(value, drawPosition, frame, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, frame.Size() / 2f, base.Projectile.scale * 0.8f, (SpriteEffects)0);
		Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		return false;
	}

	public float PlasmaBallWidthFunction(float completion, Vector2 vertexPos)
	{
		float deathInterpolant = ((ExplodeTimer > 0f) ? Utils.GetLerpValue(0f, 50f, ExplodeTimer, clamped: true) : Utils.GetLerpValue(60f, 10f, base.Projectile.timeLeft, clamped: true));
		float maxBodyWidth = base.Projectile.scale * 58f;
		float curveRatio = 0.15f;
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Sin(completion / curveRatio * ((float)Math.PI / 2f)) * maxBodyWidth + curveRatio));
		return width * MathHelper.Lerp(1f, 0f, deathInterpolant);
	}

	public Color PlasmaBallColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.Lerp(Color.Lerp(Color.Transparent, FindColorForSoul((int)base.Projectile.ai[0]), Utils.GetLerpValue(0f, 0.35f, completion)), FindColorForSoul((int)base.Projectile.ai[0]), Utils.GetLerpValue(0.35f, 1f, completion));
		float deathInterpolant = ((ExplodeTimer > 0f) ? Utils.GetLerpValue(0f, 50f, ExplodeTimer, clamped: true) : Utils.GetLerpValue(60f, 10f, base.Projectile.timeLeft, clamped: true));
		return Color.Lerp(val, Color.White, deathInterpolant);
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PlasmaBallWidthFunction, PlasmaBallColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), base.Projectile.oldPos.Length * 2);
	}
}
