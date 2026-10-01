using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class NebulashFlail : BaseFlailProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override Color SpecialDrawColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 200, 0);
		}
	}

	public override int ExudeDustType => ModContent.DustType<AstralOrange>();

	public override int WhipDustType => ModContent.DustType<AstralOrange>();

	public override int HandleHeight => 60;

	public override int BodyType1StartY => 64;

	public override int BodyType1SectionHeight => 18;

	public override int BodyType2StartY => 86;

	public override int BodyType2SectionHeight => 18;

	public override int TailStartY => 108;

	public override int TailHeight => 50;

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
		base.Projectile.localNPCHitCooldown = 6;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float useless = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity, 16f * base.Projectile.scale, ref useless))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
		if (base.Projectile.localAI[1] <= 0f && base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center.X, target.Center.Y, 0f, 0f, ModContent.ProjectileType<Nebudust>(), base.Projectile.damage / 2, hit.Knockback, base.Projectile.owner, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
		}
		base.Projectile.localAI[1] = 4f;
	}
}
