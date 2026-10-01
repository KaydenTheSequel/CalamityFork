using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class Affliction : ModItem, ILocalizedModType, IModType
{
	public static int RegenBoost = 1;

	public static int MaxLifeBoostPercent = 10;

	public static float DamageReductionBoost = 0.07f;

	public static int DefenseBoost = 9;

	public static float DamageBoost = 0.1f;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RegenBoost.ToRegenPerSecond(), MaxLifeBoostPercent, DamageReductionBoost.ToPercent(), DefenseBoost, DamageBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 44;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.expert = true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/Affliction", (AssetRequestMode)2).Value);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().affliction = true;
		if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0 && Main.LocalPlayer.team == player.team && player.team != 0)
		{
			Main.LocalPlayer.AddBuff(ModContent.BuffType<Afflicted>(), 20);
		}
	}
}
