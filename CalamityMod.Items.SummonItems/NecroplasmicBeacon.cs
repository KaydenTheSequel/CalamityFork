using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.Polterghast;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class NecroplasmicBeacon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 58;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
		base.Item.consumable = false;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/NecroplasmicBeaconGlow", (AssetRequestMode)2).Value);
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ZoneDungeon && !NPC.AnyNPCs(ModContent.NPCType<Polterghast>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<Polterghast>(player, new SoundStyle?(Polterghast.SpawnSound));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("Wood", 25).AddIngredient<Necroplasm>(50).AddTile(134)
			.Register();
	}
}
