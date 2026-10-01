using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class RefractionRotorSlashCreator : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

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
		base.Projectile.timeLeft = 45;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void AI()
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.timeLeft % 20 == 19)
		{
			float maxOffset = (float)Target.width * 0.4f;
			if (maxOffset > 300f)
			{
				maxOffset = 300f;
			}
			_ = Vector2.UnitX;
			if (base.Projectile.timeLeft <= 20)
			{
				_ = -Vector2.UnitY;
			}
			Vector2 spawnOffset = SlashDirection.ToRotationVector2();
			spawnOffset *= Main.rand.NextFloatDirection() * maxOffset;
			Vector2 sliceVelocity = spawnOffset.SafeNormalize(Vector2.UnitY) * 0.1f;
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center + spawnOffset, sliceVelocity, ModContent.ProjectileType<RefractionSlash>(), (int)((double)base.Projectile.damage * 0.5), 0f, base.Projectile.owner);
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
