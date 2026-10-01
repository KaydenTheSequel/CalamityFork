using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SkyfinNuke : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SkyfinBombers";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 720;
		base.Projectile.alpha = 0;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		float speedMult = (base.Projectile.Calamity().stealthStrike ? 9f : 12f);
		if (!base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.ai[0]++;
			if (base.Projectile.ai[0] > 30f)
			{
				NPC target = base.Projectile.Center.ClosestNPCAt(300f);
				if (target != null && target.Bottom.Y < base.Projectile.Top.Y)
				{
					target = null;
				}
				if (target != null)
				{
					Vector2 distNorm = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
					base.Projectile.velocity = (base.Projectile.velocity * 19f + distNorm * speedMult) / 20f;
				}
			}
		}
		else
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 300f, speedMult, 20f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.extraUpdates = 0;
		Vector2 dspeed = default(Vector2);
		for (int i = 0; i < 30; i++)
		{
			((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f));
			Dust.NewDust(base.Projectile.Center, 1, 1, 75, dspeed.X, dspeed.Y, 0, default(Color), 1.1f);
		}
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		int cloudAmt = Main.rand.Next(2, 5);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int c = 0; c < cloudAmt; c++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(50f, 10f, 50f, 0.01f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<SkyBomberGas>(), (int)((double)base.Projectile.damage * 0.4), base.Projectile.knockBack * 0.4f, base.Projectile.owner);
			}
			if (base.Projectile.Calamity().stealthStrike)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DarkOliveGreen, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.7f, 200, UseAdditiveBlend: true, 0.7f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OliveDrab * 0.5f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.02f, 0.05f, 340, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OliveDrab * 0.5f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.03f, 0.07f, 340, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DarkOliveGreen * 0.55f, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.04f, 0.08f, 340, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				int explode = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BileExplosion>(), (int)((double)base.Projectile.damage * 0.4), base.Projectile.knockBack * 0.4f, base.Projectile.owner, 1f);
				Main.projectile[explode].usesLocalNPCImmunity = true;
				Main.projectile[explode].usesIDStaticNPCImmunity = false;
				Main.projectile[explode].localNPCHitCooldown = 30;
			}
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Bottom);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
	}
}
