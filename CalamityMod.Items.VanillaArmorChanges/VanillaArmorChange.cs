using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public abstract class VanillaArmorChange : ILoadable
{
	public abstract int? HeadPieceID { get; }

	public abstract int? BodyPieceID { get; }

	public abstract int? LegPieceID { get; }

	public virtual int[] AlternativeHeadPieceIDs => new int[0];

	public virtual int[] AlternativeBodyPieceIDs => new int[0];

	public virtual int[] AlternativeLegPieceIDs => new int[0];

	public virtual string ArmorSetName => null;

	public virtual bool NeedsToCreateSetBonusTextManually => true;

	public virtual void UpdateSetBonusText(ref string setBonusText)
	{
	}

	public virtual void ApplyHeadPieceEffect(Player player)
	{
	}

	public virtual void ApplyBodyPieceEffect(Player player)
	{
	}

	public virtual void ApplyLegPieceEffect(Player player)
	{
	}

	public virtual void ApplyArmorSetBonus(Player player)
	{
	}

	public bool IsWearingEntireSet(Player player)
	{
		if ((HeadPieceID ?? player.armor[0].type) != player.armor[0].type && !AlternativeHeadPieceIDs.Contains(player.armor[0].type))
		{
			return false;
		}
		if ((BodyPieceID ?? player.armor[1].type) != player.armor[1].type && !AlternativeBodyPieceIDs.Contains(player.armor[1].type))
		{
			return false;
		}
		if ((LegPieceID ?? player.armor[2].type) != player.armor[2].type && !AlternativeLegPieceIDs.Contains(player.armor[2].type))
		{
			return false;
		}
		return true;
	}

	public void ApplyIndividualPieceEffects(Player player)
	{
		if (HeadPieceID.GetValueOrDefault() == player.armor[0].type || AlternativeHeadPieceIDs.Contains(player.armor[0].type))
		{
			ApplyHeadPieceEffect(player);
		}
		if (BodyPieceID.GetValueOrDefault() == player.armor[1].type || AlternativeBodyPieceIDs.Contains(player.armor[1].type))
		{
			ApplyBodyPieceEffect(player);
		}
		if (LegPieceID.GetValueOrDefault() == player.armor[2].type || AlternativeLegPieceIDs.Contains(player.armor[2].type))
		{
			ApplyLegPieceEffect(player);
		}
	}

	public void Load(Mod mod)
	{
		VanillaArmorChangeManager.ArmorChanges.Add(this);
	}

	public void Unload()
	{
	}
}
