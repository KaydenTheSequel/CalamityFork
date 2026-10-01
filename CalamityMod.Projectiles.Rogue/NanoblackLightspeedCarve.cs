using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class NanoblackLightspeedCarve : ModProjectile, ILocalizedModType, IModType
{
	internal const float TargetingRange = 600f;

	internal const int MaxHits = 3;

	internal static float HitboxRadius = 60f;

	internal static float PlacementRandomness = 12f;

	private static int Lifetime = 24;

	private static int HitboxDuration = 9;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override LocalizedText DisplayName => this.GetLocalization(IsPerfect ? "Perfect" : "Standard");

	internal bool IsPerfect => base.Projectile.ai[0] == 1f;

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = (int)(2f * HitboxRadius));
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
	}

	public override void AI()
	{
		if (base.Projectile.timeLeft == Lifetime)
		{
			FrameOneEffects();
		}
	}

	private void FrameOneEffects()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 2)
		{
			int slashVisualID = ModContent.ProjectileType<NanoblackLightspeedCarveSlashVisual>();
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, slashVisualID, 0, 0f, base.Projectile.owner, IsPerfect ? 1f : 0f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.timeLeft > Lifetime - HitboxDuration && base.Projectile.numHits < 3)
		{
			base.Projectile.penetrate++;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, HitboxRadius, targetHitbox);
	}
}
