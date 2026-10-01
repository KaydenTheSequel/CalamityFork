using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "OldLordOathsword" })]
public class OldLordClaymore : CustomUseProjItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 76;
		base.Item.height = 76;
		base.Item.damage = 144;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 90);
		base.Item.useStyle = 5;
		base.Item.shoot = ModContent.ProjectileType<OldLordClaymoreHoldout>();
		base.Item.useTurn = true;
		base.Item.knockBack = 10f;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OldLordClaymoreGlow", (AssetRequestMode)2).Value);
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 11f;
	}
}
