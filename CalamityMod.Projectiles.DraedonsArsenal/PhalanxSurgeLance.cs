using System;
using CalamityMod.Effects;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PhalanxSurgeLance : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 startVel;

	public int time;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public bool didDash => base.Projectile.ai[2] == 5f;

	public override void SetDefaults()
	{
		base.Projectile.width = 35;
		base.Projectile.height = 35;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 10;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref ArsenalEffects.ArsenalLaserColor)).ToVector3());
		Player Owner = Main.player[base.Projectile.owner];
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (time == 0)
		{
			startVel = base.Projectile.velocity;
			for (int i = 0; i <= 15; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(40f, 40f), ArsenalEffects.ArsenalLaserDust);
				dust.velocity = (base.Projectile.velocity * 10f).RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.2f, 2f);
				dust.scale = Main.rand.NextFloat(0.4f, 0.8f);
				dust.noGravity = true;
				dust.color = ArsenalEffects.ArsenalLaserColor;
			}
			for (int j = 0; j < 20; j++)
			{
				Vector2 place = base.Projectile.Center + startVel * 20f + Main.rand.NextVector2Circular(5f, 5f);
				int dir = (Main.rand.NextBool() ? 1 : (-1));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(place, -base.Projectile.velocity.RotatedBy(0.42f * (float)dir).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(12f, 35f), "CalamityMod/Particles/BloomLineSoftEdge", affectedByGravity: false, 7, 0.025f, ArsenalEffects.ArsenalLaserColor, new Vector2(1f, 0.7f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 1.3f));
				if (j % 2 == 0)
				{
					Dust dust2 = Dust.NewDustPerfect(place, ArsenalEffects.ArsenalLaserDust, -base.Projectile.velocity.RotatedBy(0.42f * (float)dir).RotatedByRandom(0.10000000149011612) * Main.rand.NextFloat(4f, 20f), 0, default(Color), Main.rand.NextFloat(0.7f, 1.2f));
					dust2.noGravity = true;
					dust2.color = ArsenalEffects.ArsenalLaserColor;
					dust2.alpha = 100;
				}
			}
		}
		base.Projectile.rotation = startVel.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.Center = Owner.Center + startVel * 30f;
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		bool onKill = target.life <= 0 && target.realLife == -1;
		_ = Main.player[base.Projectile.owner];
		float distMult = Utils.GetLerpValue(400f, 10f, target.Center.Distance(base.Projectile.Center), clamped: true);
		Vector2 launchVel = startVel;
		target.MoveNPC(launchVel, 10f + 35f * distMult, ignoreKBImmune: true);
		if (base.Projectile.numHits == 0)
		{
			if (!onKill || !didDash)
			{
				for (int i = -5; i <= 5; i++)
				{
					if (i == 0)
					{
						i++;
					}
					GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, startVel * (float)i * 2f, "CalamityMod/Particles/BloomLineSoftEdge", affectedByGravity: false, 12, 0.085f - (float)Math.Abs(i) * 0.012f, ArsenalEffects.ArsenalLaserColor * 1f, new Vector2((float)(6 - Math.Abs(i)) * 0.4f, 1f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 1.2f, 1f, 0.6f));
				}
				if (base.Projectile.timeLeft > 10)
				{
					base.Projectile.timeLeft = 10;
				}
				for (int x = 0; x < Main.maxProjectiles; x++)
				{
					Projectile projectile = Main.projectile[x];
					if (projectile.active && projectile.type == ModContent.ProjectileType<PhalanxSurgeHoldout>() && projectile.owner == base.Projectile.owner)
					{
						projectile.localAI[0] = distMult;
					}
				}
			}
			else if (didDash)
			{
				int extention = 25;
				for (int j = 0; j < Main.maxProjectiles; j++)
				{
					Projectile projectile2 = Main.projectile[j];
					if (projectile2.active && projectile2.type == ModContent.ProjectileType<PhalanxSurgeHoldout>() && projectile2.owner == base.Projectile.owner && projectile2.ai[0] > (float)(-extention))
					{
						projectile2.ai[0] -= extention;
					}
				}
				if (base.Projectile.timeLeft < extention)
				{
					base.Projectile.timeLeft = extention;
				}
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldShoot");
			style.Volume = 1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		for (int k = 0; k < MathHelper.Clamp(25 - base.Projectile.numHits * 3, 1, 10); k++)
		{
			Vector2 velocity = startVel.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(8f, 20f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalLaserDust, velocity, 0, default(Color), Main.rand.NextFloat(0.9f, 1.8f));
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalLaserColor;
			dust.alpha = 100;
			dust.fadeIn = -3f;
		}
		if (onKill)
		{
			base.Projectile.numHits--;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.4f;
		int hitsToMinMult = 10;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (startVel == Vector2.Zero)
		{
			return false;
		}
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float _ = float.NaN;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, base.Projectile.Center + startVel * 145f, (float)base.Projectile.width * base.Projectile.scale, ref _);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		if (time < 1)
		{
			return false;
		}
		Texture2D pointTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomLineFade", (AssetRequestMode)2).Value;
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float fade = Utils.GetLerpValue(0f, 5f, base.Projectile.timeLeft, clamped: true);
		Vector2 distVel = startVel * 10f;
		Color val;
		for (int i = 0; i < 6; i++)
		{
			Vector2 position = base.Projectile.Center - Main.screenPosition + distVel * (float)(3 + i);
			val = ArsenalEffects.ArsenalLaserColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(pointTexture, position, null, val * fade, base.Projectile.rotation, pointTexture.Size() * 0.5f, new Vector2(0.5f * (1f - (float)i * 0.08f) * fade, 0.8f * (1f + (float)i * 0.07f)) * 0.11f, (SpriteEffects)0);
		}
		for (int j = 0; j < 6; j++)
		{
			Vector2 position2 = base.Projectile.Center - Main.screenPosition + distVel * (float)(3 + j);
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(pointTexture, position2, null, val * 0.8f * fade, base.Projectile.rotation, pointTexture.Size() * 0.5f, new Vector2(0.5f * (1f - (float)j * 0.08f) * fade, 0.8f * (1f + (float)j * 0.07f)) * 0.07f, (SpriteEffects)0);
		}
		for (int k = 0; k < 2; k++)
		{
			Vector2 position3 = base.Projectile.Center - Main.screenPosition;
			val = ArsenalEffects.ArsenalLaserColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(bloomTexture, position3, null, val * fade, base.Projectile.rotation, bloomTexture.Size() * 0.5f, new Vector2(0.6f, 1f) * 0.8f, (SpriteEffects)0);
		}
		Vector2 position4 = base.Projectile.Center - Main.screenPosition;
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bloomTexture, position4, null, val * fade, base.Projectile.rotation, bloomTexture.Size() * 0.5f, new Vector2(0.5f, 1f) * 0.65f, (SpriteEffects)0);
		return false;
	}

	public PhalanxSurgeLance()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		startVel = Vector2.Zero;
		base._002Ector();
	}
}
