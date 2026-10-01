using System;
using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BloodBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.35f, 0f, 0f);
		if (base.Projectile.position.Y > Main.player[base.Projectile.owner].position.Y - 160f)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.ai[0] > 7f)
		{
			float scalar = 1f;
			if (base.Projectile.ai[0] == 8f)
			{
				scalar = 0.25f;
			}
			else if (base.Projectile.ai[0] == 9f)
			{
				scalar = 0.5f;
			}
			else if (base.Projectile.ai[0] == 10f)
			{
				scalar = 0.75f;
			}
			base.Projectile.ai[0]++;
			int dustType = 5;
			int blood = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 100);
			Dust dust = Main.dust[blood];
			if (Main.rand.NextBool(3))
			{
				dust.noGravity = true;
				dust.scale *= 2f;
				dust.velocity.X *= 2f;
				dust.velocity.Y *= 2f;
			}
			dust.velocity.X *= 1.2f;
			dust.velocity.Y *= 1.2f;
			dust.scale *= scalar;
		}
		else
		{
			base.Projectile.ai[0]++;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 120);
	}
}
