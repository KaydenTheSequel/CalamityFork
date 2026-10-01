using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Systems;

public abstract class DifficultyMode : ModType
{
	public enum FTWDisplayMode
	{
		Always,
		OnlyForTheWorthy,
		NotForTheWorthy
	}

	protected Asset<Texture2D> _texture;

	protected Asset<Texture2D> _textureDisabled;

	protected Asset<Texture2D> _outlineTexture;

	protected SoundStyle? _activationSound;

	internal int _difficultyTier;

	public abstract bool Enabled { get; set; }

	public abstract Asset<Texture2D> Texture { get; }

	public abstract Asset<Texture2D> TextureDisabled { get; }

	public abstract Asset<Texture2D> OutlineTexture { get; }

	public virtual FTWDisplayMode GetForTheWorthyDisplay { get; }

	public abstract SoundStyle ActivationSound { get; }

	public abstract int BackBoneGameModeID { get; }

	public abstract float DifficultyScale { get; }

	public new abstract LocalizedText Name { get; }

	public abstract Color ChatTextColor { get; }

	public abstract LocalizedText ShortDescription { get; }

	public virtual LocalizedText ExpandedDescription => LocalizedText.Empty;

	public virtual LocalizedText FTWName { get; }

	public virtual Color? FTWTextColor { get; }

	protected sealed override void Register()
	{
		ModTypeLookup<DifficultyMode>.Register(this);
	}

	public virtual bool RequiresDifficulty(DifficultyMode mode)
	{
		return false;
	}

	public virtual int[] FavoredDifficultyAtTier(int tier)
	{
		return new int[1];
	}

	public virtual bool IsBasedOn(DifficultyMode mode)
	{
		return false;
	}

	public bool CountAs<Difficulty>() where Difficulty : DifficultyMode
	{
		return CountAs(ModContent.GetInstance<Difficulty>());
	}

	public bool CountAs(DifficultyMode mode)
	{
		if (mode == this)
		{
			return true;
		}
		if (IsBasedOn(mode))
		{
			return true;
		}
		return false;
	}
}
