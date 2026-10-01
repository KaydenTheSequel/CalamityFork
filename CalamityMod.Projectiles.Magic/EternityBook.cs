using System;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.NPCs.Providence;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class EternityBook : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Eternity>();

	public float Time
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player.CantUseHoldout())
		{
			base.Projectile.Kill();
			return;
		}
		Time++;
		if (Main.myPlayer == base.Projectile.owner && Time == 1f)
		{
			NPC target = player.ClampedMouseWorld().ClosestNPCAt(4400f, ignoreTiles: true, bossPriority: true);
			if (target != null)
			{
				SoundEngine.PlaySound(in Providence.HolyRaySound);
				SummonProjectilesOnTarget(target, player);
			}
		}
		base.Projectile.localAI[0] += Utils.Remap(Time, 0f, 200f, 1f, 5f);
		base.Projectile.frame = (int)Math.Round(base.Projectile.localAI[0] / 10f) % Main.projFrames[base.Type];
		if (base.Projectile.localAI[0] >= (float)Main.projFrames[base.Type] * 10f)
		{
			base.Projectile.localAI[0] = 0f;
		}
		AdjustPlayerValues(player);
		base.Projectile.Center = player.Center + (player.compositeFrontArm.rotation + (float)Math.PI / 2f).ToRotationVector2() * 14f;
	}

	public void AdjustPlayerValues(Player player)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = player.direction);
		base.Projectile.timeLeft = 2;
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = ((float)base.Projectile.direction * base.Projectile.velocity).ToRotation();
		float frontArmRotation = 1.1107963f * (float)(-player.direction);
		float backArmRotation = frontArmRotation + MathHelper.Lerp(0.12f, 1.1f, CalamityUtils.Convert01To010(base.Projectile.localAI[0] / (float)Main.projFrames[base.Type] / 10f)) * (float)(-player.direction);
		player.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, backArmRotation);
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, frontArmRotation);
	}

	public void SummonProjectilesOnTarget(NPC target, Player owner)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<EternityHex>(), base.Projectile.damage, 0f, owner.whoAmI, target.whoAmI).localAI[1] = base.Projectile.whoAmI;
		for (int i = 0; i < 5; i++)
		{
			float crystalAngleOffset = (float)Math.PI * 2f / 5f * (float)i;
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<EternityCrystal>(), 0, 0f, owner.whoAmI, target.whoAmI, crystalAngleOffset);
			projectile.frame = i % 2;
			projectile.localAI[1] = base.Projectile.whoAmI;
		}
		for (int j = 0; j < 10; j++)
		{
			float circleOffset = (float)Math.PI / 5f * (float)j;
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<EternityCircle>(), 0, 0f, owner.whoAmI, target.whoAmI, circleOffset).localAI[1] = base.Projectile.whoAmI;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
