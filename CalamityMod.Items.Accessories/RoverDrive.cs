using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.DataStructures;
using CalamityMod.Items.Materials;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RoverDrive : ModItem, ILocalizedModType, IModType, IDyeableShaderRenderer
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

	public static int ShieldDurabilityMax = 20;

	public static int ShieldRechargeDelay = CalamityUtils.SecondsToFrames(10);

	public static int TotalShieldRechargeTime = CalamityUtils.SecondsToFrames(5);

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ShieldDurabilityMax, ShieldRechargeDelay.FramesToSeconds(), TotalShieldRechargeTime.FramesToSeconds());

	public int OwnerPlayer { get; set; }

	public float RenderDepth => 1f;

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
			if (player.Calamity().drawingParameters.RoverShieldCharge <= 0f)
			{
				return false;
			}
			return true;
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ExtractinatorMode[base.Type] = base.Item.type;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 30;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
		base.Item.accessory = true;
		base.Item.MakeUsableWithChlorophyteExtractinator();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.roverDrive = true;
		calamityPlayer.roverDriveShieldVisible = !hideVisual;
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().roverDriveShieldVisible = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string adrenTooltip = (CalamityWorld.revenge ? this.GetLocalizedValue("ShieldAdren") : "");
		tooltips.FindAndReplace("[ADREN]", adrenTooltip);
	}

	public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		resultType = ModContent.ItemType<WulfrumMetalScrap>();
		resultStack = Main.rand.Next(3, 6);
		if (Main.rand.NextFloat() > 0.8f)
		{
			resultStack = 1;
			resultType = ModContent.ItemType<EnergyCore>();
		}
	}

	public void DrawDyeableShader(SpriteBatch spriteBatch)
	{
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
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
		if (modPlayer.drawnAnyShieldThisFrame || modPlayer.drawingParameters.RoverShieldCharge <= 0f)
		{
			return;
		}
		float scale = 0.15f + 0.03f * (0.5f + 0.5f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f + (float)player.whoAmI * 0.2f));
		float shieldStrength = modPlayer.drawingParameters.RoverShieldCharge;
		float noiseScale = MathHelper.Lerp(0.4f, 0.8f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.3f) * 0.5f + 0.5f);
		Effect shieldEffect = Filters.Scene["CalamityMod:RoverDriveShield"].GetShader().Shader;
		shieldEffect.Parameters["time"].SetValue(Main.GlobalTimeWrappedHourly * 0.24f);
		shieldEffect.Parameters["blowUpPower"].SetValue(2.5f);
		shieldEffect.Parameters["blowUpSize"].SetValue(0.5f);
		shieldEffect.Parameters["noiseScale"].SetValue(noiseScale);
		float finalShieldOpacity = (0.9f + 0.1f * (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f)) * (0.5f + 0.5f * shieldStrength);
		finalShieldOpacity *= CalamityClientConfig.Instance.EnergyShieldOpacity;
		shieldEffect.Parameters["shieldOpacity"].SetValue(finalShieldOpacity);
		shieldEffect.Parameters["shieldEdgeBlendStrenght"].SetValue(4f);
		Color blueTint = default(Color);
		((Color)(ref blueTint))._002Ector(51, 102, 255);
		Color cyanTint = default(Color);
		((Color)(ref cyanTint))._002Ector(71, 202, 255);
		Color wulfGreen = new Color(194, 255, 67) * 0.8f;
		Color edgeColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.2f, blueTint, cyanTint, wulfGreen);
		Color shieldColor = blueTint;
		shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref shieldColor)).ToVector3());
		shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
		using (spriteBatch.Scope())
		{
			spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, shieldEffect, Main.Transform);
			if (NoiseTex == null)
			{
				NoiseTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/TechyNoise", (AssetRequestMode)2);
			}
			Vector2 pos = player.MountedCenter + player.gfxOffY * Vector2.UnitY - Main.screenPosition;
			Texture2D tex = NoiseTex.Value;
			spriteBatch.Draw(tex, pos, (Rectangle?)null, Color.White, 0f, tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
			spriteBatch.End();
		}
		modPlayer.drawnAnyShieldThisFrame = true;
	}
}
