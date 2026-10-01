using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Shield })]
public class ElysianAegis : ModItem, ILocalizedModType, IModType
{
	public const int ShieldSlamDamage = 500;

	public const float ShieldSlamKnockback = 12f;

	public const int ShieldSlamIFrames = 12;

	public const int RamExplosionDamage = 100;

	public const float RamExplosionKnockback = 15f;

	public new string LocalizationCategory => "Items.Accessories";

	public bool HasFlavorTooltip => true;

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(195, 223, 255);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 42;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.defense = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().DashID = ElysianAegisDash.ID;
		player.dashType = 0;
		player.noKnockback = true;
	}
}
