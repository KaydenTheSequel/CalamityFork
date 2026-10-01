using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.FurnitureAcidwood;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ParasiticSceptor : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 52);
		base.Item.damage = 12;
		base.Item.knockBack = 3f;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 35);
		base.Item.autoReuse = true;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.shootSpeed = 10f;
		base.Item.shoot = ModContent.ProjectileType<WaterLeechProj>();
		base.Item.UseSound = SoundID.Item46;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
		base.Item.rare = 2;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		float speed = base.Item.shootSpeed;
		Vector2 playerPos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float xDist = (float)Main.mouseX + Main.screenPosition.X - playerPos.X;
		float yDist = (float)Main.mouseY + Main.screenPosition.Y - playerPos.Y;
		if (player.gravDir == -1f)
		{
			yDist = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - playerPos.Y;
		}
		Vector2 vector = default(Vector2);
		((Vector2)(ref vector))._002Ector(xDist, yDist);
		float speedMult = ((Vector2)(ref vector)).Length();
		if ((float.IsNaN(xDist) && float.IsNaN(yDist)) || (xDist == 0f && yDist == 0f))
		{
			xDist = player.direction;
			yDist = 0f;
			speedMult = speed;
		}
		else
		{
			speedMult = speed / speedMult;
		}
		xDist *= speedMult;
		yDist *= speedMult;
		int leechAmt = 2;
		if (Main.rand.NextBool(3))
		{
			leechAmt++;
		}
		if (Main.rand.NextBool(4))
		{
			leechAmt++;
		}
		if (Main.rand.NextBool(5))
		{
			leechAmt++;
		}
		Vector2 directionToShoot = default(Vector2);
		for (int i = 0; i < leechAmt; i++)
		{
			float xVec = xDist;
			float yVec = yDist;
			float spreadMult = 0.05f * (float)i;
			xVec += Main.rand.NextFloat(-25f, 25f) * spreadMult;
			yVec += Main.rand.NextFloat(-25f, 25f) * spreadMult;
			((Vector2)(ref directionToShoot))._002Ector(xVec, yVec);
			speedMult = ((Vector2)(ref directionToShoot)).Length();
			speedMult = speed / speedMult;
			xVec *= speedMult;
			yVec *= speedMult;
			((Vector2)(ref directionToShoot))._002Ector(xVec, yVec);
			Projectile.NewProjectile(source, playerPos, directionToShoot, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Acidwood>(15).AddIngredient<SulphuricScale>(12).AddTile(16)
			.Register();
	}
}
