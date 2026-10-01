using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "MepheticSprayer" })]
public class BlightSpewer : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle Nanomachines = new SoundStyle("CalamityMod/Sounds/Item/NanoSwarm")
	{
		PitchVariance = 0.45f,
		Volume = 0.4f
	};

	public static int AmmoSavedPercent = 33;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 76;
		base.Item.height = 36;
		base.Item.damage = 52;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 19;
		base.Item.useAnimation = 19;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.UseSound = SoundID.Item34;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BlightFlames>();
		base.Item.shootSpeed = 7f;
		base.Item.useAmmo = AmmoID.Gel;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SparkSpreader>().AddIngredient<InfectedArmorPlating>(5).AddIngredient<PlagueCellCanister>(10)
			.AddTile(134)
			.Register();
	}
}
