using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Metaballs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class GalaxySmasherBlast : ModProjectile, ILocalizedModType, IModType
{
	private static float ExplosionRadius = 868f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 868;
		base.Projectile.height = 868;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 18;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		float fade = Utils.GetLerpValue(-2f, 18f, base.Projectile.timeLeft);
		float numberOfDusts = 8f;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			Vector2 velOffset = Utils.RotatedByRandom(new Vector2(42f, 42f), 100.0) * Main.rand.NextFloat(0.5f, 1f);
			GalaxyMetaball.SpawnParticle(base.Projectile.Center, velOffset * fade, 300f * Main.rand.NextFloat(0.8f, 1f) * fade);
			if (i % 2 == 0)
			{
				Vector2 velOffset2 = Utils.RotatedByRandom(new Vector2(92f, 92f), 100.0) * Main.rand.NextFloat(0.5f, 1f);
				GalaxyMetaball.SpawnParticle(base.Projectile.Center, velOffset2 * fade, 120f * Main.rand.NextFloat(0.7f, 1f) * fade);
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 180);
		float minMult = 0.15f;
		int hitsToMinMult = 12;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, ExplosionRadius, targetHitbox);
	}
}
