using System.Collections.Generic;
using System.IO;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Accessories;

public class EtherealTalisman : ModItem, ILocalizedModType, IModType
{
	private bool manaFlowerEnabled = true;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		if (!manaFlowerEnabled)
		{
			tooltips.RemoveAll((TooltipLine x) => x.Name == "Tooltip3");
		}
	}

	public override bool CanRightClick()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Main.keyState.PressingShift();
	}

	public override void RightClick(Player player)
	{
		manaFlowerEnabled = !manaFlowerEnabled;
		base.Item.NetStateChanged();
	}

	public override bool ConsumeItem(Player player)
	{
		return false;
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("manaFlower", manaFlowerEnabled);
	}

	public override void LoadData(TagCompound tag)
	{
		manaFlowerEnabled = tag.GetBool("manaFlower");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(manaFlowerEnabled);
	}

	public override void NetReceive(BinaryReader reader)
	{
		manaFlowerEnabled = reader.ReadBoolean();
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryDot(spriteBatch, position, new Vector2(16f, 16f) * Main.inventoryScale, manaFlowerEnabled);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().eTalisman = true;
		player.manaMagnet = true;
		if (manaFlowerEnabled)
		{
			player.manaFlower = true;
		}
		player.statManaMax2 += 150;
		player.GetDamage<MagicDamageClass>() += 0.15f;
		player.manaCost -= 0.1f;
		player.GetCritChance<MagicDamageClass>() += 5f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SigilofCalamitas>().AddRecipeGroup("AnyManaFlower").AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
