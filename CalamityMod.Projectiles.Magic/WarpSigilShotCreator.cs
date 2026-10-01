using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class WarpSigilShotCreator : ModProjectile, ILocalizedModType, IModType
{
	private const int DelayBetweenShots = 5;

	private const float ShotSpeed = 20f;

	private const float SpawnDistance = 250f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float FiringTimer => ref base.Projectile.ai[0];

	public ref float ParentIndex => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		Projectile parent = Main.projectile[(int)ParentIndex];
		if (parent == null || !parent.active || parent.type != ModContent.ProjectileType<WarpSigil>())
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.Center = parent.Center;
		base.Projectile.timeLeft = parent.timeLeft;
		Vector2 targetCenter = Main.MouseWorld;
		FiringTimer++;
		if (FiringTimer % 5f == 0f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/UnstableCastersGauntlet/VisNeedleFire");
			style.Volume = 0.35f;
			style.Pitch = -0.3f;
			style.PitchVariance = 0.1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Vector2 fixedTargetOffset = Main.rand.NextVector2Circular(36f, 36f);
			Vector2 spawnOffset = Main.rand.NextFloat((float)Math.PI * 2f).ToRotationVector2() * 250f;
			Vector2 spawnPosition = targetCenter + spawnOffset;
			Vector2 velocity = (targetCenter + fixedTargetOffset - spawnPosition).SafeNormalize(Vector2.UnitX) * 20f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<WarpSigilShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, fixedTargetOffset.X, fixedTargetOffset.Y);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}
}
