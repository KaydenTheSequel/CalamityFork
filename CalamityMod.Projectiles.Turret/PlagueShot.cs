using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class PlagueShot : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 110;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.hide = true;
	}

	public override bool PreAI()
	{
		if (base.Projectile.knockBack == 0f)
		{
			base.Projectile.hostile = true;
		}
		else
		{
			base.Projectile.friendly = true;
		}
		return true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundStyle style = SoundID.Item61 with
			{
				Volume = 0.3f
			};
			SoundEngine.PlaySound(in style, base.Projectile.position);
		}
		else
		{
			base.Projectile.hide = false;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.friendly)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 180f, 12f, 0f);
		}
		base.Projectile.velocity = (base.Projectile.oldVelocity * 7f + base.Projectile.velocity) / 8f;
		DrawParticles();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 60);
		if (!base.Projectile.hostile || Main.netMode != 1)
		{
			base.Projectile.Kill();
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	public void DrawParticles()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		if (!(base.Projectile.localAI[0] < 3f))
		{
			Vector2 bloodSpawnPosition = base.Projectile.Center + (Vector2.UnitY * 13f).RotatedBy(base.Projectile.rotation);
			Vector2 spinninpoint = -(base.Projectile.Center - bloodSpawnPosition).SafeNormalize(Vector2.UnitY);
			int bloodLifetime = Main.rand.Next(8, 13);
			float bloodScale = Main.rand.NextFloat(0.6f, 0.9f);
			Color bloodColor = Color.Lerp(Color.Green, Color.DarkGreen, Main.rand.NextFloat());
			bloodColor = Color.Lerp(bloodColor, new Color(55, 125, 11), Main.rand.NextFloat(0.65f));
			if (Main.rand.NextBool(20))
			{
				bloodScale *= 1.7f;
			}
			Vector2 bloodVelocity = spinninpoint.RotatedByRandom(0.8100000023841858) * Main.rand.NextFloat(3f, 6f);
			bloodVelocity.Y -= 0.5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(bloodSpawnPosition, bloodVelocity, bloodLifetime, bloodScale, bloodColor));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PlagueBoom", 4);
		style.Volume = 0.5f;
		style.Pitch = -0.3f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.netMode != 1)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0.4f, ModContent.ProjectileType<PlagueExplosionGas>(), (int)((float)base.Projectile.damage * 0.25f), base.Projectile.knockBack * 0.16f, Main.myPlayer);
		}
	}
}
