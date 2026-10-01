using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "TrueConferenceCall", "ConclaveCrossfire" })]
public class ConferenceCall : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 66;
		base.Item.height = 26;
		base.Item.damage = 53;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 42;
		base.Item.useAnimation = 42;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = SoundID.Item38;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = 10;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		int bulletAmt = 4;
		for (int index = 0; index < bulletAmt; index++)
		{
			velocity.X += (float)Main.rand.Next(-15, 16) * 0.05f;
			velocity.Y += (float)Main.rand.Next(-15, 16) * 0.05f;
			int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			Main.projectile[proj].extraUpdates += 2;
		}
		int maxTargets = 7;
		int[] targets = new int[maxTargets];
		int targetArrayIndex = 0;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector((int)player.Center.X - 960, (int)player.Center.Y - 540, 1920, 1080);
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.chaseable || npc.lifeMax <= 5 || npc.dontTakeDamage || npc.friendly || npc.immortal)
			{
				continue;
			}
			Rectangle hitbox = npc.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(rectangle))
			{
				if (targetArrayIndex >= maxTargets)
				{
					break;
				}
				targets[targetArrayIndex] = npc.whoAmI;
				targetArrayIndex++;
			}
		}
		if (targetArrayIndex == 0)
		{
			return false;
		}
		int extraBulletDamage = (int)((double)damage * 0.8);
		Vector2 targetPosition = default(Vector2);
		for (int j = 0; j < targetArrayIndex; j++)
		{
			((Vector2)(ref targetPosition))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			targetPosition.X = (targetPosition.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			targetPosition.Y -= 100 * j;
			Projectile dummy = new Projectile();
			dummy.SetDefaults(type);
			Vector2 extraBulletVel = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(targetPosition, Main.npc[targets[j]], base.Item.shootSpeed, dummy.MaxUpdates);
			int proj2 = Projectile.NewProjectile(source, targetPosition, extraBulletVel, type, extraBulletDamage, knockback, player.whoAmI);
			Main.projectile[proj2].tileCollide = false;
			Main.projectile[proj2].timeLeft /= 2;
		}
		if (targetArrayIndex == maxTargets)
		{
			return false;
		}
		for (int k = 0; k < maxTargets - targetArrayIndex; k++)
		{
			int randomTarget = Main.rand.Next(targetArrayIndex);
			((Vector2)(ref targetPosition))._002Ector(player.position.X + (float)player.width * 0.5f + (float)Main.rand.Next(201) * (0f - (float)player.direction) + ((float)Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
			targetPosition.X = (targetPosition.X + player.Center.X) / 2f + (float)Main.rand.Next(-200, 201);
			targetPosition.Y -= 100 * randomTarget;
			Projectile dummy2 = new Projectile();
			dummy2.SetDefaults(type);
			Vector2 extraBulletVel2 = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(targetPosition, Main.npc[targets[randomTarget]], base.Item.shootSpeed, dummy2.MaxUpdates);
			int proj3 = Projectile.NewProjectile(source, targetPosition, extraBulletVel2, type, extraBulletDamage, knockback, player.whoAmI);
			Main.projectile[proj3].tileCollide = false;
			Main.projectile[proj3].timeLeft /= 2;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(679).AddIngredient(3456, 12).AddTile(412)
			.Register();
	}
}
