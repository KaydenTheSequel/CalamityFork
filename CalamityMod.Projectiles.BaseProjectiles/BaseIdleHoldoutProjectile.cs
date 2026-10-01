using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseIdleHoldoutProjectile : ModProjectile
{
	public static Dictionary<int, int> ItemProjectileRelationship = new Dictionary<int, int>();

	public Player Owner => Main.player[base.Projectile.owner];

	public abstract int AssociatedItemID { get; }

	public abstract int IntendedProjectileType { get; }

	public override void SetStaticDefaults()
	{
		ItemProjectileRelationship[AssociatedItemID] = IntendedProjectileType;
	}

	public override void Unload()
	{
		ItemProjectileRelationship?.Clear();
		ItemProjectileRelationship = null;
	}

	public static void CheckForEveryHoldout(Player player)
	{
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		foreach (int itemID in ItemProjectileRelationship.Keys)
		{
			Item heldItem = player.HeldItem;
			if (heldItem.type != itemID)
			{
				continue;
			}
			bool bladeIsPresent = false;
			int holdoutType = ItemProjectileRelationship[itemID];
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile p = enumerator2.Current;
				if (p.type == holdoutType && p.owner == player.whoAmI)
				{
					bladeIsPresent = true;
					break;
				}
			}
			if (Main.myPlayer == player.whoAmI && !bladeIsPresent)
			{
				int damage = player.GetWeaponDamage(heldItem);
				float kb = player.GetWeaponKnockback(heldItem, heldItem.knockBack);
				Projectile.NewProjectile(player.GetSource_ItemUse(heldItem), player.Center, Vector2.Zero, holdoutType, damage, kb, player.whoAmI);
			}
		}
	}

	public sealed override void AI()
	{
		CheckForEveryHoldout(Owner);
		if (Owner.HeldItem.type != AssociatedItemID || Owner.CCed || !Owner.active || Owner.dead || Owner.Calamity().profanedCrystalBuffs)
		{
			base.Projectile.Kill();
		}
		else
		{
			SafeAI();
		}
	}

	public virtual void SafeAI()
	{
	}
}
