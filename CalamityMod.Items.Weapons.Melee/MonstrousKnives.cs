using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class MonstrousKnives : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 20;
		base.Item.damage = 8;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 21;
		base.Item.useStyle = 1;
		base.Item.useTime = 21;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item39;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<MonstrousKnife>();
		base.Item.shootSpeed = 15f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
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
		int knifeAmt = Main.rand.Next(4, 7);
		Vector2 directionToShoot = default(Vector2);
		for (int i = 0; i < knifeAmt; i++)
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
		CreateRecipe().AddIngredient(279, 50).AddIngredient(29).AddIngredient(28, 5)
			.AddTile(16)
			.Register();
	}
}
