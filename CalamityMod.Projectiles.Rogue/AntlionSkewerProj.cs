using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AntlionSkewerProj : ModProjectile, ILocalizedModType, IModType
{
	public static int StealthExtraSpit = 4;

	public static float SandBlastDamage = 0.5f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/AntlionSkewer";

	public ref float Time => ref base.Projectile.ai[0];

	public static float TimeToSpit => 15f;

	public static float TimeToAccelerate => 30f;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.localNPCHitCooldown = 20 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		Time++;
		if (Time == TimeToSpit && base.Projectile.owner == Main.myPlayer)
		{
			SoundEngine.PlaySound(base.Projectile.Calamity().stealthStrike ? SoundID.NPCDeath13 : SoundID.Item17, base.Projectile.Center);
			IEntitySource source = base.Projectile.GetSource_FromThis();
			if (base.Projectile.Calamity().stealthStrike)
			{
				for (int i = 0; i < 9; i++)
				{
					float offset = MathHelper.ToRadians(MathHelper.Lerp(-30f, 30f, (float)i / 8f));
					Vector2 spreadVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(offset) * ((i % 2 == 0) ? 6f : 4f);
					Projectile.NewProjectile(source, base.Projectile.Center, spreadVel, ModContent.ProjectileType<AntlionSkewerSandCloud>(), 0, 0f, base.Projectile.owner);
				}
				for (int j = 0; j < StealthExtraSpit; j++)
				{
					Vector2 velocity = base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(24f));
					Projectile.NewProjectile(source, base.Projectile.Center, velocity, ModContent.ProjectileType<AntlionSkewerSandBlast>(), (int)((float)base.Projectile.damage * SandBlastDamage), base.Projectile.knockBack * SandBlastDamage, base.Projectile.owner);
				}
			}
			Projectile.NewProjectile(source, base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<AntlionSkewerSandBlast>(), (int)((float)base.Projectile.damage * SandBlastDamage), base.Projectile.knockBack * SandBlastDamage, base.Projectile.owner);
		}
		if (Time > TimeToSpit && Time <= TimeToSpit + TimeToAccelerate)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.015f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
