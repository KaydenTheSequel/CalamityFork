using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class RevengeanceDifficulty : DifficultyMode
{
	public override bool Enabled
	{
		get
		{
			return CalamityWorld.revenge;
		}
		set
		{
			CalamityWorld.revenge = value;
			if (value && !Main.GameModeInfo.IsJourneyMode)
			{
				Main.GameMode = BackBoneGameModeID;
			}
		}
	}

	public override Asset<Texture2D> Texture => _texture ?? (_texture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Rev", (AssetRequestMode)2));

	public override Asset<Texture2D> OutlineTexture => _outlineTexture ?? (_outlineTexture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Rev_Border", (AssetRequestMode)2));

	public override Asset<Texture2D> TextureDisabled => _textureDisabled ?? (_textureDisabled = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Rev_Off", (AssetRequestMode)2));

	public override SoundStyle ActivationSound
	{
		get
		{
			SoundStyle valueOrDefault = _activationSound.GetValueOrDefault();
			if (!_activationSound.HasValue)
			{
				valueOrDefault = new SoundStyle("CalamityMod/Sounds/Custom/DifficultySelection/Revengeance_Mode_Select");
				_activationSound = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
	}

	public override int BackBoneGameModeID => (!Main.getGoodWorld) ? ((short)1) : ((short)0);

	public override float DifficultyScale => 0.1f;

	public override LocalizedText Name => CalamityUtils.GetText("UI.Revengeance");

	public override Color ChatTextColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(211, 42, 42);
		}
	}

	public override LocalizedText ShortDescription => CalamityUtils.GetText("UI.RevengeanceShortInfo");

	public override LocalizedText ExpandedDescription
	{
		get
		{
			string rageKey = "[c/FFCE85:" + CalamityKeybinds.RageHotKey.TooltipHotkeyString() + "]";
			string adrenKey = "[c/79DFBF:" + CalamityKeybinds.AdrenalineHotKey.TooltipHotkeyString() + "]";
			return CalamityUtils.GetText("UI.RevengeanceExpandedInfo").WithFormatArgs(rageKey, adrenKey);
		}
	}
}
