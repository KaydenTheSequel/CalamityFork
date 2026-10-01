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

public class MaliceDifficulty : DifficultyMode
{
	public override bool Enabled
	{
		get
		{
			if (!Main.getGoodWorld)
			{
				return false;
			}
			if (CalamityWorld.death)
			{
				return CalamityWorld.LegendaryMode;
			}
			return false;
		}
		set
		{
			if (Main.getGoodWorld)
			{
				if (!Main.GameModeInfo.IsJourneyMode)
				{
					Main.GameMode = ((!value) ? 1 : 2);
				}
				else
				{
					DifficultyModeSystem.AlignJourneyDifficultySlider();
				}
				CalamityWorld.revenge = value;
				CalamityWorld.death = value;
			}
		}
	}

	public override Asset<Texture2D> Texture => _texture ?? (_texture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Malice", (AssetRequestMode)2));

	public override Asset<Texture2D> OutlineTexture => _outlineTexture ?? (_outlineTexture = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Malice_Border", (AssetRequestMode)2));

	public override Asset<Texture2D> TextureDisabled => _textureDisabled ?? (_textureDisabled = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/ModeIndicator_Malice_Off", (AssetRequestMode)2));

	public override SoundStyle ActivationSound
	{
		get
		{
			SoundStyle valueOrDefault = _activationSound.GetValueOrDefault();
			if (!_activationSound.HasValue)
			{
				valueOrDefault = new SoundStyle("CalamityMod/Sounds/Custom/DifficultySelection/Malice_Mode_Select");
				_activationSound = valueOrDefault;
				return valueOrDefault;
			}
			return valueOrDefault;
		}
	}

	public override int BackBoneGameModeID => 2;

	public override float DifficultyScale => 0.5f;

	public override LocalizedText Name => CalamityUtils.GetText("UI.Malice");

	public override Color ChatTextColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(240, 128, 128);
		}
	}

	public override LocalizedText ShortDescription => CalamityUtils.GetText("UI.MaliceShortInfo");

	public override LocalizedText ExpandedDescription => CalamityUtils.GetText("UI.MaliceExpandedInfo");

	public override FTWDisplayMode GetForTheWorthyDisplay => FTWDisplayMode.OnlyForTheWorthy;

	public override int[] FavoredDifficultyAtTier(int tier)
	{
		DifficultyMode[] tierList = DifficultyModeSystem.DifficultyTiers[tier];
		List<int> difficulties = new List<int>();
		for (int i = 0; i < tierList.Length; i++)
		{
			if (tierList[i] is MasterDifficulty || tierList[i] is DeathDifficulty)
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
