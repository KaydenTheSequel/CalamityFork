using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class TeslaCannon : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/TeslaCannonFire");

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.width = 78;
		base.Item.height = 28;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.damage = 1200;
		base.Item.knockBack = 10f;
		base.Item.useTime = (base.Item.useAnimation = 90);
		base.Item.autoReuse = true;
		base.Item.mana = 60;
		base.Item.useStyle = 5;
		base.Item.UseSound = FireSound;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.shoot = ModContent.ProjectileType<TeslaCannonShot>();
		base.Item.shootSpeed = 5f;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref velocity)).Length() > 5f)
		{
			((Vector2)(ref velocity)).Normalize();
			velocity *= 5f;
		}
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-20f, 0f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 5);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(25).AddIngredient<DubiousPlating>(15).AddIngredient<CosmiliteBar>(8)
			.AddIngredient<AscendantSpiritEssence>(2)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(5, out var condition), condition)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
