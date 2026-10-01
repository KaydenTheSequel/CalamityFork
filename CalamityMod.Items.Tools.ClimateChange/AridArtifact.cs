using CalamityMod.World;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools.ClimateChange;

public class AridArtifact : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 5;
		base.Item.useAnimation = 20;
		base.Item.useTime = 20;
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.Item66;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.EventItem;
	}

	public override bool CanUseItem(Player player)
	{
		if (!DownedBossSystem.downedDesertScourge)
		{
			return Main.hardMode;
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (Main.netMode == 1)
		{
			return true;
		}
		if (Sandstorm.Happening)
		{
			CalamityWorld.StopSandstorm();
		}
		else
		{
			CalamityWorld.StartSandstorm();
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(169, 50).AddRecipeGroup("AnyAdamantiteBar", 10).AddIngredient(3794, 5)
			.AddTile(134)
			.Register();
	}
}
