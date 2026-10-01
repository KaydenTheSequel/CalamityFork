using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class NoDifficulty : DifficultyMode
{
	public override bool Enabled
	{
		get
		{
			return true;
		}
		set
		{
			if (!Main.GameModeInfo.IsJourneyMode)
			{
				Main.GameMode = ((!value) ? ((short)1) : ((short)0));
			}
			else
			{
				DifficultyModeSystem.AlignJourneyDifficultySlider();
			}
		}
	}

	public override Asset<Texture2D> Texture => _texture ?? (_texture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Classic", (AssetRequestMode)2));

	public override Asset<Texture2D> OutlineTexture => _outlineTexture ?? (_outlineTexture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Classic_Border", (AssetRequestMode)2));

	public override Asset<Texture2D> TextureDisabled => _textureDisabled ?? (_textureDisabled = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Classic_Off", (AssetRequestMode)2));

	public override SoundStyle ActivationSound
	{
		get
		{
			SoundStyle valueOrDefault = _activationSound.GetValueOrDefault();
			if (!_activationSound.HasValue)
			{
				valueOrDefault = SoundID.MenuTick with
				{
					Volume = 1f
				};
				_activationSound = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
	}

	public override int BackBoneGameModeID => 0;

	public override float DifficultyScale => 0f;

	public override LocalizedText Name => Language.GetText("UI.Normal");

	public override Color ChatTextColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}
	}

	public override LocalizedText ShortDescription => CalamityUtils.GetText("UI.ClassicInfo");

	public override FTWDisplayMode GetForTheWorthyDisplay => FTWDisplayMode.NotForTheWorthy;
}
