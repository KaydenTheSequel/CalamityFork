using System.Collections.Generic;
using CalamityMod.Balancing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.PermanentBoosters;

public class RedLightningContainer : ModItem, ILocalizedModType, IModType
{
	public int frameCounter;

	public int frame;

	public new string LocalizationCategory => "Items.Misc";

	public override LocalizedText Tooltip => CalamityUtils.GetText(LocalizationCategory + ".RageBoosterTooltip").WithFormatArgs(BalancingConstants.RageDurationPerBooster.FramesToSeconds());

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 40;
		base.Item.consumable = true;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.UseSound = SoundID.Item122;
		base.Item.useStyle = 4;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 11;
		base.Item.SetRevExclusive();
	}

	public static bool HasConsumedBefore(Player player)
	{
		return player.Calamity().rageBoostThree;
	}

	public override bool CanUseItem(Player player)
	{
		if (HasConsumedBefore(player))
		{
			return false;
		}
		return true;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frameI, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/PermanentBoosters/RedLightningContainer_Animated", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, position, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 6), Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/PermanentBoosters/RedLightningContainer_Animated", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 6), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/PermanentBoosters/RedLightningContainerGlow", (AssetRequestMode)2).Value;
		spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)base.Item.GetCurrentFrame(ref frame, ref frameCounter, 5, 6, frameCounterUp: false), Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
	}

	public override bool? UseItem(Player player)
	{
		if (player.itemAnimation > 0 && player.itemTime == 0)
		{
			player.itemTime = base.Item.useTime;
			player.Calamity().rageBoostThree = true;
		}
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		if (HasConsumedBefore(Main.LocalPlayer))
		{
			list.AddConsumedTooltip("Tooltip0");
		}
	}
}
