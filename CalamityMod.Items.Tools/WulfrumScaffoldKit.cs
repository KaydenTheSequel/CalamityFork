using System.IO;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Tools;

public class WulfrumScaffoldKit : ModItem, ILocalizedModType, IModType
{
	public int storedScrap;

	public static int TilesPerScrap = 40;

	public static int TileTime = 360;

	public static int TileReach = 40;

	public new string LocalizationCategory => "Items.Tools";

	public static int PlacedTileType => ModContent.TileType<WulfrumPipes>();

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TilesPerScrap);

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 42;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.autoReuse = false;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 10;
		base.Item.UseSound = null;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.rare = 1;
		base.Item.value = Item.sellPrice(0, 0, 10);
		storedScrap = 0;
		base.Item.shoot = ModContent.ProjectileType<WulfrumScaffoldKitHoldout>();
		TileTime = 360;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool CanUseItem(Player player)
	{
		if ((storedScrap > 0 || player.HasItem(ModContent.ItemType<WulfrumMetalScrap>())) && !player.noBuilding)
		{
			return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == base.Item.shoot);
		}
		return false;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		float barScale = 1f;
		Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
		Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
		Vector2 drawPos = position + Vector2.UnitY * (float)(frame.Height - 2) * scale + Vector2.UnitX * ((float)frame.Width - (float)barBG.Width * barScale) * scale * 0.5f;
		Rectangle frameCrop = default(Rectangle);
		((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)((float)storedScrap / (float)TilesPerScrap * (float)barFG.Width), barFG.Height);
		Color colorBG = Color.RoyalBlue;
		Color colorFG = Color.Lerp(Color.Teal, Color.YellowGreen, (float)storedScrap / (float)TilesPerScrap);
		spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, colorBG, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, colorFG * 0.8f, 0f, origin, scale * barScale, (SpriteEffects)0, 0f);
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (modItem is WulfrumScaffoldKit a && item.ModItem is WulfrumScaffoldKit a2)
		{
			a.storedScrap = a2.storedScrap;
		}
		return modItem;
	}

	public override void SaveData(TagCompound tag)
	{
		tag["storedScrap"] = storedScrap;
	}

	public override void LoadData(TagCompound tag)
	{
		storedScrap = tag.GetInt("storedScrap");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(storedScrap);
	}

	public override void NetReceive(BinaryReader reader)
	{
		storedScrap = reader.ReadInt32();
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<WulfrumMetalScrap>(6).AddIngredient<EnergyCore>().AddTile(16)
			.Register();
	}
}
