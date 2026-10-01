using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Eternity : ModItem, ILocalizedModType, IModType
{
	public const int BaseDamage = 840;

	public const int ExplosionDamage = 8400;

	public const int MaxHomers = 40;

	public const int DustID = 16;

	public static readonly Color BlueColor;

	public static readonly Color PinkColor;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 40;
		base.Item.damage = 840;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 30;
		base.Item.useAnimation = (base.Item.useTime = 120);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<EternityBook>();
		base.Item.shootSpeed = 0f;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip1");
		if (line != null)
		{
			line.OverrideColor = new Color((int)MathHelper.Lerp(156f, 255f, (float)Main.DiscoR / 256f), 108, 251);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Heresy>().AddIngredient<ShadowspecBar>(5).AddIngredient<DarkPlasma>(20)
			.AddIngredient(526, 5)
			.AddTile<DraedonsForge>()
			.Register();
	}

	static Eternity()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		BlueColor = new Color(34, 34, 160);
		PinkColor = new Color(169, 30, 184);
	}
}
