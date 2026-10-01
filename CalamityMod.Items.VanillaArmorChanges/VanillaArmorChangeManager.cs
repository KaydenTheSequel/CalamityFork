using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.VanillaArmorChanges;

public class VanillaArmorChangeManager : ModSystem
{
	internal static List<VanillaArmorChange> ArmorChanges = new List<VanillaArmorChange>();

	public override void Unload()
	{
		ArmorChanges = null;
	}

	public static void ApplySetBonusTooltipChanges(Item checkItem, ref string setBonusText)
	{
		for (int i = 0; i < ArmorChanges.Count; i++)
		{
			bool num = ArmorChanges[i].HeadPieceID.GetValueOrDefault() == checkItem.type || ArmorChanges[i].AlternativeHeadPieceIDs.Contains(checkItem.type);
			bool isValidBodyPiece = ArmorChanges[i].BodyPieceID.GetValueOrDefault() == checkItem.type || ArmorChanges[i].AlternativeBodyPieceIDs.Contains(checkItem.type);
			bool isValidLegPiece = ArmorChanges[i].LegPieceID.GetValueOrDefault() == checkItem.type || ArmorChanges[i].AlternativeLegPieceIDs.Contains(checkItem.type);
			if ((num | isValidBodyPiece | isValidLegPiece) && !ArmorChanges[i].NeedsToCreateSetBonusTextManually)
			{
				ArmorChanges[i].UpdateSetBonusText(ref setBonusText);
			}
		}
	}

	public static void CreateTooltipManuallyAsNecessary(Player player)
	{
		for (int i = 0; i < ArmorChanges.Count; i++)
		{
			if (ArmorChanges[i].IsWearingEntireSet(player) && ArmorChanges[i].NeedsToCreateSetBonusTextManually)
			{
				ArmorChanges[i].UpdateSetBonusText(ref player.setBonus);
				break;
			}
		}
	}

	public static string GetSetBonusName(Player player)
	{
		for (int i = 0; i < ArmorChanges.Count; i++)
		{
			if (ArmorChanges[i].IsWearingEntireSet(player))
			{
				return ArmorChanges[i].ArmorSetName;
			}
		}
		return string.Empty;
	}

	public static void ApplyPotentialEffectsTo(Player player)
	{
		for (int i = 0; i < ArmorChanges.Count; i++)
		{
			ArmorChanges[i].ApplyIndividualPieceEffects(player);
			if (ArmorChanges[i].IsWearingEntireSet(player))
			{
				ArmorChanges[i].ApplyArmorSetBonus(player);
			}
		}
	}
}
