using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.BaseItems;

public class TransformationPlayer : ModPlayer
{
	internal TransformationAccessory currentTransformation;

	internal TransformationAccessory previousTransformation;

	internal int previousHighest = -1;

	internal int currentHighest = -1;

	public int Type
	{
		get
		{
			if (currentTransformation != null)
			{
				return currentTransformation.Type;
			}
			return -1;
		}
		set
		{
			ModItem item = ModContent.GetModItem(value);
			if (item != null && item is TransformationAccessory trans)
			{
				currentTransformation = trans;
				currentTransformation.IsForced = true;
			}
			else
			{
				currentTransformation = null;
			}
		}
	}

	public override void Load()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		On_Player.ResetVisibleAccessories += new hook_ResetVisibleAccessories(ResetTransformation);
		On_Player.UpdateVisibleAccessory += new hook_UpdateVisibleAccessory(SetTransformationItem);
	}

	private void ResetTransformation(orig_ResetVisibleAccessories orig, Player self)
	{
		orig.Invoke(self);
		if (!self.dead)
		{
			Reset(self);
			return;
		}
		TransformationAccessory trans = (self.Transformation().currentTransformation = self.Transformation().previousTransformation);
		if (trans != null)
		{
			EquipTransformation(trans, self, base.Mod);
		}
	}

	private static void Reset(Player p)
	{
		p.Transformation().previousHighest = p.Transformation().currentHighest;
		p.Transformation().currentHighest = -1;
		if (p.Transformation().currentTransformation != null && !p.Transformation().currentTransformation.IsForced)
		{
			p.Transformation().previousTransformation = p.Transformation().currentTransformation;
			p.Transformation().currentTransformation = null;
		}
	}

	private void SetTransformationItem(orig_UpdateVisibleAccessory orig, Player self, int itemSlot, Item item, bool modded)
	{
		orig.Invoke(self, itemSlot, item, modded);
		if ((self.Transformation().currentTransformation == null || !self.Transformation().currentTransformation.IsForced) && item.ModItem is TransformationAccessory transformation && transformation.ShouldTransform(self) && (currentTransformation == null || transformation.Priority >= currentTransformation.Priority))
		{
			self.Transformation().currentTransformation = transformation;
		}
		if (self.Transformation().currentHighest < itemSlot)
		{
			self.Transformation().currentHighest = itemSlot;
		}
		if (self.Transformation().currentTransformation != null && itemSlot == self.Transformation().previousHighest)
		{
			EquipTransformation(self.Transformation().currentTransformation, self, base.Mod);
		}
	}

	public override void UpdateAutopause()
	{
		if (currentTransformation != null && !currentTransformation.CustomSetEquipType(base.Player, EquipType.Head, base.Mod, currentTransformation.Name))
		{
			(EquipType Type, string AssetName, string EquipName) tuple = currentTransformation.EquipSlots.First(((EquipType Type, string AssetName, string EquipName) v) => v.Type == EquipType.Head);
			string AssetName = tuple.AssetName;
			string EquipName = tuple.EquipName;
			string name = ((EquipName == null && AssetName == null) ? null : (EquipName ?? currentTransformation.Name));
			SetEquipSlot(base.Player, EquipType.Head, base.Mod, name);
		}
		Reset(base.Player);
	}

	public override void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
	{
		if (currentTransformation != null)
		{
			currentTransformation.ModifyDrawInfo(ref drawInfo);
		}
	}

	public override void FrameEffects()
	{
		if (currentTransformation != null)
		{
			currentTransformation.TransformFrameEffects(base.Player);
			if (currentTransformation.ShouldHideAccessories)
			{
				base.Player.HideAccessories();
			}
		}
	}

	public override void PostUpdate()
	{
		currentTransformation?.TransformPostUpdate(base.Player);
	}

	public static void SetEquipSlot(Player player, EquipType type, Mod mod, string name)
	{
		switch (type)
		{
		case EquipType.Head:
			player.head = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Body:
			player.body = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Legs:
			player.legs = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.HandsOn:
			player.handon = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.HandsOff:
			player.handoff = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Back:
			player.back = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Front:
			player.front = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Shoes:
			player.shoe = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Waist:
			player.waist = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Wings:
			player.wings = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Shield:
			player.shield = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Neck:
			player.neck = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Face:
			player.face = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Balloon:
			player.balloon = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		case EquipType.Beard:
			player.beard = ((name == null) ? (-1) : EquipLoader.GetEquipSlot(mod, name, type));
			break;
		}
	}

	public static int GetEquipSlot(Player player, EquipType type)
	{
		return type switch
		{
			EquipType.Head => player.head, 
			EquipType.Body => player.body, 
			EquipType.Legs => player.legs, 
			EquipType.HandsOn => player.handon, 
			EquipType.HandsOff => player.handoff, 
			EquipType.Back => player.back, 
			EquipType.Front => player.front, 
			EquipType.Shoes => player.shoe, 
			EquipType.Waist => player.waist, 
			EquipType.Wings => player.wings, 
			EquipType.Shield => player.shield, 
			EquipType.Neck => player.neck, 
			EquipType.Face => player.face, 
			EquipType.Balloon => player.balloon, 
			EquipType.Beard => player.beard, 
			_ => -1, 
		};
	}

	public static void EquipTransformation(TransformationAccessory trans, Player self, Mod mod)
	{
		bool[] list = new bool[15];
		(EquipType, string, string)[] equipSlots = trans.EquipSlots;
		for (int i = 0; i < equipSlots.Length; i++)
		{
			(EquipType, string, string) tuple = equipSlots[i];
			EquipType Type = tuple.Item1;
			string AssetName = tuple.Item2;
			string EquipName = tuple.Item3;
			string name = ((AssetName == null) ? null : (EquipName ?? trans.Name));
			if (!trans.CustomSetEquipType(self, Type, mod, name) && !list[(int)Type])
			{
				SetEquipSlot(self, Type, mod, name);
				list[(int)Type] = true;
			}
		}
		if (trans.ShouldHideAccessories)
		{
			self.HideAccessories();
		}
	}

	public static EquipTexture GetEquipTexture(Player p, EquipType type)
	{
		return EquipLoader.GetEquipTexture(type, GetEquipSlot(p, type));
	}
}
