using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityMod.Items.BaseItems;

public abstract class HeldOnlyItem : ModItem
{
	private sealed class HeldOnlyItemHooks : ModSystem
	{
		[CompilerGenerated]
		private static class _003C_003EO
		{
			public static hook_dropItemCheck _003C0_003E__DontDropCoolStuff;

			public static hook_LeftClick_ItemArray_int_int _003C1_003E__LockMouseToSpecialItem;

			public static hook_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color _003C2_003E__DrawSpecial;
		}

		public override void Load()
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0055: Unknown result type (might be due to invalid IL or missing references)
			//IL_005b: Expected O, but got Unknown
			object obj = _003C_003EO._003C0_003E__DontDropCoolStuff;
			if (obj == null)
			{
				hook_dropItemCheck val = DontDropCoolStuff;
				_003C_003EO._003C0_003E__DontDropCoolStuff = val;
				obj = (object)val;
			}
			On_Player.dropItemCheck += (hook_dropItemCheck)obj;
			object obj2 = _003C_003EO._003C1_003E__LockMouseToSpecialItem;
			if (obj2 == null)
			{
				hook_LeftClick_ItemArray_int_int val2 = LockMouseToSpecialItem;
				_003C_003EO._003C1_003E__LockMouseToSpecialItem = val2;
				obj2 = (object)val2;
			}
			On_ItemSlot.LeftClick_ItemArray_int_int += (hook_LeftClick_ItemArray_int_int)obj2;
			object obj3 = _003C_003EO._003C2_003E__DrawSpecial;
			if (obj3 == null)
			{
				hook_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color val3 = DrawSpecial;
				_003C_003EO._003C2_003E__DrawSpecial = val3;
				obj3 = (object)val3;
			}
			On_ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += (hook_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color)obj3;
		}

		private static void DrawSpecial(orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch sb, Item[] inv, int context, int slot, Vector2 position, Color color)
		{
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			if (!(inv[slot].ModItem is HeldOnlyItem { VisibleInUI: false }))
			{
				orig.Invoke(sb, inv, context, slot, position, color);
			}
		}

		private static void LockMouseToSpecialItem(orig_LeftClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
		{
			if (!(Main.mouseItem.ModItem is HeldOnlyItem))
			{
				orig.Invoke(inv, context, slot);
			}
		}

		private static void DontDropCoolStuff(orig_dropItemCheck orig, Player self)
		{
			if (!(Main.mouseItem.ModItem is HeldOnlyItem))
			{
				orig.Invoke(self);
			}
		}
	}

	public virtual bool VisibleInUI => false;

	public override void PostUpdate()
	{
		base.Item.type = 0;
		base.Item.stack = 0;
	}

	public override bool CanPickup(Player player)
	{
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		return false;
	}
}
