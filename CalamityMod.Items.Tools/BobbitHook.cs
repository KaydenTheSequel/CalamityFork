using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class BobbitHook : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 40f;

	public static float LaunchSpeed = 25f;

	public static float ReelbackSpeed = 28f;

	public static float PullSpeed = 24f;

	public new string LocalizationCategory => "Items.Tools";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToString(), LaunchSpeed.ToString(), ReelbackSpeed.ToString(), PullSpeed.ToString());

	public override void SetDefaults()
	{
		base.Item.CloneDefaults(1236);
		base.Item.width = 30;
		base.Item.height = 32;
		base.Item.shootSpeed = LaunchSpeed;
		base.Item.shoot = ModContent.ProjectileType<BobbitHead>();
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}
}
