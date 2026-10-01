using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.TileEntities;
using CalamityMod.Tiles.DraedonSummoner;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.DraedonMisc;

public class AdvancedDisplay : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle InstallSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/AdvancedDisplayInstall");

	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 52;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.useStyle = 1;
		base.Item.rare = 8;
		base.Item.useAnimation = (base.Item.useTime = 15);
	}

	public override bool? UseItem(Player player)
	{
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 3);
	}

	public override bool ConsumeItem(Player player)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Point placeTileCoords = Main.MouseWorld.ToTileCoordinates();
		Tile tile = CalamityUtils.ParanoidTileRetrieval(placeTileCoords.X, placeTileCoords.Y);
		float checkDistance = ((float)(Player.tileRangeX + Player.tileRangeY) / 2f + (float)player.blockRange) * 16f;
		if (Main.myPlayer == player.whoAmI && player.WithinRange(Main.MouseWorld, checkDistance) && tile.HasTile && tile.TileType == ModContent.TileType<CodebreakerTile>())
		{
			SoundEngine.PlaySound(in InstallSound, Main.LocalPlayer.Center);
			TECodebreaker codebreakerTileEntity = CalamityUtils.FindTileEntity<TECodebreaker>(placeTileCoords.X, placeTileCoords.Y, 5, 8, 18);
			if (codebreakerTileEntity == null || codebreakerTileEntity.ContainsAdvancedDisplay)
			{
				return false;
			}
			codebreakerTileEntity.ContainsAdvancedDisplay = true;
			codebreakerTileEntity.SyncConstituents((short)Main.myPlayer);
			return true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(10).AddIngredient<DubiousPlating>(10).AddIngredient<LifeAlloy>(3)
			.AddIngredient(170, 20)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(3, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
