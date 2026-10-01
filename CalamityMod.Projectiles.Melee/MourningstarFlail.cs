using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MourningstarFlail : BaseFlailProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 7;
		base.Projectile.extraUpdates = 1;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(189, 180);
		if (base.Projectile.localAI[1] <= 0f && base.Projectile.owner == Main.myPlayer)
		{
			int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<FuckYou>(), base.Projectile.damage, hit.Knockback, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			Main.projectile[proj].usesLocalNPCImmunity = false;
			Main.projectile[proj].usesIDStaticNPCImmunity = true;
			Main.projectile[proj].idStaticNPCHitCooldown = 60;
		}
		base.Projectile.localAI[1] = 16f;
	}
}
