using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Tools;

public class BallAndChain : ModItem, ILocalizedModType, IModType
{
	public static Asset<Texture2D> DisabledSprite;

	public bool Enabled = true;

	public new string LocalizationCategory => "Items.Tools";

	public override void Load()
	{
		DisabledSprite = ModContent.Request<Texture2D>("CalamityMod/Items/Tools/BallAndChainDisabled", (AssetRequestMode)2);
	}

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 48;
		base.Item.rare = 1;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override ModItem Clone(Item item)
	{
		BallAndChain obj = (BallAndChain)base.Clone(item);
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

	public override bool CanUseItem(Player player)
	{
		return false;
	}

	public override void UpdateInventory(Player player)
	{
		player.Calamity().blockAllDashes |= Enabled;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string text = this.GetLocalizedValue(Enabled ? "TooltipEnabled" : "TooltipDisabled");
		tooltips.FindAndReplace("[STATE]", text);
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = (Enabled ? TextureAssets.Item[base.Type].Value : DisabledSprite.Value);
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, tex, position, frame, drawColor, itemColor, origin, scale, 0.8f, default(Vector2), (SpriteEffects)0);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = (Enabled ? TextureAssets.Item[base.Type].Value : DisabledSprite.Value);
		Vector2 origin = tex.Size() / 2f;
		spriteBatch.Draw(tex, base.Item.Bottom - Main.screenPosition - Vector2.UnitY * origin.Y, (Rectangle?)null, lightColor, rotation, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("IronBar", 10).AddIngredient(85).AddTile(16)
			.Register();
	}
}
