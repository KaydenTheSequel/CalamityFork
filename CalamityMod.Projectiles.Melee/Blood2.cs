using CalamityMod.Buffs.DamageOverTime;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Blood2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

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
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 150;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 120 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			for (int i = 0; i < 2; i++)
			{
				int blood = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 5, 0f, 0f, 100);
				Main.dust[blood].noGravity = true;
				Dust obj = Main.dust[blood];
				obj.velocity *= 0f;
			}
		}
		if (base.Projectile.timeLeft < 120)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 450f, 6f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BurningBlood>(), 60);
	}
}
