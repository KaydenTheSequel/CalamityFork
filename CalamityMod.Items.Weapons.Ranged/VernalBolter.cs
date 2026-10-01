using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "CosmicBolter" })]
public class VernalBolter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 68;
		base.Item.height = 94;
		base.Item.damage = 35;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.75f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item75;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<VernalBolt>();
		base.Item.shootSpeed = 10f;
		base.Item.useAmmo = AmmoID.Arrow;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/VernalBolterGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo spawnSource, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		Vector2 source = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float piOver10 = (float)Math.PI / 10f;
		int projAmt = 3;
		((Vector2)(ref velocity)).Normalize();
		velocity *= 30f;
		bool canHit = Collision.CanHit(source, 0, 0, source + velocity, 0, 0);
		for (int i = 0; i < projAmt; i++)
		{
			float offsetAmt = (float)i - ((float)projAmt - 1f) / 2f;
			Vector2 offset = velocity.RotatedBy(piOver10 * offsetAmt);
			if (!canHit)
			{
				offset -= velocity;
			}
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				Projectile.NewProjectile(spawnSource, source + offset, velocity, ModContent.ProjectileType<VernalBolt>(), (int)((double)damage * 1.2), knockback * 1.2f, player.whoAmI);
				continue;
			}
			int proj = Projectile.NewProjectile(spawnSource, source + offset, velocity, type, damage, knockback, player.whoAmI);
			Main.projectile[proj].noDropItem = true;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LunarianBow>().AddIngredient(578).AddIngredient<LivingShard>(12)
			.AddTile(134)
			.Register();
	}
}
