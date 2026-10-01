using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class EventHorizonBlackhole : ModProjectile, ILocalizedModType, IModType
{
	public int killCounter = 21;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 90;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 25;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.timeLeft < 60)
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		if (base.Projectile.frame == 8)
		{
			return;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.timeLeft > 15)
		{
			if (base.Projectile.frame >= 4)
			{
				base.Projectile.frame = 0;
			}
			return;
		}
		if (base.Projectile.frame < 4)
		{
			base.Projectile.frame = 4;
		}
		if (base.Projectile.frame >= 8)
		{
			base.Projectile.frame = 4;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
