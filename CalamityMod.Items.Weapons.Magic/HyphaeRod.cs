using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class HyphaeRod : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 34;
		base.Item.damage = 22;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 7;
		base.Item.useTime = 24;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item8;
		base.Item.autoReuse = true;
		base.Item.shoot = 590;
		base.Item.shootSpeed = 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo projSource, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		float speed = base.Item.shootSpeed;
		Vector2 source = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		float xDist = (float)Main.mouseX + Main.screenPosition.X + source.X;
		float yDist = (float)Main.mouseY + Main.screenPosition.Y + source.Y;
		Vector2 spawnVec = default(Vector2);
		((Vector2)(ref spawnVec))._002Ector(xDist, yDist);
		if (player.gravDir == -1f)
		{
			spawnVec.Y = Main.screenPosition.Y + (float)Main.screenHeight + (float)Main.mouseY + source.Y;
		}
		float distance = ((Vector2)(ref spawnVec)).Length();
		if ((float.IsNaN(spawnVec.X) && float.IsNaN(spawnVec.Y)) || (spawnVec.X == 0f && spawnVec.Y == 0f))
		{
			spawnVec.X = player.direction;
			spawnVec.Y = 0f;
			distance = speed;
		}
		else
		{
			distance = speed / distance;
		}
		int projAmt = 3;
		for (int projIndex = 0; projIndex < projAmt; projIndex++)
		{
			((Vector2)(ref source))._002Ector(player.Center.X + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y);
			source.X = (source.X + player.Center.X) / 2f + (float)Main.rand.Next(-100, 101);
			source.Y -= 50 * projIndex;
			spawnVec.X = (float)Main.mouseX + Main.screenPosition.X - source.X;
			spawnVec.Y = (float)Main.mouseY + Main.screenPosition.Y - source.Y;
			if (spawnVec.Y < 0f)
			{
				spawnVec.Y *= -1f;
			}
			if (spawnVec.Y < 20f)
			{
				spawnVec.Y = 20f;
			}
			distance = ((Vector2)(ref spawnVec)).Length();
			distance = speed / distance;
			spawnVec.X *= distance;
			spawnVec.Y *= distance;
			spawnVec.X += (float)Main.rand.Next(-180, 181) * 0.02f;
			spawnVec.Y += (float)Main.rand.Next(-180, 181) * 0.02f;
			int proj = Projectile.NewProjectile(projSource, source, spawnVec, type, damage, knockback, player.whoAmI, 0f, Main.rand.Next(3));
			if (proj.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[proj].DamageType = DamageClass.Magic;
				Main.projectile[proj].timeLeft = CalamityUtils.SecondsToFrames(3f);
			}
		}
		return false;
	}
}
