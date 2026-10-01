using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Vanity;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
public class ArtemisMask : ModItem, IExtendedHat, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.Vanity";

	public string ExtensionTexture => "CalamityMod/Items/Armor/Vanity/ArtemisMask_Extra";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlotHead = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Head);
			ArmorIDs.Head.Sets.DrawHead[equipSlotHead] = false;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 20;
		base.Item.rare = 1;
		base.Item.value = Item.sellPrice(0, 0, 75);
		base.Item.vanity = true;
	}

	public Vector2 ExtensionSpriteOffset(PlayerDrawSet drawInfo)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(((float)drawInfo.drawPlayer.direction == 1f) ? (-6f) : 0f, 0f);
	}
}
