using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "Sponge" })]
public class TheSponge : ModItem, ILocalizedModType, IModType, IDyeableShaderRenderer
{
	public static Asset<Texture2D> NoiseTex;

	public static readonly SoundStyle ShieldHurtSound = new SoundStyle("CalamityMod/Sounds/Custom/RoverDriveHit")
	{
		PitchVariance = 0.6f,
		Volume = 0.6f,
		MaxInstances = 0
	};

	public static readonly SoundStyle ActivationSound = new SoundStyle("CalamityMod/Sounds/Custom/RoverDriveActivate")
	{
		Volume = 0.85f
	};

	public static readonly SoundStyle BreakSound = new SoundStyle("CalamityMod/Sounds/Custom/RoverDriveBreak")
	{
		Volume = 0.75f
	};

	public static int ShieldDurabilityMax = 120;

	public static float ShieldActiveDamageReduction = 0.1f;

	public static int ShieldRechargeDelay = CalamityUtils.SecondsToFrames(8);

	public static int TotalShieldRechargeTime = CalamityUtils.SecondsToFrames(10);

	public new string LocalizationCategory => "Items.Accessories";

	public override string Texture
	{
		get
		{
			if (DateTime.Now.Month != 4 || DateTime.Now.Day != 1)
			{
				return "CalamityMod/Items/Accessories/TheSponge";
			}
			return "CalamityMod/Items/Accessories/TheSpongeReal";
		}
	}

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ShieldDurabilityMax, ShieldActiveDamageReduction.ToPercent(), ShieldRechargeDelay.FramesToSeconds(), TotalShieldRechargeTime.FramesToSeconds());

	public int OwnerPlayer { get; set; }

	public float RenderDepth => 4f;

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
			if (player.Calamity().drawingParameters.SpongeShieldCharge <= 0f)
			{
				return false;
			}
			return true;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 30));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.sponge = true;
		player.noKnockback = true;
		calamityPlayer.spongeShieldVisible = !hideVisual;
		if (calamityPlayer.SpongeShieldDurability > 0)
		{
			player.endurance += ShieldActiveDamageReduction;
		}
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().spongeShieldVisible = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string adrenTooltip = (CalamityWorld.revenge ? this.GetLocalizedValue("ShieldAdren") : "");
		tooltips.FindAndReplace("[ADREN]", adrenTooltip);
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (Texture == "CalamityMod/Items/Accessories/TheSponge")
		{
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/TheSpongeShield", (AssetRequestMode)2).Value;
			spriteBatch.Draw(tex, base.Item.Center - Main.screenPosition + new Vector2(0f, 0f), (Rectangle?)Main.itemAnimations[base.Type].GetFrame(tex), Color.Cyan * 0.5f, 0f, new Vector2((float)tex.Width / 2f, (float)tex.Height / 30f * 0.8f), 1f, (SpriteEffects)0, 0f);
		}
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		return Texture != "CalamityMod/Items/Accessories/TheSponge";
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		if (Texture == "CalamityMod/Items/Accessories/TheSponge")
		{
			float wantedScale = 0.85f;
			Vector2 drawOffset = default(Vector2);
			((Vector2)(ref drawOffset))._002Ector(-2f, -1f);
			CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, wantedScale, drawOffset, (SpriteEffects)0);
			Texture2D tex = ModContent.Request<Texture2D>("CalamityMod/Items/Accessories/TheSpongeShield", (AssetRequestMode)2).Value;
			CalamityUtils.DrawInventoryCustomScale(spriteBatch, tex, position, Main.itemAnimations[base.Type].GetFrame(tex), Color.Cyan * 0.4f, itemColor, origin, scale, wantedScale, drawOffset, (SpriteEffects)0);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RoverDrive>().AddIngredient<MysteriousCircuitry>(10).AddIngredient<DubiousPlating>(20)
			.AddIngredient<CosmiliteBar>(5)
			.AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	public void DrawDyeableShader(SpriteBatch spriteBatch)
	{
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		if (OwnerPlayer < 0 || OwnerPlayer >= 255)
		{
			return;
		}
		Player player = Main.player[OwnerPlayer];
		if (player == null || player.outOfRange || player.dead)
		{
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.drawnAnyShieldThisFrame || modPlayer.drawingParameters.SpongeShieldCharge <= 0f)
		{
			return;
		}
		int i = player.whoAmI;
		float maxExtraScale = 0.025f;
		float extraScalePulseInterpolant = MathF.Pow(4f, MathF.Sin(Main.GlobalTimeWrappedHourly * 0.791f + (float)i) - 1f);
		float scale = 0.155f + maxExtraScale * extraScalePulseInterpolant;
		float visualShieldStrength = modPlayer.drawingParameters.SpongeShieldCharge;
		float noiseScale = MathHelper.Lerp(0.28f, 0.38f, 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 0.347f + (float)i));
		Effect shieldEffect = Filters.Scene["CalamityMod:RoverDriveShield"].GetShader().Shader;
		shieldEffect.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 0.0813f);
		shieldEffect.Parameters["blowUpPower"].SetValue(3f);
		shieldEffect.Parameters["blowUpSize"].SetValue(0.56f);
		shieldEffect.Parameters["noiseScale"].SetValue(noiseScale);
		float num = 0.9f + 0.1f * MathF.Sin(Main.GlobalTimeWrappedHourly * 1.95f);
		float minShieldStrengthOpacityMultiplier = 0.25f;
		float finalShieldOpacity = num * MathHelper.Lerp(minShieldStrengthOpacityMultiplier, 1f, visualShieldStrength);
		finalShieldOpacity *= CalamityClientConfig.Instance.EnergyShieldOpacity;
		shieldEffect.Parameters["shieldOpacity"].SetValue(finalShieldOpacity);
		shieldEffect.Parameters["shieldEdgeBlendStrenght"].SetValue(4f);
		Color shieldColor = default(Color);
		((Color)(ref shieldColor))._002Ector(24, 156, 204);
		Color primaryEdgeColor = shieldColor;
		Color secondaryEdgeColor = default(Color);
		((Color)(ref secondaryEdgeColor))._002Ector(34, 224, 227);
		Color edgeColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.2f, primaryEdgeColor, secondaryEdgeColor);
		shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref shieldColor)).ToVector3());
		shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
		using (Main.spriteBatch.Scope())
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, shieldEffect, Main.Transform);
			if (NoiseTex == null)
			{
				NoiseTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Neurons", (AssetRequestMode)2);
			}
			Vector2 pos = player.MountedCenter + player.gfxOffY * Vector2.UnitY - Main.screenPosition;
			Texture2D tex = NoiseTex.Value;
			Main.spriteBatch.Draw(tex, pos, (Rectangle?)null, Color.White, 0f, tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
		}
		modPlayer.drawnAnyShieldThisFrame = true;
	}
}
