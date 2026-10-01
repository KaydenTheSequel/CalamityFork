using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.TileEntities;
using CalamityMod.Tiles.DraedonSummoner;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.DraedonMisc;

public class AuricQuantumCoolingCell : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle InstallSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/AuricQuantumCoolingCellInstallNew");

	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 44;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.consumable = true;
		base.Item.useStyle = 1;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 5, allowOldWorlds: true);
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale * Main.rand.NextFloat(0.9f, 1.1f);
		Lighting.AddLight(base.Item.Center, 1.2f * brightness, 0.4f * brightness, 0.8f);
	}

	public override bool? UseItem(Player player)
	{
		return true;
	}

	public override bool ConsumeItem(Player player)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Point placeTileCoords = Main.MouseWorld.ToTileCoordinates();
		Tile tile = CalamityUtils.ParanoidTileRetrieval(placeTileCoords.X, placeTileCoords.Y);
		float checkDistance = ((float)(Player.tileRangeX + Player.tileRangeY) / 2f + (float)player.blockRange) * 16f;
		if (Main.myPlayer == player.whoAmI && player.WithinRange(Main.MouseWorld, checkDistance) && tile.HasTile && tile.TileType == ModContent.TileType<CodebreakerTile>())
		{
			SoundEngine.PlaySound(in InstallSound, Main.LocalPlayer.Center);
			TECodebreaker codebreakerTileEntity = CalamityUtils.FindTileEntity<TECodebreaker>(placeTileCoords.X, placeTileCoords.Y, 5, 8, 18);
			if (codebreakerTileEntity == null || codebreakerTileEntity.ContainsCoolingCell || codebreakerTileEntity.DecryptionCountdown > 0)
			{
				return false;
			}
			codebreakerTileEntity.ContainsCoolingCell = true;
			codebreakerTileEntity.SyncConstituents((short)Main.myPlayer);
			return true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AuricBar>(5).AddIngredient<MysteriousCircuitry>(8).AddIngredient<DubiousPlating>(8)
			.AddIngredient<EndothermicEnergy>(40)
			.AddIngredient<EssenceofEleum>(6)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(5, out var condition), condition)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
