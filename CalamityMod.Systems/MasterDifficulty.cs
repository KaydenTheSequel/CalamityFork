using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class MasterDifficulty : DifficultyMode
{
	public override bool Enabled
	{
		get
		{
			return Main.masterMode;
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

	public override Asset<Texture2D> Texture => _texture ?? (_texture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Master", (AssetRequestMode)2));

	public override Asset<Texture2D> OutlineTexture => _outlineTexture ?? (_outlineTexture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Master_Border", (AssetRequestMode)2));

	public override Asset<Texture2D> TextureDisabled => _textureDisabled ?? (_textureDisabled = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Master_Off", (AssetRequestMode)2));

	public override SoundStyle ActivationSound
	{
		get
		{
			SoundStyle valueOrDefault = _activationSound.GetValueOrDefault();
			if (!_activationSound.HasValue)
			{
				valueOrDefault = SoundID.NPCDeath10;
				_activationSound = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
	}

	public override int BackBoneGameModeID
	{
		get
		{
			if (!Main.getGoodWorld)
			{
				return 2;
			}
			return 1;
		}
	}

	public override float DifficultyScale => 0.25f;

	public override LocalizedText Name => Language.GetText("UI.Master");

	public override Color ChatTextColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Main.hcColor;
		}
	}

	public override LocalizedText ShortDescription => CalamityUtils.GetText("UI.MasterShortInfo");

	public override LocalizedText ExpandedDescription => CalamityUtils.GetText("UI.MasterExpandedInfo");
}
