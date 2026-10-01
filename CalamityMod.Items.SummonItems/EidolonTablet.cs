using System;
using CalamityMod.Events;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class EidolonTablet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		NPCID.Sets.MPAllowedEnemies[439] = true;
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 18;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 9;
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
		if (BossRushEvent.BossRushActive)
		{
			return false;
		}
		if (NPC.MoonLordCountdown > 0)
		{
			return false;
		}
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			switch (enumerator.Current.type)
			{
			case 398:
			case 422:
			case 439:
			case 493:
			case 507:
			case 517:
				return false;
			}
		}
		return true;
	}

	public override bool? UseItem(Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		int posX = (int)player.Center.X + 30;
		int posY = (int)player.Center.Y - 90;
		NPC npc = CalamityUtils.SpawnBossOnPosUsingItem(player, 439, posX, posY, (SoundStyle?)null);
		if (npc != null)
		{
			WorldGen.GetRidOfCultists();
			npc.spriteDirection = (npc.direction = Math.Sign(player.Center.X - player.Center.X - 30f));
		}
		return true;
	}
}
