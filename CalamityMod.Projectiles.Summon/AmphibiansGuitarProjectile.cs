using System;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AmphibiansGuitarProjectile : ModProjectile, ILocalizedModType, IModType
{
	public Color noteColor;

	public int time;

	public NPC targeted;

	public override string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool IsHatNote => base.Projectile.ai[0] != 0f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 44;
		base.Projectile.timeLeft = 150;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0 && base.Projectile.ai[2] == 5f)
		{
			base.Projectile.penetrate = 3;
		}
		base.Projectile.scale = 1.6f * Utils.GetLerpValue(-5f, 20f, time, clamped: true);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (time % 10 == 0)
		{
			if (base.Projectile.ai[1] < 4f)
			{
				base.Projectile.ai[1]++;
			}
			else
			{
				base.Projectile.ai[1] = 0f;
			}
		}
		float num = base.Projectile.ai[1];
		Color val = ((num == 0f) ? Color.Red : ((num == 1f) ? Color.Cyan : ((num == 2f) ? Color.Goldenrod : ((num != 3f) ? Color.Lime : Color.Magenta))));
		Color chooseColor = val;
		if (time == 0)
		{
			noteColor = chooseColor;
		}
		else
		{
			noteColor = Color.Lerp(noteColor, chooseColor, 0.07f);
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref noteColor)).ToVector3() * 0.5f);
		if (time % 3 == 0)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(40f, 40f), ModContent.DustType<LightDust>(), -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.5f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.85f, 1.35f);
			dust.color = noteColor;
			dust.noLightEmittence = true;
		}
		if (base.Projectile.ai[2] == 5f && time % 2 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 8, 0.065f, noteColor * 0.75f, new Vector2(1f, 0.3f), quickShrink: true, glow: false));
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= (IsHatNote ? 1.5f : 1f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		if (IsHatNote)
		{
			SoundStyle style = SoundID.DD2_WitherBeastCrystalImpact with
			{
				Volume = 0.8f,
				Pitch = -0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		for (int i = 0; i <= (IsHatNote ? 8 : 4); i++)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(IsHatNote ? 100f : 0.5f) * Main.rand.NextFloat(0.4f, 1.3f), affectedByGravity: false, 15, 1.2f, noteColor));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Evernote", (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/EvernoteWall", (AssetRequestMode)2);
		ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2);
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color val = noteColor;
		((Color)(ref val)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, val * 0.4f, 1, tex.Value, drawCentered: true, shrink: true);
		Texture2D value = tex.Value;
		val = noteColor;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(value, generalDrawPos, null, val, base.Projectile.rotation, tex.Size() * 0.5f, new Vector2(0.9f * Utils.GetLerpValue(-5f, 20f, time, clamped: true), 1f) * base.Projectile.scale, (SpriteEffects)0);
		Texture2D value2 = tex.Value;
		val = Color.Lerp(Color.White, noteColor, 0.15f);
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(value2, generalDrawPos, null, val, base.Projectile.rotation, tex.Size() * 0.5f, new Vector2(0.9f * Utils.GetLerpValue(-5f, 20f, time, clamped: true), 1f) * base.Projectile.scale * 0.88f, (SpriteEffects)0);
		if (IsHatNote)
		{
			for (int i = 0; i < 3; i++)
			{
				Texture2D value3 = tex2.Value;
				val = noteColor;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value3, generalDrawPos, null, val, Main.GlobalTimeWrappedHourly * 7.3f + (float)(i * 2), tex2.Size() * 0.5f, base.Projectile.scale * (0.7f + (float)i * 0.7f) * 0.9f, (SpriteEffects)0);
			}
		}
		return false;
	}

	public AmphibiansGuitarProjectile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		noteColor = Color.White;
		base._002Ector();
	}
}
