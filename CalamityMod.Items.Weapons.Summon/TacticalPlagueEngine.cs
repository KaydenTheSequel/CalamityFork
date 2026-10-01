using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class TacticalPlagueEngine : ModItem, ILocalizedModType, IModType
{
	public const int BulletShootRate = 125;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.damage = 140;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 0.5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = SoundID.Item14;
		base.Item.autoReuse = true;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<TacticalPlagueEngineBuff>();
		base.Item.shoot = ModContent.ProjectileType<TacticalPlagueJet>();
		base.Item.shootSpeed = 7f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI, 0f, 1f).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlackHawkRemote>().AddIngredient<FuelCellBundle>().AddIngredient(3467, 5)
			.AddTile(134)
			.Register();
	}
}
