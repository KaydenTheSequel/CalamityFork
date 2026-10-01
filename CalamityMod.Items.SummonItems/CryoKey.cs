using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.Cryogen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

public class CryoKey : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.SortingPriorityBossSpawns[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 48;
		base.Item.rare = 5;
		base.Item.useAnimation = 10;
		base.Item.useTime = 10;
		base.Item.useStyle = 4;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.BossItem;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ZoneSnow && !NPC.AnyNPCs(ModContent.NPCType<Cryogen>()))
		{
			return !BossRushEvent.BossRushActive;
		}
		return false;
	}

	public override bool? UseItem(Player player)
	{
		CalamityUtils.SpawnBossUsingItem<Cryogen>(player, new SoundStyle?(SoundID.Roar));
		return true;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/CryoKey", (AssetRequestMode)2).Value;
		Color overlay = (Main.zenithWorld ? Color.Red : Color.White);
		spriteBatch.Draw(texture, position, (Rectangle?)null, overlay, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/CryoKey", (AssetRequestMode)2).Value;
		Color overlay = (Main.zenithWorld ? Color.Red : lightColor);
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)null, overlay, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.zenithWorld)
		{
			base.Item.SetNameOverride(this.GetLocalizedValue("GFBName"));
		}
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[SPAWN]", this.GetLocalizedValue(Main.zenithWorld ? "SpawnGFB" : "SpawnNormal"));
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyIceBlock", 50).AddIngredient(520, 5).AddIngredient(521, 5)
			.AddIngredient<EssenceofEleum>(8)
			.AddTile(16)
			.Register();
	}
}
