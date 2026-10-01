using CalamityMod.Buffs.Summon;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class WarloksMoonFist : ModItem, ILocalizedModType, IModType
{
	public const int SlotCount = 4;

	public const int PunchCooldownTime = 36;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 4f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 50;
		base.Item.damage = 450;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = 11;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.UseSound = SoundID.Item104;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.buffType = ModContent.BuffType<MoonFistBuff>();
		base.Item.shoot = ModContent.ProjectileType<MoonFist>();
		base.Item.DamageType = DamageClass.Summon;
		base.Item.Calamity().donorItem = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		int existingFists = player.ownedProjectileCounts[type];
		Projectile projectile = Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
		projectile.originalDamage = base.Item.damage;
		projectile.ai[0] = existingFists;
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1297).AddIngredient<Lumenyl>(10).AddIngredient<RuinousSoul>(5)
			.AddIngredient<ExodiumCluster>(5)
			.AddTile(134)
			.Register();
	}
}
