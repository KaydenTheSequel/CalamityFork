using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VividLaser2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 120;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		Vector2 value7 = default(Vector2);
		((Vector2)(ref value7))._002Ector(5f, 10f);
		base.Projectile.ai[1]++;
		for (int dust = 0; dust < 2; dust++)
		{
			Vector2 dustPosOffset = Vector2.UnitX * -12f;
			dustPosOffset = -Vector2.UnitY.RotatedBy(base.Projectile.ai[1] * ((float)Math.PI / 24f) + (float)dust * (float)Math.PI) * value7 - base.Projectile.rotation.ToRotationVector2() * 10f;
			int exo = Dust.NewDust(base.Projectile.Center, 0, 0, 66, 0f, 0f, 160, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
			Main.dust[exo].scale = 0.75f;
			Main.dust[exo].noGravity = true;
			Main.dust[exo].position = base.Projectile.Center + dustPosOffset;
			Main.dust[exo].velocity = base.Projectile.velocity;
		}
		if (base.Projectile.timeLeft < 110)
		{
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[0] >= 1f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 400f, 12f, 20f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			int randomDust = Utils.SelectRandom<int>(Main.rand, 107, 234, 269);
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, randomDust, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.timeLeft >= 110)
		{
			return false;
		}
		return null;
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.timeLeft < 110;
	}
}
