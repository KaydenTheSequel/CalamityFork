using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class DaemonsFlame : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 66;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 128;
		base.Item.damage = 162;
		base.Item.useTime = 12;
		base.Item.useAnimation = 12;
		base.Item.useStyle = 5;
		base.Item.knockBack = 4f;
		base.Item.UseSound = SoundID.Item5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DaemonsFlameBow>();
		base.Item.shootSpeed = 20f;
		base.Item.useAmmo = AmmoID.Arrow;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/DaemonsFlameGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DaemonsFlameBow>(), damage, knockback, player.whoAmI);
		return false;
	}
}
