using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "PrideHuntersPlanarRipper" })]
public class PridefulHuntersPlanarRipper : ModItem, ILocalizedModType, IModType
{
	private int counter;

	public static int AmmoSavedPercent = 50;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 68;
		base.Item.height = 32;
		base.Item.damage = 79;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 5;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item11;
		base.Item.shoot = 14;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.shootSpeed = 15f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.Calamity().donorItem = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-12f, -6f);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (type == 14)
		{
			type = ModContent.ProjectileType<PlanarRipperBolt>();
		}
		counter++;
		if (counter == 4)
		{
			damage = (int)((float)damage * 1.35f);
			counter = 0;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<P90>().AddIngredient(1265).AddIngredient(3467, 5)
			.AddIngredient(3456, 10)
			.AddTile(134)
			.Register();
	}
}
