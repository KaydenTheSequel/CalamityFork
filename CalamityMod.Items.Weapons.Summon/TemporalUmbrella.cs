using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon.Umbrella;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "BensUmbrella" })]
public class TemporalUmbrella : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 5f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 72;
		base.Item.damage = 193;
		base.Item.knockBack = 1f;
		base.Item.mana = 99;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<MagicHatBuff>();
		base.Item.shoot = ModContent.ProjectileType<MagicHat>();
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.Item68;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.maxMinions >= 5;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		CalamityUtils.KillShootProjectileMany(player, type, ModContent.ProjectileType<MagicArrow>(), ModContent.ProjectileType<MagicHammer>(), ModContent.ProjectileType<MagicAxe>(), ModContent.ProjectileType<MagicUmbrella>(), ModContent.ProjectileType<MagicRifle>());
		Projectile.NewProjectileDirect(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ViridVanguard>().AddIngredient<SarosPossession>().AddIngredient(946)
			.AddIngredient(239)
			.AddIngredient<ShadowspecBar>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
