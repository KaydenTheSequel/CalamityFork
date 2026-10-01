using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.CalPlayer;
using CalamityMod.Rarities;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Accessories;

public class ShatteredCommunity : ModItem, ILocalizedModType, IModType
{
	public const float RagePerSecond = 0.02f;

	public static readonly int RageGainCooldown;

	private static readonly Color rarityColorOne;

	private static readonly Color rarityColorTwo;

	internal const long BaseLevelCost = 400000L;

	internal const int MaxLevel = 25;

	internal const float RageDamagePerLevel = 0.01f;

	internal int level;

	internal long totalRageDamage;

	public new string LocalizationCategory => "Items.Accessories";

	internal static long LevelCost(int level)
	{
		return 400000L * (long)level;
	}

	internal static long CumulativeLevelCost(int level)
	{
		return 200000L * (long)level * (level + 1);
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(7, 5));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<TheCommunity>();
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override ModItem Clone(Item item)
	{
		ShatteredCommunity obj = (ShatteredCommunity)base.Clone(base.Item);
		obj.level = level;
		obj.totalRageDamage = totalRageDamage;
		return obj;
	}

	internal static Color GetRarityColor()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.ColorSwap(rarityColorOne, rarityColorTwo, 3f);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.shatteredCommunity = true;
		player.GetModPlayer<ShatteredCommunityPlayer>().sc = this;
		player.GetDamage<GenericDamageClass>() += 0.1f;
		player.GetCritChance<GenericDamageClass>() += 5f;
		player.statDefense += 10;
		player.endurance += 0.05f;
		player.lifeRegen += 2;
		player.moveSpeed += 0.1f;
		calamityPlayer.RageDamageBoost += (float)level * 0.01f;
	}

	public override bool CanEquipAccessory(Player player, int slot, bool modded)
	{
		return !player.Calamity().community;
	}

	public override bool CanUseItem(Player player)
	{
		return false;
	}

	public override void PostUpdate()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale;
		Lighting.AddLight(base.Item.Center, 0.92f * brightness, 0.42f * brightness, 0.92f * brightness);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string desc = (CalamityWorld.revenge ? this.GetLocalizedValue("RageModified") : this.GetLocalization("RageAdd").Format(CalamityKeybinds.RageHotKey.TooltipHotkeyString()));
		tooltips.FindAndReplace("[RAGEDESC]", desc);
		tooltips.FindAndReplace("[LEVEL]", level.ToString());
		string progressKey = "[PROGRESS]";
		TooltipLine progressLine = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Text.Contains(progressKey));
		if (progressLine != null)
		{
			if (level < 25)
			{
				long num = totalRageDamage - CumulativeLevelCost(level);
				long totalToNextLevel = LevelCost(level + 1);
				double ratio = (double)num / (double)totalToNextLevel;
				string percent = (100.0 * ratio).ToString("0.00");
				progressLine.Text = progressLine.Text.Replace(progressKey, percent);
			}
			else
			{
				progressLine.Text = string.Empty;
			}
		}
		tooltips.FindAndReplace("[DAMAGE]", totalRageDamage.ToString());
	}

	public override void SaveData(TagCompound tag)
	{
		tag.Add("level", level);
		tag.Add("totalDamage", totalRageDamage);
	}

	public override void LoadData(TagCompound tag)
	{
		level = tag.GetInt("level");
		if (level > 25)
		{
			level = 25;
		}
		totalRageDamage = tag.GetLong("totalDamage");
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(level);
		writer.Write(totalRageDamage);
	}

	public override void NetReceive(BinaryReader reader)
	{
		level = reader.ReadInt32();
		totalRageDamage = reader.ReadInt64();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.5f, new Vector2(0f, 2f), (SpriteEffects)0);
		return true;
	}

	static ShatteredCommunity()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		RageGainCooldown = 10;
		rarityColorOne = new Color(128, 62, 128);
		rarityColorTwo = new Color(245, 105, 245);
	}
}
