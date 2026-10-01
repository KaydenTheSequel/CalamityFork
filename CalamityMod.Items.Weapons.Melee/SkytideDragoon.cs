using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SkytideDragoon : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 94;
		base.Item.damage = 365;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 35;
		base.Item.useTime = 35;
		base.Item.useTurn = true;
		base.Item.knockBack = 12f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<SkytideDragoonHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/SkytideDragoonGlow", (AssetRequestMode)2).Value);
	}
}
