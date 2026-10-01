using System;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "GreatbowofTurmoil" })]
public class ContinentalGreatbow : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 36;
		base.Item.damage = 38;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 24;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = SoundID.Item5;
		base.Item.autoReuse = true;
		base.Item.shoot = 1;
		base.Item.shootSpeed = 17f;
		base.Item.useAmmo = AmmoID.Arrow;
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
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		Vector2 source = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float piOverTen = (float)Math.PI / 10f;
		int arrowAmt = 3;
		((Vector2)(ref velocity)).Normalize();
		velocity *= 40f;
		bool canHit = Collision.CanHit(source, 0, 0, source + velocity, 0, 0);
		for (int projIndex = 0; projIndex < arrowAmt; projIndex++)
		{
			float offsetAmt = (float)projIndex - ((float)arrowAmt - 1f) / 2f;
			Vector2 offset = velocity.RotatedBy(piOverTen * offsetAmt);
			if (!canHit)
			{
				offset -= velocity;
			}
			if (CalamityUtils.CheckWoodenAmmo(type, player))
			{
				type = 2;
			}
			int baseArrow = Projectile.NewProjectile(spawnSource, source + offset, velocity, type, damage, knockback, player.whoAmI);
			Main.projectile[baseArrow].noDropItem = true;
		}
		for (int i = 0; i < 2; i++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
			type = Utils.SelectRandom<int>(Main.rand, 41, 278);
			int index = Projectile.NewProjectile(spawnSource, position, new Vector2(SpeedX, SpeedY), type, (int)((float)damage * 0.5f), knockback, player.whoAmI);
			Main.projectile[index].noDropItem = true;
			Main.projectile[index].usesLocalNPCImmunity = true;
			Main.projectile[index].localNPCHitCooldown = 10;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ScoriaBar>(10).AddTile(134).Register();
	}
}
