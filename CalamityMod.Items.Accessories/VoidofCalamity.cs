using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[LegacyName(new string[] { "CalamityRing" })]
public class VoidofCalamity : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().voidOfCalamity = true;
		player.GetDamage<GenericDamageClass>() += 0.12f;
		if (player.whoAmI == Main.myPlayer)
		{
			IEntitySource source = player.GetSource_Accessory(base.Item);
			if (player.immune && player.miscCounter % 10 == 0)
			{
				int damage = (int)player.GetBestClassDamage().ApplyTo(30f);
				CalamityUtils.ProjectileRain(source, player.Center, 400f, 100f, 500f, 800f, 22f, ModContent.ProjectileType<StandingFire>(), damage, 5f, player.whoAmI);
			}
		}
	}
}
