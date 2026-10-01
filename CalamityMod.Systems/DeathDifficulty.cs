using System.Collections.Generic;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public class DeathDifficulty : DifficultyMode
{
	public override bool Enabled
	{
		get
		{
			return CalamityWorld.death;
		}
		set
		{
			if (Main.getGoodWorld)
			{
				if (!Main.GameModeInfo.IsJourneyMode)
				{
					Main.GameMode = (value ? ((short)1) : ((short)0));
				}
				else
				{
					DifficultyModeSystem.AlignJourneyDifficultySlider();
				}
				CalamityWorld.death = value;
			}
			else
			{
				CalamityWorld.death = value;
				if (value && !Main.GameModeInfo.IsJourneyMode)
				{
					Main.GameMode = BackBoneGameModeID;
				}
			}
		}
	}

	public override Asset<Texture2D> Texture => _texture ?? (_texture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Death", (AssetRequestMode)2));

	public override Asset<Texture2D> OutlineTexture => _outlineTexture ?? (_outlineTexture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Death_Border", (AssetRequestMode)2));

	public override Asset<Texture2D> TextureDisabled => _textureDisabled ?? (_textureDisabled = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Death_Off", (AssetRequestMode)2));

	public override SoundStyle ActivationSound
	{
		get
		{
			SoundStyle valueOrDefault = _activationSound.GetValueOrDefault();
			if (!_activationSound.HasValue)
			{
				valueOrDefault = new SoundStyle("CalamityMod/Sounds/Custom/DifficultySelection/Death_Mode_Select");
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

	public override LocalizedText Name => CalamityUtils.GetText("UI.Death");

	public override Color ChatTextColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(192, 64, 219);
		}
	}

	public override LocalizedText ShortDescription => CalamityUtils.GetText("UI.DeathShortInfo");

	public override LocalizedText ExpandedDescription => CalamityUtils.GetText("UI.DeathExpandedInfo");

	public override int[] FavoredDifficultyAtTier(int tier)
	{
		DifficultyMode[] tierList = DifficultyModeSystem.DifficultyTiers[tier];
		List<int> difficulties = new List<int>();
		for (int i = 0; i < tierList.Length; i++)
		{
			if (tierList[i] is MasterDifficulty || tierList[i] is RevengeanceDifficulty)
			{
				difficulties.Add(i);
			}
		}
		if (difficulties.Count <= 0)
		{
			difficulties.Add(0);
		}
		return difficulties.ToArray();
	}
}
