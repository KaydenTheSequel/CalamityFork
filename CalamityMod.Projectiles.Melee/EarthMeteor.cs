using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class EarthMeteor : ModProjectile, ILocalizedModType, IModType
{
	public SlotId AudSlot;

	public Color mainColor;

	public Color randomColor;

	public Color variedColor;

	public int colorTimer;

	public int fallTime;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 84;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_071e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_0806: Unknown result type (might be due to invalid IL or missing references)
		//IL_083e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05df: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_066a: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		NPC targeted = ((base.Projectile.ai[1] == 0f) ? Owner.ClampedMouseWorld().ClosestNPCAt(1000f) : Main.npc[(int)base.Projectile.ai[1]]);
		base.Projectile.scale = 1.2f;
		randomColor = (Color)(Main.rand.Next(3) switch
		{
			0 => Color.OrangeRed, 
			1 => Color.MediumTurquoise, 
			_ => Color.LawnGreen, 
		});
		if (time == 0f)
		{
			mainColor = randomColor;
		}
		if (time % 20f == 0f)
		{
			variedColor = (Color)(colorTimer switch
			{
				0 => Color.OrangeRed, 
				1 => Color.MediumTurquoise, 
				_ => Color.LawnGreen, 
			});
			colorTimer++;
			if (colorTimer >= 3)
			{
				colorTimer = 0;
			}
		}
		mainColor = Color.Lerp(mainColor, variedColor, 0.07f);
		if (time == 0f && base.Projectile.ai[2] == 2f)
		{
			SoundStyle fire2 = new SoundStyle("CalamityMod/Sounds/Item/WeldingShoot");
			SoundStyle style = fire2 with
			{
				Volume = 0.01f,
				Pitch = 0.01f,
				IsLooped = true
			};
			AudSlot = SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound) && ChargeSound.IsPlaying && base.Projectile.ai[2] == 2f)
		{
			ChargeSound.Position = base.Projectile.Center;
			ChargeSound.Pitch = Utils.Remap(time, 0f, fallTime, 0.4f, -0.8f) * 100f;
			ChargeSound.Volume = Utils.Remap(time, (float)fallTime * 0.2f, fallTime, 0f, 0.9f) * 100f;
		}
		if (time == (float)(int)((float)fallTime * 0.2f) && base.Projectile.ai[2] > 0f)
		{
			Projectile.NewProjectile(position: (targeted != null && targeted.active && targeted.life > 0) ? (targeted.Center + new Vector2(Main.rand.NextFloat(-450f, 450f), Main.rand.NextFloat(-450f, -650f))) : (Owner.Center + new Vector2(Main.rand.NextFloat(-450f, 450f), Main.rand.NextFloat(-450f, -650f))), spawnSource: base.Projectile.GetSource_FromThis(), velocity: Vector2.Zero, Type: ModContent.ProjectileType<EarthMeteor>(), Damage: base.Projectile.damage, KnockBack: base.Projectile.knockBack, Owner: base.Projectile.owner, ai0: 0f, ai1: base.Projectile.ai[1], ai2: base.Projectile.ai[2] - 1f);
		}
		if (time == (float)fallTime)
		{
			for (int i = 0; i < 2; i++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), 0f, 0.8f, 0f, 27, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), 0f, 0.65f, 0f, 27, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			base.Projectile.extraUpdates = 30;
			NPC target = Owner.ClampedMouseWorld().ClosestNPCAt(1000f);
			if (target != null)
			{
				base.Projectile.velocity = (target.Center - base.Projectile.Center + target.velocity * 8f).SafeNormalize(Vector2.UnitX) * 8f;
			}
			else
			{
				base.Projectile.velocity = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 8f;
			}
		}
		if (time >= (float)fallTime)
		{
			float fadeIn = Utils.GetLerpValue((float)fallTime * 1.7f, fallTime, time, clamped: true);
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.475f / (float)Math.PI);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * (16f + 34f * fadeIn);
			if (targetDist < 1400f && time % 2f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + offset, -base.Projectile.velocity * 0.5f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 10, 0.3f + 0.4f * fadeIn, mainColor, new Vector2(0.5f, 1f)));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center - offset, -base.Projectile.velocity * 0.5f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 10, 0.3f + 0.4f * fadeIn, mainColor, new Vector2(0.5f, 1f)));
			}
		}
		else
		{
			float randSize = Main.rand.NextFloat(0.8f, 1.2f);
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor * (float)Math.Pow(Utils.Remap(time, 0f, fallTime, 0f, 1f), 3.0), "CalamityMod/Particles/LargeBloom", new Vector2(Utils.Remap(time, 0f, fallTime, 0.3f, 4f), Utils.Remap(time, (float)fallTime * 0.6f, fallTime, 1f, 4f)), 0f, 0.8f * randSize, 0f, 3, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * (float)Math.Pow(Utils.Remap(time, 0f, fallTime, 0f, 1f), 3.0), "CalamityMod/Particles/LargeBloom", new Vector2(Utils.Remap(time, 0f, fallTime, 0.3f, 4f), Utils.Remap(time, (float)fallTime * 0.6f, fallTime, 1f, 4f)), 0f, 0.65f * randSize, 0f, 3, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearRagged", (AssetRequestMode)2);
		if (time <= (float)fallTime)
		{
			return false;
		}
		for (int i = 0; i < 10; i++)
		{
			Texture2D value = tex.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * (float)(i - 110);
			Color color = mainColor;
			((Color)(ref color)).A = 0;
			Main.EntitySpriteDraw(value, position, null, color, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale * new Vector2(0.4f - (float)i * 0.05f, 1.3f + (float)i * 0.1f) * 0.4f, (SpriteEffects)((i % 2 != 0) ? 1 : 0));
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(AudSlot, out ActiveSound ChargeSound) && base.Projectile.ai[2] == 2f)
		{
			ChargeSound?.Stop();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
		if (base.Projectile.numHits <= 0)
		{
			Main.player[base.Projectile.owner].SetScreenshake(4.5f);
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<EarthBoom>(), (int)((float)base.Projectile.damage * 0.75f), base.Projectile.knockBack, base.Projectile.owner);
			for (int i = 0; i < 15; i++)
			{
				randomColor = (Color)(Main.rand.Next(3) switch
				{
					0 => Color.OrangeRed, 
					1 => Color.MediumTurquoise, 
					_ => Color.LawnGreen, 
				});
				Dust dust = Dust.NewDustPerfect(target.Center, 278, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(5.5f, 20f));
				dust.scale = Main.rand.NextFloat(0.85f, 1.15f);
				dust.noGravity = false;
				dust.color = Color.Lerp(Color.White, randomColor, 0.5f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(5.5f, 20f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, 38, Main.rand.NextFloat(2.2f, 4.8f), randomColor, new Vector2(0.4f, Main.rand.NextFloat(0.9f, 1.4f)), useAddativeBlend: true, glowCenter: true));
			}
			for (int j = 0; j < 3; j++)
			{
				string tex = "CalamityMod/Particles/ShatteredExplosion";
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, Vector2.Zero, tex, affectedByGravity: false, 12, 0.5f - (float)j * 0.08f, Color.OrangeRed * 0.8f, Vector2.One * 0.65f, useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f), fadeIn: false, affectedByLight: false, (j == 0) ? 0.6f : 0.8f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, Vector2.Zero, tex, affectedByGravity: false, 10, 0.4f - (float)j * 0.08f, Color.MediumTurquoise * 0.8f, Vector2.One * 0.65f, useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f), fadeIn: false, affectedByLight: false, (j == 0) ? 0.6f : 0.8f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, Vector2.Zero, tex, affectedByGravity: false, 8, 0.35f - (float)j * 0.08f, Color.LawnGreen * 0.8f, Vector2.One * 0.65f, useAddativeBlend: true, glowCenter: false, Main.rand.NextFloat(-10f, 10f), fadeIn: false, affectedByLight: false, (j == 0) ? 0.6f : 0.8f));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/EarthMeteor");
			style.Volume = 0.9f;
			SoundEngine.PlaySound(in style, target.Center);
			if (!CalamityClientConfig.Instance.Photosensitivity)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, mainColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 4f, 3f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 3f, 2f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
		}
	}

	public override bool? CanDamage()
	{
		if (!(time < (float)fallTime))
		{
			return null;
		}
		return false;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 80f, targetHitbox);
	}

	public EarthMeteor()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		mainColor = Color.White;
		randomColor = Color.White;
		variedColor = Color.White;
		fallTime = 180;
		base._002Ector();
	}
}
