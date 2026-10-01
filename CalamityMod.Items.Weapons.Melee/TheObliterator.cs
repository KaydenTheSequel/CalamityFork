using System.Collections.Generic;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Projectiles.Melee.Yoyos;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TheObliterator : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 720f;

	public static float Speed = 54f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToTiles(), Speed);

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		tooltips.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
		if (!Main.zenithWorld)
		{
			tooltips.FindAndReplaceAll("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
		}
	}

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[base.Type] = true;
		ItemID.Sets.GamepadExtraRange[base.Type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 40;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 475;
		base.Item.knockBack = 7.5f;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<ObliteratorYoyo>();
		base.Item.shootSpeed = 16f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}
}
