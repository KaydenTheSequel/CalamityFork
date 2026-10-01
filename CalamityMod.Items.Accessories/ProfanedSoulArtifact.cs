using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Items.Placeables.Plates;
using CalamityMod.Rarities;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ProfanedSoulArtifact : ModItem, ILocalizedModType, IModType, IDyeableShaderRenderer
{
	public static Asset<Texture2D> HeatTex;

	public static int ShieldRechargeDelay = CalamityUtils.SecondsToFrames(5);

	public static int TotalShieldRechargeTime = CalamityUtils.SecondsToFrames(2);

	public static int ShieldDurabilityMax = 25;

	public new string LocalizationCategory => "Items.Accessories";

	public int OwnerPlayer { get; set; }

	public float RenderDepth => 3f;

	public bool ShouldDrawDyeableShader
	{
		get
		{
			if (CalamityClientConfig.Instance.EnergyShieldOpacity <= 0f)
			{
				return false;
			}
			if (OwnerPlayer < 0 || OwnerPlayer >= 255)
			{
				return false;
			}
			Player player = Main.player[OwnerPlayer];
			if (player == null)
			{
				return false;
			}
			if (player.outOfRange || player.dead)
			{
				return false;
			}
			if (player.Calamity().drawingParameters.ProfanedShieldCharge <= 0f)
			{
				return false;
			}
			return true;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 6));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public void DrawDyeableShader(SpriteBatch spriteBatch)
	{
		DrawProfanedSoulShields(OwnerPlayer);
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 40;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
	{
		return incomingItem.type != ModContent.ItemType<ProfanedSoulCrystal>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.pSoulArtifact = true;
		calamityPlayer.pSoulShieldVisible = !hideVisual;
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().pSoulShieldVisible = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string adrenTooltip = (CalamityWorld.revenge ? this.GetLocalizedValue("ShieldAdren") : "");
		tooltips.FindAndReplace("[ADREN]", adrenTooltip);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DivineGeode>(5).AddIngredient<Havocplate>(25).AddIngredient<ExodiumCluster>(25)
			.AddTile(134)
			.Register();
	}

	internal static void DrawProfanedSoulShields(int whoAmI)
	{
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		if (whoAmI < 0 || whoAmI >= 255)
		{
			return;
		}
		Player player = Main.player[whoAmI];
		if (player == null || player.outOfRange || player.dead)
		{
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.drawnAnyShieldThisFrame || modPlayer.drawingParameters.ProfanedShieldCharge <= 0f)
		{
			return;
		}
		int i = player.whoAmI;
		float scale = 0.15f + 0.03f * (0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 0.5f + (float)i * 0.2f));
		float visualShieldStrength = modPlayer.drawingParameters.ProfanedShieldCharge;
		Color shieldColor = modPlayer.drawingParameters.ProfanedShieldColor;
		float noiseScale = MathHelper.Lerp(0.4f, 0.8f, MathF.Sin(Main.GlobalTimeWrappedHourly * 0.3f) * 0.5f + 0.5f);
		Effect shieldEffect = Filters.Scene["CalamityMod:RoverDriveShield"].GetShader().Shader;
		shieldEffect.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 0.058f);
		shieldEffect.Parameters["blowUpPower"].SetValue(2.8f);
		shieldEffect.Parameters["blowUpSize"].SetValue(0.4f);
		shieldEffect.Parameters["noiseScale"].SetValue(noiseScale);
		float num = 0.9f + 0.1f * MathF.Sin(Main.GlobalTimeWrappedHourly * 1.95f);
		float minShieldStrengthOpacityMultiplier = 0.5f;
		float finalShieldOpacity = num * MathHelper.Lerp(minShieldStrengthOpacityMultiplier, 1f, visualShieldStrength);
		finalShieldOpacity *= CalamityClientConfig.Instance.EnergyShieldOpacity;
		shieldEffect.Parameters["shieldOpacity"].SetValue(finalShieldOpacity);
		shieldEffect.Parameters["shieldEdgeBlendStrenght"].SetValue(4f);
		Color primaryEdgeColor = new Color(230, 199, 102) * 0.8f;
		Color secondaryEdgeColor = new Color(249, 231, 217) * 0.8f;
		Color edgeColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.2f, primaryEdgeColor, secondaryEdgeColor);
		shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref shieldColor)).ToVector3());
		shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, shieldEffect, Main.Transform);
			if (HeatTex == null)
			{
				HeatTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons2", (AssetRequestMode)2);
			}
			Vector2 pos = player.MountedCenter + player.gfxOffY * Vector2.UnitY - Main.screenPosition;
			Texture2D tex = HeatTex.Value;
			Main.spriteBatch.Draw(tex, pos, (Rectangle?)null, Color.White, 0f, tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
			float shieldScale = scale * 1.75f;
			Texture2D shieldTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleOpenCircle", (AssetRequestMode)2).Value;
			Rectangle shieldFrame = shieldTexture.Frame();
			Vector2 origin = shieldFrame.Size() * 0.5f;
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.Transform);
			Main.spriteBatch.Draw(shieldTexture, pos, (Rectangle?)shieldFrame, shieldColor * 0.5f, player.fullRotation, origin, shieldScale, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(shieldTexture, pos, (Rectangle?)shieldFrame, secondaryEdgeColor * 0.5f, player.fullRotation, origin, shieldScale * 0.95f, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(shieldTexture, pos, (Rectangle?)shieldFrame, shieldColor * 0.5f, player.fullRotation, origin, shieldScale, (SpriteEffects)0, 0f);
			Main.spriteBatch.Draw(shieldTexture, pos, (Rectangle?)shieldFrame, secondaryEdgeColor * 0.5f, player.fullRotation, origin, shieldScale * 0.95f, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
		}
		modPlayer.drawnAnyShieldThisFrame = true;
	}
}
