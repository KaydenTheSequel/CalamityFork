using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class Calamity : ModItem, ILocalizedModType, IModType
{
	public const float MaxNPCSpeed = 5f;

	public const int BaseDamage = 320;

	public const int FramesPerHit = 5;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 108;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().blazingCursorVisuals = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().blazingCursorDamage = true;
		player.Calamity().blazingCursorVisuals = true;
	}

	public override void UpdateItemDye(Player player, int dye, bool hideVisual)
	{
		player.Calamity().CalamityFireDyeShader = GameShaders.Armor.GetSecondaryShader(dye, player);
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().blazingCursorVisuals = true;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (Main.zenithWorld ? ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/Calamity_GFB", (AssetRequestMode)2).Value : TextureAssets.Item[base.Type].Value);
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, texture, position, frame, drawColor, itemColor, origin, scale, 0.5f, new Vector2(0f, -4f), (SpriteEffects)0);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/Calamity_GFB", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)Main.itemAnimations[base.Type].GetFrame(texture), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}
}
