using CalamityMod.Items.Materials;
using CalamityMod.Projectiles;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SomaPrime : ModItem, ILocalizedModType, IModType
{
	private static readonly float XYInaccuracy = 0.32f;

	public static int AmmoSavedPercent = 80;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 94;
		base.Item.height = 34;
		base.Item.damage = 705;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 5);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.UseSound = SoundID.Item40;
		base.Item.autoReuse = true;
		base.Item.shoot = 242;
		base.Item.shootSpeed = 9f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 26f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-25f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (type == 14)
		{
			type = 242;
			damage += 4;
		}
		velocity.X += Main.rand.NextFloat(0f - XYInaccuracy, XYInaccuracy);
		velocity.Y += Main.rand.NextFloat(0f - XYInaccuracy, XYInaccuracy);
		Vector2 vel = velocity;
		CalamityGlobalProjectile calamityGlobalProjectile = Projectile.NewProjectileDirect(source, position, vel, type, damage, knockback, player.whoAmI).Calamity();
		calamityGlobalProjectile.supercritHits = -1;
		calamityGlobalProjectile.bonusCritDamage++;
		calamityGlobalProjectile.appliesSomaShred = true;
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Kingsbane>().AddIngredient(1255).AddIngredient<ShadowspecBar>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
