using System.Collections.Generic;
using CalamityMod.Items.BaseItems;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class LittleE : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[3]
	{
		(EquipType.Head, "BigE", null),
		(EquipType.Body, "BigE", null),
		(EquipType.Legs, "BigE", null)
	};

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(5, 14));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[2716] = base.Type;
		base.SetStaticDefaults();
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 32;
		base.Item.accessory = true;
		base.Item.vanity = true;
		base.Item.rare = 10;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.Calamity().devItem = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		string text = StringExtensions.FormatWith(Language.GetTextValue("Mods.CalamityMod.Items.Accessories.LittleE.Tooltip"), new object[1] { Main.LocalPlayer.name });
		if (base.Item.social)
		{
			tooltips.Insert(1, new TooltipLine(CalamityMod.Instance, "Tooltip", text));
		}
		else
		{
			tooltips[3].Text = text;
		}
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = base.Item.GetFrame(whoAmI);
		Vector2 position = base.Item.Center - Main.screenPosition + Vector2.UnitY * 4f;
		Vector2 origin = frame.Size() / 2f;
		spriteBatch.Draw(TextureAssets.Item[base.Type].Value, position, (Rectangle?)frame, lightColor, rotation, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}
}
