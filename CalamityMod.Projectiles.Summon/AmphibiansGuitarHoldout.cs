using System;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AmphibiansGuitarHoldout : ModProjectile, ILocalizedModType, IModType
{
	private static readonly int MinionType = ModContent.ProjectileType<AmphibiansGuitarMinion>();

	public override string LocalizationCategory => "Projectiles.Summon";

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<AmphibiansGuitar>();

	private Player Owner => Main.player[base.Projectile.owner];

	private ref float SpawnTimer => ref base.Projectile.ai[0];

	private static float ArmSwing => (float)Math.PI / 4f + Utils.Remap(MathF.Sin(Main.GlobalTimeWrappedHourly * 6.7f), -1f, 1f, 0f - MathHelper.ToRadians(15f), MathHelper.ToRadians(15f));

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 50);
		base.Projectile.tileCollide = false;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		SpawnTimer++;
		if (SpawnTimer > 19f && Owner.ownedProjectileCounts[MinionType] < 8 && Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.MountedCenter, Vector2.Zero, MinionType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, Owner.ownedProjectileCounts[MinionType]);
			SpawnTimer = 0f;
		}
		ManageHoldout();
	}

	private void ManageHoldout()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.CantUseHoldout())
		{
			base.Projectile.Kill();
		}
		Vector2 armPosition = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
		Vector2 ownerToMouse = Owner.Calamity().mouseWorld - armPosition;
		float holdoutDirection = base.Projectile.velocity.ToRotation();
		int direction = MathF.Sign(ownerToMouse.X);
		Vector2 lengthOffset = base.Projectile.rotation.ToRotationVector2() * 15f;
		base.Projectile.Center = armPosition + lengthOffset;
		base.Projectile.velocity = holdoutDirection.AngleTowards(ownerToMouse.ToRotation(), 0.2f).ToRotationVector2();
		base.Projectile.rotation = holdoutDirection;
		base.Projectile.spriteDirection = direction;
		Owner.ChangeDir(direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = (Owner.itemAnimation = 2);
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		float armRotation = (base.Projectile.rotation - (float)Math.PI / 2f) * Owner.gravDir + ((Owner.gravDir == -1f) ? ((float)Math.PI) : 0f);
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.ThreeQuarters, armRotation + ArmSwing * (float)direction);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armRotation);
		base.Projectile.timeLeft = 2;
		base.Projectile.ForceNetUpdate();
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(position: base.Projectile.Center - Main.screenPosition, rotation: base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f), origin: value.Size() * 0.5f, effects: (SpriteEffects)((float)base.Projectile.spriteDirection * Owner.gravDir == -1f), texture: value, sourceRectangle: null, color: base.Projectile.GetAlpha(lightColor), scale: base.Projectile.scale * Owner.gravDir);
		return false;
	}
}
