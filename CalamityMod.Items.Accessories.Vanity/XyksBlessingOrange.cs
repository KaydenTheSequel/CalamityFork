using CalamityMod.Items.BaseItems;
using CalamityMod.Rarities;
using Terraria;
using Terraria.GameContent.NetModules;
using Terraria.ModLoader;
using Terraria.Net;

namespace CalamityMod.Items.Accessories.Vanity;

public class XyksBlessingOrange : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[4]
	{
		(EquipType.Head, "Xyk2", null),
		(EquipType.Body, "Xyk2", null),
		(EquipType.Legs, "Xyk2", null),
		(EquipType.Wings, null, null)
	};

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 34;
		base.Item.accessory = true;
		base.Item.vanity = true;
		base.Item.rare = ModContent.RarityType<DarkOrange>();
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.Calamity().devItem = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void RightClick(Player player)
	{
		player.PutItemInInventoryFromItemUsage(ModContent.ItemType<XyksBlessingBlue>(), 1);
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().XykVisualsOrange = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		if (!hideVisual)
		{
			player.Calamity().XykVisualsOrange = true;
		}
	}

	public override void OnResearched(bool fullyResearched)
	{
		if (fullyResearched)
		{
			if (!Main.ServerSideCharacter)
			{
				Main.LocalPlayerCreativeTracker.ItemSacrifices.RegisterItemSacrifice(ModContent.ItemType<XyksBlessingBlue>(), 1);
				return;
			}
			NetPacket packet = NetCreativeUnlocksPlayerReportModule.SerializeSacrificeRequest(ModContent.ItemType<XyksBlessingBlue>(), 1);
			NetManager.Instance.SendToServerOrLoopback(packet);
		}
	}
}
