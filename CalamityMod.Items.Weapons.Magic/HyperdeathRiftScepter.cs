using System.Collections.Generic;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "DeathhailStaff" })]
public class HyperdeathRiftScepter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		tooltips.FindAndReplaceAll("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
	}

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 84;
		base.Item.damage = 4300;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 50;
		base.Item.useTime = (base.Item.useAnimation = 50);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.UseSound = SoundID.Item12 with
		{
			Volume = 0.75f
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<HyperdeathRiftScepterBeam>();
		base.Item.shootSpeed = 18f;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Magic/HyperdeathRiftScepterGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, player.Calamity().mouseWorld, Vector2.Zero, type, damage, knockback, player.whoAmI, 1f);
		return false;
	}
}
