using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Rarities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class CosmicWorm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 18;
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

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(ModContent.NPCType<DevourerofGodsHead>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return true;
		}
		CalamityUtils.SpawnBossOnPosUsingItem<DevourerofGodsHead>(player, (int)player.Center.X, (int)player.Center.Y - 1600, new SoundStyle?(DevourerofGodsHead.SpawnSound));
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArmoredShell>().AddIngredient<TwistingNether>().AddIngredient<DarkPlasma>()
			.AddTile(134)
			.Register()
			.DisableDecraft();
		CreateRecipe().AddIngredient(3467, 40).AddIngredient<GalacticaSingularity>(10).AddIngredient<Necroplasm>(40)
			.AddTile(134)
			.Register();
	}
}
