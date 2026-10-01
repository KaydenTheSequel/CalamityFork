using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TotalMeltdown : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 13;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 120;
		base.Projectile.height = 122;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Main.projFrames[base.Type] * 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		if ((float)base.Projectile.timeLeft % 5f == 4f)
		{
			base.Projectile.frame++;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(323, 300);
	}
}
