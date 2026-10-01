using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Face })]
public class AbyssalDivingGear : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Face);
			ArmorIDs.Face.Sets.PreventHairDraw[equipSlot] = true;
			ArmorIDs.Face.Sets.OverrideHelmet[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.depthCharm = true;
		calamityPlayer.jellyfishNecklace = true;
		player.arcticDivingGear = true;
		player.accFlipper = true;
		player.accDivingHelm = true;
		player.iceSkate = true;
		if (player.wet)
		{
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.2f, 0.8f, 0.9f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1861).AddIngredient<DepthCharm>().AddIngredient<DepthCells>(10)
			.AddIngredient<Lumenyl>(10)
			.AddTile(134)
			.Register();
	}
}
