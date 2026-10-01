using System.Collections.Generic;
using System.IO;
using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "StatisBeltOfCurses" })]
public class StatisVoidSash : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public static SoundStyle VoidDash = new SoundStyle("CalamityMod/Sounds/Item/VoidDash");

	private bool toggleEnabled = true;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(8, 3));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 32;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool CanRightClick()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Main.keyState.PressingShift();
	}

	public override void RightClick(Player player)
	{
		toggleEnabled = !toggleEnabled;
		base.Item.NetStateChanged();
	}

	public override bool ConsumeItem(Player player)
	{
		return false;
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("toggleEffect", toggleEnabled);
	}

	public override void LoadData(TagCompound tag)
	{
		toggleEnabled = tag.GetBool("toggleEffect");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(toggleEnabled);
	}

	public override void NetReceive(BinaryReader reader)
	{
		toggleEnabled = reader.ReadBoolean();
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryDot(spriteBatch, position, new Vector2(16f, 16f) * Main.inventoryScale, toggleEnabled);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips.FindAndReplace("[TOGGLE]", toggleEnabled ? this.GetLocalizedValue("ToggleEffect") : "");
		base.ModifyTooltips(tooltips);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.voidSashVisuals = !hideVisual;
		player.autoJump = true;
		player.jumpSpeedBoost += 1.6f;
		player.moveSpeed += 0.2f;
		player.noFallDmg = true;
		player.blackBelt = true;
		calamityPlayer.DashID = StatisVoidSashDash.ID;
		player.dashType = 0;
		if (toggleEnabled)
		{
			player.spikedBoots = 2;
		}
		player.accFlipper = true;
		player.Calamity().statisVoidSash = true;
		player.MountedCenter.ToTileCoordinates();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StatisNinjaBelt>().AddIngredient<TwistingNether>(10).AddIngredient<NightmareFuel>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 1f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
