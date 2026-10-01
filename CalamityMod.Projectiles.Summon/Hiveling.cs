using System;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class Hiveling : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/NPCs/Astral/Glomerling";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 240;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f + (float)Math.PI / 4f;
		base.Projectile.spriteDirection = -base.Projectile.direction;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.05f;
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1200f, Main.player[base.Projectile.owner]);
		if (potentialTarget != null)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 30f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * 10f) / 31f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		int dye = Main.player[base.Projectile.owner]?.cMinion ?? 0;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
		return false;
	}
}
