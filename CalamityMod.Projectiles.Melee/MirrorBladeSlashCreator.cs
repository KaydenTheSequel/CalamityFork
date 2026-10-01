using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MirrorBladeSlashCreator : ModProjectile, ILocalizedModType, IModType
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
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft % 18 == 0 && Main.myPlayer == base.Projectile.owner)
		{
			float maxOffset = (float)Target.width * 0.4f;
			if (maxOffset > 300f)
			{
				maxOffset = 300f;
			}
			Vector2 spawnOffset = SlashDirection.ToRotationVector2() * Main.rand.NextFloatDirection() * maxOffset;
			Vector2 sliceVelocity = spawnOffset.SafeNormalize(Vector2.UnitY) * 0.1f;
			int damage = base.Projectile.damage;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Target.Center + spawnOffset, sliceVelocity, ModContent.ProjectileType<MirrorBladeSlash>(), damage, 0f, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2]);
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
