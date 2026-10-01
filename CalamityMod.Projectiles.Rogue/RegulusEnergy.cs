using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RegulusEnergy : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 360;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		Timer++;
		if (Timer % 2f == 0f)
		{
			int dustType = ((Timer % 4f == 0f) ? ModContent.DustType<AstralBlue>() : ModContent.DustType<AstralOrange>());
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width - 28, base.Projectile.height - 28, dustType, 0f, 0f, 100, default(Color), 1.5f);
			dust.noGravity = true;
			dust.velocity *= 0.1f;
			dust.velocity += base.Projectile.velocity * 0.5f;
		}
		if (Timer < 60f)
		{
			base.Projectile.velocity.X *= 0.98f;
			base.Projectile.velocity.Y *= 0.98f;
		}
		else
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 500f, 12f, 20f);
		}
	}

	public override bool? CanDamage()
	{
		if (!(Timer >= 60f))
		{
			return false;
		}
		return null;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		int dustAmt = 18;
		for (int j = 0; j < dustAmt; j++)
		{
			float dustRotation = (float)Math.PI / (float)dustAmt * (float)j;
			Vector2 dustVel = -Vector2.UnitY.RotatedBy(dustRotation) * (float)(Math.Sin(dustRotation) + 5.0) * 2f;
			Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<AstralOrange>(), dustVel, 50, default(Color), 0.8f).noGravity = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 60);
	}
}
