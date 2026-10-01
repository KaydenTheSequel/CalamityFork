using System.Collections.Generic;
using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Accessories;

public class DeificAmulet : ModItem, ILocalizedModType, IModType
{
	public static readonly int MaxBonusIFrames = 30;

	private bool panicNecklaceEnabled = true;

	public new string LocalizationCategory => "Items.Accessories";

	public static int StarDamage => 130.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.rare = 9;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.accessory = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips.FindAndReplace("[TOGGLE]", panicNecklaceEnabled ? this.GetLocalizedValue("ToggleEffect") : "");
	}

	public override bool CanRightClick()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Main.keyState.PressingShift();
	}

	public override void RightClick(Player player)
	{
		panicNecklaceEnabled = !panicNecklaceEnabled;
		base.Item.NetStateChanged();
	}

	public override bool ConsumeItem(Player player)
	{
		return false;
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("panic", panicNecklaceEnabled);
	}

	public override void LoadData(TagCompound tag)
	{
		panicNecklaceEnabled = tag.GetBool("panic");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(panicNecklaceEnabled);
	}

	public override void NetReceive(BinaryReader reader)
	{
		panicNecklaceEnabled = reader.ReadBoolean();
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryDot(spriteBatch, position, new Vector2(16f, 16f) * Main.inventoryScale, panicNecklaceEnabled);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		player.longInvince = true;
		calamityPlayer.dAmulet = true;
		player.panic = panicNecklaceEnabled;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(862).AddIngredient(1578).AddIngredient<AstralBar>(10)
			.AddTile(114)
			.Register();
	}
}
