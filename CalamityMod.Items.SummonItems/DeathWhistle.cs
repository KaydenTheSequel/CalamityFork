using CalamityMod.Events;
using CalamityMod.NPCs.Ravager;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "AncientMedallion" })]
public class DeathWhistle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 17;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 8;
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
		if (!NPC.AnyNPCs(ModContent.NPCType<RavagerBody>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		int posX = (int)(player.position.X + (float)Main.rand.Next(-250, 251));
		int posY = (int)(player.position.Y - 500f);
		int bossToSpawn = ModContent.NPCType<RavagerBody>();
		CalamityUtils.SpawnBossOnPosUsingItem(player, bossToSpawn, posX, posY, new SoundStyle?(SoundID.ScaryScream));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2766, 15).AddIngredient(1101, 25).AddTile(26)
			.Register();
	}
}
