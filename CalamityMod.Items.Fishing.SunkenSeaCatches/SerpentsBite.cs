using CalamityMod.Projectiles.Typeless;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.SunkenSeaCatches;

public class SerpentsBite : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 28.125f;

	public static float LaunchSpeed = 18f;

	public static float ReelbackSpeed = 14f;

	public static float PullSpeed = 12f;

	public new string LocalizationCategory => "Items.Fishing";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToString(), LaunchSpeed.ToString(), ReelbackSpeed.ToString(), PullSpeed.ToString());

	public override void SetDefaults()
	{
		base.Item.CloneDefaults(1236);
		base.Item.width = 30;
		base.Item.height = 32;
		base.Item.shootSpeed = LaunchSpeed;
		base.Item.shoot = ModContent.ProjectileType<SerpentsBiteHook>();
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
	}
}
