using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Shield })]
public class AsgardsValor : ModItem, ILocalizedModType, IModType
{
	public const int ShieldSlamDamage = 200;

	public const float ShieldSlamKnockback = 9f;

	public const int ShieldSlamIFrames = 12;

	public new string LocalizationCategory => "Items.Accessories";

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
		base.Item.width = 50;
		base.Item.height = 48;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().DashID = AsgardsValorDash.ID;
		player.dashType = 0;
		player.noKnockback = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<OrnateShield>().AddIngredient<CoreofCalamity>().AddIngredient(1225, 5)
			.AddTile(134)
			.Register();
	}
}
