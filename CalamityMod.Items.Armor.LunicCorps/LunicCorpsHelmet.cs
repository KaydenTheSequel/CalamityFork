using System;
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
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.LunicCorps;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class LunicCorpsHelmet : ModItem, ILocalizedModType, IModType, IDyeableShaderRenderer
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

	public static float NonArrowDamageBoost = 0.15f;

	public static float SetBonusJumpSpeedBoost = 1f;

	public static int ShieldDurabilityMax = 50;

	public static int ShieldRechargeDelay = CalamityUtils.SecondsToFrames(5);

	public static int TotalShieldRechargeTime = CalamityUtils.SecondsToFrames(2);

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(NonArrowDamageBoost.ToPercent());

	public int OwnerPlayer { get; set; }

	public float RenderDepth => 2f;

	public bool ShaderIsDyeable => false;

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
			if (player.Calamity().drawingParameters.LunicShieldCharge <= 0f)
			{
				return false;
			}
			return true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.defense = 14;
		base.Item.rare = 9;
		base.Item.Calamity().donorItem = true;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<LunicCorpsVest>())
		{
			return legs.type == ModContent.ItemType<LunicCorpsBoots>();
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().lunicCorpsSet = true;
		Color AbilityBriefColor = Color.Lerp(new Color(240, 207, 60), new Color(70, 205, 251), 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 3f));
		string adrenTooltip = (CalamityWorld.revenge ? ("\n" + this.GetLocalizedValue("ShieldAdren")) : "");
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusJumpSpeedBoost.ToJumpSpeedPercent(), AbilityBriefColor.Hex3(), ShieldDurabilityMax, adrenTooltip, ShieldRechargeDelay.FramesToSeconds(), TotalShieldRechargeTime.FramesToSeconds());
		player.jumpSpeedBoost += SetBonusJumpSpeedBoost;
	}

	public override void UpdateEquip(Player player)
	{
		player.bulletDamage += NonArrowDamageBoost;
		player.specialistDamage += NonArrowDamageBoost;
		player.nightVision = true;
		player.detectCreature = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3109).AddIngredient<AstralBar>(6).AddIngredient(1006, 6)
			.AddIngredient(170, 20)
			.AddTile(412)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<LunicCorpsBoots>())
			.Register();
	}

	public void DrawDyeableShader(SpriteBatch spriteBatch)
	{
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		if (OwnerPlayer < 0 || OwnerPlayer >= 255)
		{
			return;
		}
		Player player = Main.player[OwnerPlayer];
		if (player.outOfRange || player.dead)
		{
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.drawnAnyShieldThisFrame || modPlayer.drawingParameters.LunicShieldCharge <= 0f)
		{
			return;
		}
		int i = player.whoAmI;
		float maxExtraScale = 0.013f;
		float extraScalePulseInterpolant = MathF.Pow(12f, MathF.Sin(Main.GlobalTimeWrappedHourly * 1.6f + (float)i) - 1f);
		float scale = 0.11f + maxExtraScale * extraScalePulseInterpolant;
		float visualShieldStrength = modPlayer.drawingParameters.LunicShieldCharge;
		float noiseScale = MathHelper.Lerp(0.65f, 0.75f, 0.5f + 0.5f * MathF.Sin(Main.GlobalTimeWrappedHourly * 0.87f + (float)i));
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
		Color shieldColor = default(Color);
		((Color)(ref shieldColor))._002Ector(201, 180, 129);
		Color primaryEdgeColor = default(Color);
		((Color)(ref primaryEdgeColor))._002Ector(232, 212, 175);
		Color secondaryEdgeColor = default(Color);
		((Color)(ref secondaryEdgeColor))._002Ector(237, 205, 145);
		Color edgeColor = CalamityUtils.MulticolorLerp(Main.GlobalTimeWrappedHourly * 0.2f, primaryEdgeColor, secondaryEdgeColor);
		shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref shieldColor)).ToVector3());
		shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
		using (spriteBatch.Scope())
		{
			spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, shieldEffect, Main.Transform);
			if (NoiseTex == null)
			{
				NoiseTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoronoiShapes2", (AssetRequestMode)2);
			}
			Vector2 pos = player.MountedCenter + player.gfxOffY * Vector2.UnitY - Main.screenPosition;
			Texture2D tex = NoiseTex.Value;
			spriteBatch.Draw(tex, pos, (Rectangle?)null, Color.White, 0f, tex.Size() / 2f, scale, (SpriteEffects)0, 0f);
			spriteBatch.End();
		}
		modPlayer.drawnAnyShieldThisFrame = true;
	}
}
