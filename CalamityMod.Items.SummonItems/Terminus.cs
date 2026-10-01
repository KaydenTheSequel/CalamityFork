using System;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.SummonItems;

[LegacyName(new string[] { "BossRush" })]
public class Terminus : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.SummonItems";

	public override void SetDefaults()
	{
		base.Item.width = (Main.zenithWorld ? 54 : 70);
		base.Item.height = (Main.zenithWorld ? 78 : 80);
		base.Item.rare = 1;
		base.Item.useAnimation = 45;
		base.Item.useTime = 45;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<TerminusHoldout>();
		base.Item.useStyle = 4;
		base.Item.consumable = false;
	}

	public override void UpdateInventory(Player player)
	{
		if (Main.zenithWorld)
		{
			base.Item.SetNameOverride(this.GetLocalizedValue("GFBName"));
		}
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = ContentSamples.CreativeHelper.ItemGroup.EventItem;
	}

	public static void DrawTerminusGlow(SpriteBatch spriteBatch, Vector2 baseDrawPosition, Rectangle frame, float rotation, float baseScale)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/TerminusGlow", (AssetRequestMode)2).Value;
		Vector2 origin = frame.Size() * 0.5f;
		float pulseRate = Main.GlobalTimeWrappedHourly % 5f;
		float slowingRate = Main.GlobalTimeWrappedHourly * 0.4f;
		float orbitRadius = ((baseScale != 1f) ? 4f : 7f);
		float glowCount = 3f;
		pulseRate /= 2.5f;
		if (pulseRate >= 1f)
		{
			pulseRate = 2f - pulseRate;
		}
		pulseRate *= 0.6f;
		for (int i = 0; (float)i < glowCount; i++)
		{
			float pulseRotation = ((float)i / glowCount + slowingRate) * ((float)Math.PI * 2f);
			Vector2 offset = Vector2.UnitY * orbitRadius;
			offset = offset.RotatedBy(pulseRotation) * pulseRate;
			spriteBatch.Draw(glowTexture, baseDrawPosition + offset, (Rectangle?)frame, new Color(56, 12, 115, 33), rotation, origin, baseScale, (SpriteEffects)0, 0f);
		}
		spriteBatch.Draw(glowTexture, baseDrawPosition, (Rectangle?)frame, Color.White, rotation, origin, baseScale, (SpriteEffects)0, 0f);
	}

	public static void DrawGlowInWorld(SpriteBatch spriteBatch, Vector2 baseDrawPosition, float rotation, float baseScale)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/TerminusGlow", (AssetRequestMode)2).Value.Bounds;
		DrawTerminusGlow(spriteBatch, baseDrawPosition + new Vector2(0f, -1f), frame, rotation, baseScale);
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		float invScale = scale * 1.4f;
		if (Main.zenithWorld)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Terminus_GFB", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, position - new Vector2(-4f, -1f), (Rectangle?)null, Color.White, 0f, origin, invScale - 0.04f, (SpriteEffects)0, 0f);
		}
		else
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Terminus", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, position, (Rectangle?)null, Color.White, 0f, origin, invScale, (SpriteEffects)0, 0f);
			DrawTerminusGlow(spriteBatch, position, frame, 0f, invScale);
		}
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/SummonItems/Terminus_GFB", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)null, lightColor, rotation, Vector2.Zero, scale, (SpriteEffects)0, 0f);
			return false;
		}
		return true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.zenithWorld)
		{
			DrawGlowInWorld(spriteBatch, base.Item.Center - Main.screenPosition, rotation, scale);
		}
	}
}
