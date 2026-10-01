using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "ChickenEgg", "JungleDragonEgg" })]
public class YharonEgg : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 60;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
		base.Item.consumable = false;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override bool CanUseItem(Player player)
	{
		if (!NPC.AnyNPCs(ModContent.NPCType<Yharon>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<Yharon>(player, new SoundStyle?(Yharon.FireSound));
		return true;
	}

	public override void UseItemFrame(Player player)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		player.itemLocation = player.HandPosition.Value + new Vector2((float)(10 * -player.direction), 20f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LifeAlloy>(10).AddIngredient<EffulgentFeather>(15).AddTile(134)
			.Register();
	}
}
