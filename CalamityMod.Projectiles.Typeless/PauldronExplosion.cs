using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class PauldronExplosion : ModProjectile, ILocalizedModType, IModType
{
	private static float ExplosionRadius = 150f;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 150;
		base.Projectile.height = 150;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = AverageDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 240);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, ExplosionRadius, targetHitbox);
	}

	public override bool? CanDamage()
	{
		return base.CanDamage();
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		float minMult = 0.3f;
		int hitsToMinMult = 8;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		target.MoveNPC(launchVel, 15f, ignoreKBImmune: true);
	}

	public override bool? CanCutTiles()
	{
		return false;
	}
}
