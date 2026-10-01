using System.Collections.Generic;
using System.Linq;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class GildedProboscis : BaseSwordHoldoutItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override int ProjectileType => ModContent.ProjectileType<GildedProboscisProj>();

	public override bool SizeModifiers => false;

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		float cdmg = 2f + Main.LocalPlayer.Calamity().critDamage + Main.LocalPlayer.GetTotalCritChance(TrueMeleeNoSpeedDamageClass.Instance) * 0.02f;
		tooltips.FirstOrDefault((TooltipLine x) => x.Name == "CritChance").Text = CalamityUtils.GetText("Common.CritDamageTootip").Format(cdmg.ToPercent());
	}

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 66;
		base.Item.damage = 2090;
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 65);
		base.Item.useStyle = 5;
		base.Item.knockBack = 15f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shootSpeed = 13f;
		base.Item.channel = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
		base.SetDefaults();
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}
}
