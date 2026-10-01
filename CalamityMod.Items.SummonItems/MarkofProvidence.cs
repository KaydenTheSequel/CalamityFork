using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.StormWeaver;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "RuneofCos", "RuneofKos" })]
public class MarkofProvidence : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle CVSound = new SoundStyle("CalamityMod/Sounds/Item/CeaselessVoidSpawn");

	public static readonly SoundStyle SignutSound = new SoundStyle("CalamityMod/Sounds/Item/SignusSpawn");

	public static readonly SoundStyle StormSound = new SoundStyle("CalamityMod/Sounds/Item/StormWeaverSpawn");

	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
		base.Item.UseSound = null;
		base.Item.consumable = false;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override bool CanUseItem(Player player)
	{
		if ((player.ZoneSkyHeight || player.ZoneUnderworldHeight || player.ZoneDungeon) && !NPC.AnyNPCs(ModContent.NPCType<StormWeaverHead>()) && !NPC.AnyNPCs(ModContent.NPCType<CeaselessVoid>()) && !NPC.AnyNPCs(ModContent.NPCType<Signus>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		if (player.ZoneDungeon)
		{
			CalamityUtils.SpawnBossUsingItem<CeaselessVoid>(player, new SoundStyle?(CVSound));
		}
		else if (player.ZoneUnderworldHeight)
		{
			CalamityUtils.SpawnBossUsingItem<Signus>(player, new SoundStyle?(SignutSound));
		}
		else if (player.ZoneSkyHeight)
		{
			CalamityUtils.SpawnBossUsingItem<StormWeaverHead>(player, new SoundStyle?(StormSound));
		}
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		Player player = Main.LocalPlayer;
		string line = this.GetLocalizedValue("SpawnInfo");
		if (player.ZoneDungeon)
		{
			line = this.GetLocalizedValue("SpawnVoid");
		}
		else if (player.ZoneUnderworldHeight)
		{
			line = this.GetLocalizedValue("SpawnSignus");
		}
		else if (player.ZoneSkyHeight)
		{
			line = this.GetLocalizedValue("SpawnWeaver");
		}
		list.FindAndReplace("[SPAWN]", line);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3467, 3).AddIngredient<UnholyEssence>(40).AddIngredient(3458, 5)
			.AddTile(134)
			.Register();
	}
}
