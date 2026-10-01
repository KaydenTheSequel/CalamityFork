using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class TarraEnergy : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 200;
		base.Projectile.extraUpdates = 1;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 170 && target.CanBeChasedBy(base.Projectile);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		int cap = 2;
		float capDamageFactor = 0.05f;
		int excessCount = Main.player[base.Projectile.owner].ownedProjectileCounts[base.Type] - cap;
		modifiers.SourceDamage *= MathHelper.Clamp(1f - capDamageFactor * (float)excessCount, 0f, 1f);
	}

	public override void AI()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			float x2 = base.Projectile.position.X - base.Projectile.velocity.X / 10f * (float)i;
			float y2 = base.Projectile.position.Y - base.Projectile.velocity.Y / 10f * (float)i;
			Vector2 dspeed = base.Projectile.velocity * Main.rand.NextFloat(0.7f, 0.4f);
			int greenDust = Dust.NewDust(new Vector2(x2, y2), 1, 1, 107);
			Main.dust[greenDust].alpha = base.Projectile.alpha;
			Main.dust[greenDust].position.X = x2;
			Main.dust[greenDust].position.Y = y2;
			Main.dust[greenDust].velocity = dspeed;
			Main.dust[greenDust].noGravity = true;
			Main.dust[greenDust].noLight = true;
		}
		if (base.Projectile.timeLeft < 170)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 600f, 9f, 20f);
		}
	}
}
