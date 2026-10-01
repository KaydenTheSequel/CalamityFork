using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class ForgottenApexWand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 48;
		base.Item.useStyle = 1;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.UseSound = SoundID.Item89;
		base.Item.noMelee = true;
		base.Item.rare = 5;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.autoReuse = true;
		base.Item.knockBack = 4f;
		base.Item.mana = 10;
		base.Item.damage = 28;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.buffType = ModContent.BuffType<AncientMineralSharkBuff>();
		base.Item.shoot = ModContent.ProjectileType<ApexShark>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, ModContent.ProjectileType<ApexShark>(), damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyAdamantiteBar", 5).AddIngredient(3783, 2).AddIngredient(181, 4)
			.AddTile(134)
			.Register();
	}
}
