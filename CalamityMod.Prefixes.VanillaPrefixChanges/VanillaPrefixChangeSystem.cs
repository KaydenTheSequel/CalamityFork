using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Prefixes.VanillaPrefixChanges.Stats;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Prefixes.VanillaPrefixChanges;

public sealed class VanillaPrefixChangeSystem : ModSystem
{
	public sealed class VanillaPrefixChangeTooltipModify : GlobalItem
	{
		public override bool InstancePerEntity => false;

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (!PrefixReworkEnabled || !PrefixChanges.TryGetValue(item.prefix, out var change))
			{
				return;
			}
			TooltipLine tooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Name.Equals(change.TargetTooltipName));
			if (tooltip != null)
			{
				tooltip.Text = string.Empty;
				IEnumerator<IVanillaPrefixStat> stats = change.PopulateStats();
				while (stats.MoveNext())
				{
					stats.Current.ModifyTooltip(tooltip);
				}
				change.PostModifyTooltip(tooltip);
				tooltip.Text = tooltip.Text.Trim();
			}
		}
	}

	public static bool PrefixReworkEnabled = true;

	public static readonly Dictionary<int, VanillaPrefixChange> PrefixChanges = new Dictionary<int, VanillaPrefixChange>();

	public override void Load()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		On_Player.GrantPrefixBenefits += new hook_GrantPrefixBenefits(OnGrantBenefits);
		IL_Item.Prefix += new Manipulator(VanillaPrefixValueOverride);
	}

	public override void Unload()
	{
		PrefixChanges.Clear();
	}

	private void VanillaPrefixValueOverride(ILContext il)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ILCursor cursor = new ILCursor(il);
		if (!cursor.TryGotoNext(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchCallOrCallvirt<ModPrefix>(x, "ModifyValue")
		}))
		{
			CalamityMod.Log.ILFailure("Vanilla Prefix Override", "Unable to locate callvirt (ModPrefix.ModifyValue)");
			return;
		}
		int multLocaIdx = default(int);
		if (!ILPatternMatchingExt.MatchLdloca(cursor.Prev, ref multLocaIdx))
		{
			CalamityMod.Log.ILFailure("Vanilla Prefix Override", "Unable to locate Ldloca (mult)");
			return;
		}
		if (!cursor.TryGotoPrev(new Func<Instruction, bool>[1]
		{
			(Instruction x) => ILPatternMatchingExt.MatchLdsfld<PrefixID>(x, "Count")
		}))
		{
			CalamityMod.Log.ILFailure("Vanilla Prefix Override", "Unable to locate Ldsfld (PrefixID.Count)");
			return;
		}
		int prefixLocaIdx = default(int);
		if (!ILPatternMatchingExt.MatchLdloc(cursor.Prev, ref prefixLocaIdx))
		{
			CalamityMod.Log.ILFailure("Vanilla Prefix Override", "Unable to locate Ldloc (prefix)");
			return;
		}
		cursor.GotoPrev((MoveType)1, Array.Empty<Func<Instruction, bool>>());
		cursor.EmitLdloc(prefixLocaIdx);
		cursor.EmitLdloca(multLocaIdx);
		cursor.EmitDelegate<_003C_003EA_007B00000008_007D<int, float>>((_003C_003EA_007B00000008_007D<int, float>)delegate(int prefixID, ref float value)
		{
			if (PrefixReworkEnabled && PrefixChanges.TryGetValue(prefixID, out var value2))
			{
				value2.ModifyValue(ref value);
			}
		});
	}

	private void OnGrantBenefits(orig_GrantPrefixBenefits orig, Player self, Item item)
	{
		VanillaPrefixChange prefixChange;
		if (!PrefixReworkEnabled)
		{
			orig.Invoke(self, item);
		}
		else if (PrefixChanges.TryGetValue(item.prefix, out prefixChange))
		{
			IEnumerator<IVanillaPrefixStat> stats = prefixChange.PopulateStats();
			while (stats.MoveNext())
			{
				stats.Current.ApplyEffects(self);
			}
			prefixChange.PostApplyEffects(self);
		}
		else
		{
			orig.Invoke(self, item);
		}
	}
}
