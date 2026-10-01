using CalamityMod.Events;
using CalamityMod.NPCs.Crabulon;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class DecapoditaSprout : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 18;
		base.Item.rare = 2;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
		base.Item.consumable = false;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ZoneGlowshroom && (double)(player.position.Y / 16f) > Main.worldSurface && !NPC.AnyNPCs(ModContent.NPCType<Crabulon>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		int posX = (int)(player.position.X + (float)Main.rand.Next(-160, 161));
		int posY = (int)(player.position.Y - 320f);
		CalamityUtils.SpawnBossOnPosUsingItem<Crabulon>(player, posX, posY, new SoundStyle?(SoundID.Roar));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(183, 50).AddTile(26).Register();
	}
}
