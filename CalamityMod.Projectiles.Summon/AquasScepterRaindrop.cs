using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AquasScepterRaindrop : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 1;
	}

	public sealed override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 48;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 8;
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		base.Projectile.damage = (int)((float)base.Projectile.damage * 0.6f);
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft <= 8)
		{
			base.Projectile.Opacity -= 0.125f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.92f;
		}
	}
}
