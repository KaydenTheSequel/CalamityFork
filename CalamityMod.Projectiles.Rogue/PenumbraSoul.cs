using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PenumbraSoul : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.height = 18;
		base.Projectile.width = 18;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.alpha = 80;
		base.Projectile.extraUpdates = 1;
		base.DrawOffsetX = 1;
		base.DrawOriginOffsetY = 4;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		int trailDust = 1;
		for (int i = 0; i < trailDust; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position - base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 54, 0f, 0f, 0, new Color(38, 30, 43));
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity += base.Projectile.velocity * 0.8f;
		}
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 400f, 12f, 35f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		int dustAmt = Main.rand.Next(30, 41);
		for (int i = 0; i < dustAmt; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 54, 0f, 0f, 0, new Color(38, 30, 43), Main.rand.NextFloat(1f, 1.8f));
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= Main.rand.NextFloat(2f, 3.1f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
