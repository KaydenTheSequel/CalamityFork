using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class HauntedScroll : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		Main.RegisterItemAnimation(base.Type, new DrawAnimationVertical(6, 6));
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 48;
		base.Item.damage = 25;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.UseSound = SoundID.Item60;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<HauntedDishesBuff>();
		base.Item.shoot = ModContent.ProjectileType<HauntedDishes>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 30f).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("Wood", 10).AddIngredient(520, 5).AddIngredient(521, 5)
			.AddIngredient(4326, 3)
			.AddTile(16)
			.Register();
	}
}
