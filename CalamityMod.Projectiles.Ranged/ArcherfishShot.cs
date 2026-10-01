using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ArcherfishShot : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 2;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 600;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(1f, 1f), 411);
			gore.timeLeft = 9 + Main.rand.Next(7);
			gore.scale = Main.rand.NextFloat(0.6f, 1f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
		for (int i = 0; i < 6; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 211, 0f, 0f, 100);
			dust.noGravity = true;
			dust.velocity = base.Projectile.velocity * 0.5f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(60f)) * 0.3f, 411);
			gore.timeLeft = 9 + Main.rand.Next(7);
			gore.scale = Main.rand.NextFloat(0.6f, 1f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(103, 120);
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(103, 120);
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 120);
	}
}
