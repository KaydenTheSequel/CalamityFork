using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.BaseItems;

public abstract class TransformationAccessory : ModItem
{
	public bool IsForced;

	public virtual string AssetPath => "CalamityMod/Items/Accessories/Vanity/";

	public virtual (EquipType Type, string AssetName, string EquipName)[] EquipSlots => Array.Empty<(EquipType, string, string)>();

	public virtual float Priority => 1f;

	public virtual Func<Player, bool> ShouldTransform => (Player player) => true;

	public virtual bool ShouldHideAccessories => false;

	public virtual (SoundStyle sound, int delay)? HurtSound(Player p)
	{
		return null;
	}

	public virtual void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
	{
	}

	public virtual void ArmorIDSets()
	{
		if (EquipSlots.Any(((EquipType Type, string AssetName, string EquipName) s) => s.Type == EquipType.Head))
		{
			string name = EquipSlots.First(((EquipType Type, string AssetName, string EquipName) s) => s.Type == EquipType.Head).EquipName;
			int equipSlotHead = EquipLoader.GetEquipSlot(base.Mod, name ?? Name, EquipType.Head);
			ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
		}
		if (EquipSlots.Any(((EquipType Type, string AssetName, string EquipName) s) => s.Type == EquipType.Body))
		{
			string name2 = EquipSlots.First(((EquipType Type, string AssetName, string EquipName) s) => s.Type == EquipType.Body).EquipName;
			int equipSlotBody = EquipLoader.GetEquipSlot(base.Mod, name2 ?? Name, EquipType.Body);
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
			ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
		}
		if (EquipSlots.Any(((EquipType Type, string AssetName, string EquipName) s) => s.Type == EquipType.Legs))
		{
			string name3 = EquipSlots.First(((EquipType Type, string AssetName, string EquipName) s) => s.Type == EquipType.Legs).EquipName;
			int equipSlotLegs = EquipLoader.GetEquipSlot(base.Mod, name3 ?? Name, EquipType.Legs);
			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlotLegs] = true;
		}
	}

	public virtual bool CustomSetEquipType(Player player, EquipType type, Mod mod, string name)
	{
		return false;
	}

	public virtual void TransformFrameEffects(Player player)
	{
	}

	public virtual void TransformPostUpdate(Player player)
	{
	}

	public override void Load()
	{
		if (Main.netMode == 2)
		{
			return;
		}
		(EquipType, string, string)[] equipSlots = EquipSlots;
		for (int i = 0; i < equipSlots.Length; i++)
		{
			(EquipType, string, string) v = equipSlots[i];
			if (v.Item2 != null)
			{
				Mod mod = base.Mod;
				string assetPath = AssetPath;
				string item = v.Item2;
				var (equipType, _, _) = v;
				EquipLoader.AddEquipTexture(mod, assetPath + item + "_" + equipType, v.Item1, this, v.Item3);
			}
		}
	}

	public override void SetStaticDefaults()
	{
		if (Main.netMode != 2)
		{
			ArmorIDSets();
		}
	}
}
