using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Shield })]
public class ShieldoftheHighRuler : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public const int ShieldSlamIFrames = 12;

	public const float EoCDashVelocity = 14.5f;

	public const float TabiDashVelocity = 18.9f;

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
		base.Item.width = 36;
		base.Item.height = 38;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 300;
		base.Item.knockBack = 9f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
		base.Item.defense = 4;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override bool MeleePrefix()
	{
		return false;
	}

	public override bool WeaponPrefix()
	{
		return false;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		player.dashType = 2;
		calamityPlayer.DashID = string.Empty;
		calamityPlayer.copyrightInfringementShield = true;
		player.noKnockback = true;
		player.fireWalk = true;
		player.buffImmune[24] = true;
		player.buffImmune[46] = true;
		player.buffImmune[44] = true;
		player.buffImmune[33] = true;
		player.buffImmune[36] = true;
		player.buffImmune[30] = true;
		player.buffImmune[20] = true;
		player.buffImmune[32] = true;
		player.buffImmune[31] = true;
		player.buffImmune[35] = true;
		player.buffImmune[23] = true;
		player.buffImmune[22] = true;
		player.buffImmune[194] = true;
		player.buffImmune[156] = true;
		player.statLifeMax2 += 10;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3097).AddIngredient(156).AddIngredient<LifeAlloy>(4)
			.AddTile(134)
			.Register();
	}
}
