using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class StellarTorusBeam : BaseLaserbeamProjectile, ILocalizedModType, IModType
{
	public float Opacity = 0.33f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override Texture2D LaserBeginTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/StellarTorusBeamStart", (AssetRequestMode)1).Value;

	public override Texture2D LaserMiddleTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/StellarTorusBeamMiddle", (AssetRequestMode)1).Value;

	public override Texture2D LaserEndTexture => ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/StellarTorusBeamEnd", (AssetRequestMode)1).Value;

	public override float Lifetime => StellarTorusStaff.TimeShooting;

	public override float MaxLaserLength => StellarTorusStaff.EnemyDetectionDistance * 2f;

	public override float MaxScale => 1f;

	public override Color LightCastColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return Color.White * Opacity;
		}
	}

	public override Color LaserOverlayColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return Color.White * Opacity;
		}
	}

	public ref float MinionID => ref base.Projectile.ai[1];

	public Projectile Minion => Main.projectile[(int)MinionID];

	public ref float TargetID => ref base.Projectile.ai[2];

	public NPC Target => Main.npc[(int)TargetID];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.localNPCHitCooldown = StellarTorusStaff.IFrames;
		base.Projectile.timeLeft = (int)Lifetime;
		base.Projectile.penetrate = -1;
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.alpha = 254;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
	}

	public override void ExtraBehavior()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Minion.rotation.ToRotationVector2();
		if (Target == null)
		{
			base.Projectile.Kill();
		}
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Vector2.Lerp(base.Projectile.velocity, base.Projectile.velocity * MaxLaserLength, Main.rand.NextFloat(1f)) + Main.rand.NextVector2Circular(30f, 30f), base.Projectile.velocity * Main.rand.NextFloat(8f, 12f), affectedByGravity: false, 20, Main.rand.NextFloat(1f, 1.5f), Color.Cyan));
		}
		Vector2 center = base.Projectile.Center;
		Vector2 velocity = Minion.velocity;
		Color cyan = Color.Cyan;
		((Color)(ref cyan)).A = 6;
		GeneralParticleHandler.SpawnParticle(new GenericBloom(center, velocity, cyan, 0.6f, (int)(Lifetime / 4f)));
	}

	public override void AttachToSomething()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Minion.Center;
		if (Minion == null)
		{
			base.Projectile.Kill();
			base.Projectile.netUpdate = true;
		}
	}
}
