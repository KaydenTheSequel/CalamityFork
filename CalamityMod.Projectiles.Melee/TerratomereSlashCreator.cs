using System;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TerratomereSlashCreator : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public NPC Target => Main.npc[(int)base.Projectile.ai[0]];

	public float SlashDirection
	{
		get
		{
			if (base.Projectile.ai[1] > (float)Math.PI)
			{
				return Main.rand.NextFloatDirection();
			}
			return base.Projectile.ai[1] + Main.rand.NextFloatDirection() * 0.2f;
		}
	}

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.ignoreWater = false;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 36;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft % 9 != 0)
		{
			return;
		}
		SoundEngine.PlaySound(in CommonCalamitySounds.SwiftSliceSound, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			float maxOffset = (float)Target.width * 0.4f;
			if (maxOffset > 300f)
			{
				maxOffset = 300f;
			}
			Vector2 spawnOffset = SlashDirection.ToRotationVector2() * Main.rand.NextFloatDirection() * maxOffset;
			Vector2 sliceVelocity = spawnOffset.SafeNormalize(Vector2.UnitY) * 0.1f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Target.Center + spawnOffset, sliceVelocity, ModContent.ProjectileType<TerratomereSlash>(), (int)((float)base.Projectile.damage * 0.4f), 0f, base.Projectile.owner);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
