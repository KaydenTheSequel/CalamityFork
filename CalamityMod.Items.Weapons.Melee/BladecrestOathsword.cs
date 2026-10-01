using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class BladecrestOathsword : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public int throwCount;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 56;
		base.Item.damage = 42;
		base.Item.knockBack = 3f;
		base.Item.useTime = 53;
		base.Item.useAnimation = 53;
		base.Item.shoot = ModContent.ProjectileType<BladecrestOathswordThrownBlade>();
		base.Item.shootSpeed = 6f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useStyle = 5;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		throwCount++;
		Projectile.NewProjectileDirect(source, player.MountedCenter, velocity, type, damage, knockback, player.whoAmI, 0f, throwCount);
		return false;
	}
}
