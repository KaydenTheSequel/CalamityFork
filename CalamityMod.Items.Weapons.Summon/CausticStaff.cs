using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class CausticStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 28;
		base.Item.mana = 10;
		base.Item.damage = 15;
		base.Item.useStyle = 1;
		base.Item.buffType = ModContent.BuffType<CausticStaffBuff>();
		base.Item.shoot = ModContent.ProjectileType<CausticStaffSummon>();
		base.Item.UseSound = SoundID.Item77;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.Calamity().donorItem = true;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.autoReuse = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 1f).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyEvilBar", 10).AddRecipeGroup("CursedFlameIchor", 10).AddIngredient(521, 10)
			.AddTile(26)
			.Register();
	}
}
