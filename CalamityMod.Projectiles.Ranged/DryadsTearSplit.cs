using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DryadsTearSplit : ModProjectile, ILocalizedModType, IModType
{
	private float speed;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.aiStyle = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 210;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.ArmorPenetration = 10;
		base.AIType = 14;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.ai[2] >= 30f && target.CanBeChasedBy(base.Projectile);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesFromEdge(base.Projectile, 0, lightColor);
		return false;
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[2]++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 264, -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.6f));
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(0.5f, 0.8f);
		dust.color = (Main.rand.NextBool(3) ? Color.MediumAquamarine : Color.Lime);
		if (speed == 0f)
		{
			speed = ((Vector2)(ref base.Projectile.velocity)).Length();
		}
		if (base.Projectile.ai[2] >= 30f)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 500f, speed, 12f);
		}
	}
}
