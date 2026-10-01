using System;
using CalamityMod.Projectiles.Melee.Spears;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class BansheeHook : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 120;
		base.Item.height = 108;
		base.Item.damage = 250;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 21;
		base.Item.useStyle = 5;
		base.Item.useTime = 21;
		base.Item.knockBack = 8.5f;
		base.Item.UseSound = SoundID.DD2_GhastlyGlaivePierce;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<BansheeHookProj>();
		base.Item.shootSpeed = 42f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/BansheeHookGlow", (AssetRequestMode)2).Value);
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		float mouseXDist = (float)Main.mouseX + Main.screenPosition.X - position.X;
		float mouseYDist = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
		if (player.gravDir == -1f)
		{
			mouseYDist = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - position.Y;
		}
		float mouseDistance = (float)Math.Sqrt(mouseXDist * mouseXDist + mouseYDist * mouseYDist);
		if ((float.IsNaN(mouseXDist) && float.IsNaN(mouseYDist)) || (mouseXDist == 0f && mouseYDist == 0f))
		{
			mouseXDist = player.direction;
			mouseYDist = 0f;
			mouseDistance = base.Item.shootSpeed;
		}
		else
		{
			mouseDistance = base.Item.shootSpeed / mouseDistance;
		}
		mouseXDist *= mouseDistance;
		mouseYDist *= mouseDistance;
		float ai4 = Main.rand.NextFloat() * base.Item.shootSpeed * 0.75f * (float)player.direction;
		((Vector2)(ref velocity))._002Ector(mouseXDist, mouseYDist);
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, ai4);
		return false;
	}
}
