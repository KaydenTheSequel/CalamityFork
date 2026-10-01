using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Wingman : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 42;
		base.Item.damage = 82;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 5;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 25f;
		base.Item.shoot = ModContent.ProjectileType<WingmanHoldout>();
		base.Item.channel = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] < 2;
	}

	public override void HoldItem(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.mouseRotationListener = true;
		calamityPlayer.rightClickListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<WingmanHoldout>(), damage, knockback, player.whoAmI, 0f, 0f, (i == 0) ? 1 : (-1)).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		}
		return false;
	}
}
