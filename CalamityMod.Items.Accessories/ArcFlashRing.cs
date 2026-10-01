using CalamityMod.CalPlayer;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ArcFlashRing : ModItem, ILocalizedModType, IModType
{
	public static int LightningSpawnPercent = 5;

	public static float LightningDamageMult = 3f;

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(LightningSpawnPercent, LightningDamageMult);

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 64;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.arcFlashRing = true;
		if (!hideVisual)
		{
			modPlayer.arcFlashRingVisual = true;
		}
		else
		{
			modPlayer.arcFlashRingVisual = false;
		}
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.7f, new Vector2(1f, 0f), (SpriteEffects)0);
		return false;
	}
}
