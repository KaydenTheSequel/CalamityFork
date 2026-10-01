using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class WulfrumBattery : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ExtraDropSound = new SoundStyle("CalamityMod/Sounds/Custom/WulfrumExtraDrop")
	{
		PitchVariance = 0.3f
	};

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ExtractinatorMode[base.Type] = base.Item.type;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 1;
		base.Item.useStyle = 10;
		base.Item.useAnimation = 10;
		base.Item.useTime = 2;
		base.Item.consumable = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage<SummonDamageClass>() += 0.07f;
		if (!hideVisual)
		{
			player.GetModPlayer<WulfrumBatteryPlayer>().battery = true;
		}
	}

	public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		resultType = ModContent.ItemType<WulfrumMetalScrap>();
		resultStack = Main.rand.Next(3, 6);
		if (Main.rand.NextFloat() > 0.8f)
		{
			resultStack = 1;
			resultType = ModContent.ItemType<EnergyCore>();
		}
	}
}
