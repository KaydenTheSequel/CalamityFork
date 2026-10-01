using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class FetidEmesis : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 66;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 76;
		base.Item.height = 46;
		base.Item.damage = 214;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 6);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.UseSound = null;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<FetidEmesisHoldout>();
		base.Item.shootSpeed = 16f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<FetidEmesisHoldout>(), damage, knockback, player.whoAmI).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}
}
