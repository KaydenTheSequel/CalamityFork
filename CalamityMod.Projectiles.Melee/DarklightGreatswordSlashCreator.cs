using System;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DarklightGreatswordSlashCreator : ModProjectile, ILocalizedModType, IModType
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
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft % 18 != 0)
		{
			return;
		}
		SoundStyle style = CommonCalamitySounds.SwiftSliceSound with
		{
			Volume = CommonCalamitySounds.SwiftSliceSound.Volume * 0.5f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner)
		{
			float maxOffset = (float)Target.width * 0.4f;
			if (maxOffset > 300f)
			{
				maxOffset = 300f;
			}
			Vector2 spawnOffset = SlashDirection.ToRotationVector2() * Main.rand.NextFloatDirection() * maxOffset;
			Vector2 sliceVelocity = spawnOffset.SafeNormalize(Vector2.UnitY) * 0.1f;
			int damage = base.Projectile.damage;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Target.Center + spawnOffset, sliceVelocity, ModContent.ProjectileType<DarklightGreatswordSlash>(), damage, 0f, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2]);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
