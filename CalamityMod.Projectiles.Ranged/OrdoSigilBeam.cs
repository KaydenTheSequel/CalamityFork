using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class OrdoSigilBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 5;
		base.Projectile.MaxUpdates = 15;
		base.Projectile.timeLeft = 20 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Time++;
		bool isDrawingUpdate = base.Projectile.numUpdates % 3 == 0;
		if ((Time > 6f) & isDrawingUpdate)
		{
			Color outerSparkColor = default(Color);
			((Color)(ref outerSparkColor))._002Ector(255, 255, 255);
			float scaleBoost = MathHelper.Clamp(Time * 0.03f, 0f, 2f);
			float outerSparkScale = 3.2f + scaleBoost;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 7, outerSparkScale, outerSparkColor));
			Color innerSparkColor = default(Color);
			((Color)(ref innerSparkColor))._002Ector(181, 181, 181);
			float innerSparkScale = 1.6f + scaleBoost;
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 7, innerSparkScale, innerSparkColor));
		}
		if (base.Projectile.FinalExtraUpdate())
		{
			Vector2 center = base.Projectile.Center;
			Color mediumBlue = Color.MediumBlue;
			Lighting.AddLight(center, ((Color)(ref mediumBlue)).ToVector3() * 0.4f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
	}
}
