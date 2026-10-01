using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class LegendaryDifficulty : DifficultyMode
{
	public override bool Enabled
	{
		get
		{
			return CalamityWorld.LegendaryMode;
		}
		set
		{
			if (!Main.GameModeInfo.IsJourneyMode)
			{
				Main.GameMode = ((!value) ? 1 : 2);
			}
			else
			{
				DifficultyModeSystem.AlignJourneyDifficultySlider();
			}
		}
	}

	public override Asset<Texture2D> Texture => _texture ?? (_texture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Legendary", (AssetRequestMode)2));

	public override Asset<Texture2D> OutlineTexture => _outlineTexture ?? (_outlineTexture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Legendary_Border", (AssetRequestMode)2));

	public override Asset<Texture2D> TextureDisabled => _textureDisabled ?? (_textureDisabled = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Legendary_Off", (AssetRequestMode)2));

	public override SoundStyle ActivationSound
	{
		get
		{
			SoundStyle valueOrDefault = _activationSound.GetValueOrDefault();
			if (!_activationSound.HasValue)
			{
				valueOrDefault = new SoundStyle("CalamityMod/Sounds/Custom/DifficultySelection/Legendary_Mode_Select");
				_activationSound = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
	}

	public override int BackBoneGameModeID => 2;

	public override float DifficultyScale => 0.5f;

	public override LocalizedText Name => Language.GetText("UI.Legendary");

	public override Color ChatTextColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Main.legendaryModeColor;
		}
	}

	public override LocalizedText ShortDescription => CalamityUtils.GetText("UI.LegendaryShortInfo");

	public override LocalizedText ExpandedDescription => CalamityUtils.GetText("UI.LegendaryExpandedInfo");

	public override FTWDisplayMode GetForTheWorthyDisplay => FTWDisplayMode.OnlyForTheWorthy;
}
