using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ShadeNimbusCloud : ModProjectile, ILocalizedModType, IModType
{
	public bool StartFading;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = 300;
		base.Projectile.width = (base.Projectile.height = 28);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.95f;
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 8 % Main.projFrames[base.Type];
		if (Collision.SolidCollision(base.Projectile.Center, base.Projectile.width, base.Projectile.height) || base.Projectile.timeLeft < 127)
		{
			StartFading = true;
		}
		if (StartFading)
		{
			base.Projectile.alpha += 2;
		}
		base.Projectile.netUpdate = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int dustIndex = 0; dustIndex < 40; dustIndex++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14, 0f, 0f, 0, default(Color), 0.5f);
		}
		target.AddBuff(ModContent.BuffType<BrainRot>(), 120);
	}
}
