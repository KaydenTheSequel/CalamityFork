using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PerditoSigilShotCreator : ModProjectile, ILocalizedModType, IModType
{
	private const int TotalShots = 13;

	private const int DelayBetweenShots = 4;

	private const float ShotSpeed = 17f;

	private const float SpawnRadius = 150f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float FiringTimer => ref base.Projectile.ai[0];

	public ref float ShotsFiredCount => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = 1;
		base.Projectile.height = 1;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 62;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Main.MouseWorld;
		Vector2 targetCenter = base.Projectile.Center;
		FiringTimer++;
		if (FiringTimer % 4f == 0f && ShotsFiredCount < 13f)
		{
			ShotsFiredCount++;
			if (ShotsFiredCount == 13f)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/UnstableCastersGauntlet/PerditoSigilHit2");
				style.Volume = 0.9f;
				style.PitchVariance = 0.1f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			else
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/UnstableCastersGauntlet/PerditoSigilHit1");
				style.Volume = 0.8f;
				style.PitchVariance = 0.1f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			Vector2 spawnOffset = Main.rand.NextFloat((float)Math.PI * 2f).ToRotationVector2() * 150f;
			Vector2 spawnPosition = targetCenter + spawnOffset;
			Vector2 velocity = (targetCenter - spawnPosition).SafeNormalize(Vector2.UnitX) * 17f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, velocity, ModContent.ProjectileType<PerditoSigilShot>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (ShotsFiredCount >= 13f)
		{
			base.Projectile.Kill();
		}
	}
}
