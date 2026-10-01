using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ArtAttackHoldout : ModProjectile
{
	public const float AimResponsiveness = 0.72f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<ArtAttack>();

	public Player Owner => Main.player[base.Projectile.owner];

	public override string Texture => "CalamityMod/Items/Weapons/Magic/ArtAttack";

	public override void SetDefaults()
	{
		base.Projectile.width = 70;
		base.Projectile.height = 70;
		base.Projectile.friendly = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 90000;
	}

	public override void AI()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		UpdatePlayerVisuals();
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		UpdateAim();
		int attackType = ModContent.ProjectileType<ArtAttackStar>();
		if (Owner.ownedProjectileCounts[attackType] == 0)
		{
			if (base.Projectile.ai[0] >= 0f && Owner.CheckMana(Owner.HeldItem, -1, pay: true))
			{
				SoundEngine.PlaySound(in ArtAttack.UseSound, Owner.Center);
				Vector2 initialStarVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 15f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.ClampedMouseWorld(), initialStarVelocity, attackType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				base.Projectile.ai[0] = -24f;
			}
			else
			{
				base.Projectile.ai[0]++;
			}
		}
		if (!Owner.channel)
		{
			base.Projectile.Kill();
		}
	}

	private void UpdatePlayerVisuals()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.Center = Owner.RotatedRelativePoint(Owner.MountedCenter);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = CalamityUtils.WrapAngle90Degrees(base.Projectile.rotation);
	}

	private void UpdateAim()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		Vector2 aimOffset = base.Projectile.SafeDirectionTo(Main.MouseWorld, -Vector2.UnitY);
		aimOffset = Vector2.Lerp(aimOffset, Vector2.Normalize(base.Projectile.velocity), 0.72f).SafeNormalize(Vector2.UnitY) * 30f;
		if (aimOffset != base.Projectile.velocity)
		{
			base.Projectile.ForceNetUpdate();
		}
		base.Projectile.velocity = aimOffset;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
