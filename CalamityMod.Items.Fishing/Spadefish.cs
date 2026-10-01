using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing;

public class Spadefish : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 44;
		base.Item.damage = 15;
		base.Item.knockBack = 2f;
		base.Item.useTime = 7;
		base.Item.useAnimation = 20;
		base.Item.pick = 34;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
	}
}
