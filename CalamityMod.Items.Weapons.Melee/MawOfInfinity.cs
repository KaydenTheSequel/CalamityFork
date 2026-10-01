using System.Collections.Generic;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Projectiles.BaseProjectiles;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "Excelsus" })]
public class MawOfInfinity : BaseSwordHoldoutItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override int ProjectileType => ModContent.ProjectileType<MawOfInfinityHoldout>();

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		tooltips.FindAndReplaceAll("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
	}

	public override void SetDefaults()
	{
		base.Item.width = 78;
		base.Item.height = 94;
		base.Item.damage = 2080;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTime = (base.Item.useAnimation = 33);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.shoot = ModContent.ProjectileType<MawOfInfinityHoldout>();
		base.Item.shootSpeed = 12f;
		base.SetDefaults();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/MawOfInfinityGlow", (AssetRequestMode)2).Value);
	}
}
