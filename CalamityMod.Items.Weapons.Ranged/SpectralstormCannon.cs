using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class SpectralstormCannon : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 33;

	public int BuiltUpHeat;

	public const int OverheatLevel = 540;

	public const int OverheatCooldown = 160;

	public const int OverheatDamage = 50;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 26;
		base.Item.damage = 85;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = (base.Item.useAnimation = 11);
		base.Item.knockBack = 1.5f;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.shoot = ModContent.ProjectileType<SpectralstormCannonHoldout>();
		base.Item.shootSpeed = 10f;
		base.Item.useAmmo = AmmoID.Flare;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().rightClickListener = true;
		if (player.ownedProjectileCounts[base.Item.shoot] > 0 && !Main.mouseLeft && player.Calamity().flareGunOverheat == 0 && BuiltUpHeat > 0)
		{
			BuiltUpHeat -= 3;
			if (BuiltUpHeat < 0)
			{
				BuiltUpHeat = 0;
			}
		}
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return !player.Calamity().mouseRight;
		}
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		type = base.Item.shoot;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FirestormCannon>().AddIngredient(3458, 6).AddIngredient(1508, 10)
			.AddTile(412)
			.Register();
	}
}
