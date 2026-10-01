using System;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SulphuricAcidCannon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 90;
		base.Item.height = 30;
		base.Item.damage = 550;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 35;
		base.Item.useAnimation = 35;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.UseSound = SoundID.Item73;
		base.Item.useAmmo = AmmoID.Rocket;
		base.Item.shoot = ModContent.ProjectileType<AcidRocket>();
		base.Item.shootSpeed = 16f;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.UnitX * -15f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Vector2 newPos = position + Utils.RotatedBy(new Vector2(78f, (float)player.direction * ((Math.Abs(velocity.SafeNormalize(Vector2.Zero).X) < 0.02f) ? (-2f) : (-8f))), (double)velocity.ToRotation(), default(Vector2));
		Vector2 newVel = velocity.RotatedByRandom(MathHelper.ToRadians(10f));
		player.PickAmmo(player.HeldItem, out var _, out var _, out var _, out var _, out var rocketType);
		Projectile.NewProjectile(source, newPos, newVel, base.Item.shoot, damage, knockback, player.whoAmI, rocketType);
		return false;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		float itemRotation = player.compositeFrontArm.rotation + (float)Math.PI / 2f * player.gravDir;
		float pullback = 7f;
		float animProgress = 0.5f - (float)player.itemTime / (float)player.itemTimeMax;
		(player.Center - player.Calamity().mouseWorld).ToRotation();
		_ = player.gravDir;
		if (animProgress < 0.4f)
		{
			pullback -= 2.75f * (float)Math.Pow((0.6f - animProgress) / 0.6f, 2.0);
		}
		Vector2 itemPosition = player.MountedCenter + itemRotation.ToRotationVector2() * pullback;
		Vector2 itemSize = default(Vector2);
		((Vector2)(ref itemSize))._002Ector(52f, 28f);
		Vector2 itemOrigin = default(Vector2);
		((Vector2)(ref itemOrigin))._002Ector(-24f, 4f);
		CalamityUtils.CleanHoldStyle(player, itemRotation, itemPosition, itemSize, itemOrigin);
		base.UseStyle(player, heldItemFrame);
	}

	public override void UseItemFrame(Player player)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		player.ChangeDir(Math.Sign((player.Calamity().mouseWorld - player.Center).X));
		_ = (float)player.itemTime / (float)player.itemTimeMax;
		float rotation = (player.Center - player.Calamity().mouseWorld).ToRotation() * player.gravDir + (float)Math.PI / 2f;
		player.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, rotation);
	}
}
