using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Net;

namespace CalamityMod.Items.Tools.ClimateChange;

[LegacyName(new string[] { "Moonlight" })]
public class Bakidon : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public static int FreezeTime => CalamityUtils.MinutesToFrames(10);

	public static float RechargeMultiplier => 1f;

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.rare = 4;
		base.Item.useAnimation = 9;
		base.Item.useTime = 9;
		base.Item.autoReuse = false;
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.Item60;
		base.Item.consumable = false;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override bool? UseItem(Player player)
	{
		player.Calamity().WeakTimeFreezeInUse = !player.Calamity().WeakTimeFreezeInUse;
		if (Main.netMode != 2 && player == Main.LocalPlayer)
		{
			NetPacket packet = NetCreativePowersModule.PreparePacket(CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>().PowerId, 1);
			packet.Writer.Write(player.Calamity().WeakTimeFreezeInUse);
			NetManager.Instance.SendToServerOrLoopback(packet);
		}
		return true;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer cplayer = Main.LocalPlayer.Calamity();
		float fill = 1f - cplayer.WeakTimeFreezeUseTimer / ((float)FreezeTime / RechargeMultiplier);
		if (!(fill >= 1f))
		{
			float barScale = 1.1f;
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			Vector2 barOrigin = barBG.Size() * 0.5f;
			float yOffset = 7.5f;
			Vector2 drawPos = position + Vector2.UnitY * scale * ((float)frame.Height - yOffset);
			Rectangle frameCrop = default(Rectangle);
			((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)(fill * (float)barFG.Width), barFG.Height);
			Color colorBG = Color.DarkViolet * 0.5f;
			Color colorFG = Color.Lerp(Color.Orange, Color.Green, fill);
			spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, colorBG, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, colorFG, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(75, 10).AddIngredient(520, 7).AddIngredient(521, 7)
			.AddIngredient<EssenceofSunlight>(5)
			.AddTile(26)
			.Register();
	}
}
