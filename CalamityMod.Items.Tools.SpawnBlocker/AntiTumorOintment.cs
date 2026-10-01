using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Tools.SpawnBlocker;

public class AntiTumorOintment : ModItem, ILocalizedModType, IModType
{
	public bool Enabled = true;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)92;
	}

	public override ModItem Clone(Item item)
	{
		AntiTumorOintment obj = (AntiTumorOintment)base.Clone(item);
		obj.Enabled = Enabled;
		return obj;
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("blockerEnabled", Enabled);
	}

	public override void LoadData(TagCompound tag)
	{
		Enabled = tag.GetBool("blockerEnabled");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(Enabled);
	}

	public override void NetReceive(BinaryReader reader)
	{
		Enabled = reader.ReadBoolean();
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override bool ConsumeItem(Player player)
	{
		return false;
	}

	public override void RightClick(Player player)
	{
		Enabled = !Enabled;
		base.Item.NetStateChanged();
	}

	public override void UpdateInventory(Player player)
	{
		player.Calamity().disableHiveCystSpawns |= Enabled;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string text = ((!Enabled) ? CalamityUtils.GetTextValue("Items.Misc.SpawnBlockersOff") : CalamityUtils.GetTextValue("Items.Misc.SpawnBlockersOn"));
		tooltips.FindAndReplace("[STATE]", text);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(57, 5).AddIngredient(126).AddTile(16)
			.Register();
	}
}
