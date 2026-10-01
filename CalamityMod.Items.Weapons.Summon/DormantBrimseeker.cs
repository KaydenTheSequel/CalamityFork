using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class DormantBrimseeker : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 32);
		base.Item.damage = 42;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DormantBrimseekerSummoner>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		float totalSlots = 0f;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.minion && p.owner == player.whoAmI)
			{
				totalSlots += p.minionSlots;
			}
		}
		if (totalSlots < (float)player.maxMinions)
		{
			int p2 = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (Main.projectile.IndexInRange(p2))
			{
				Main.projectile[p2].originalDamage = base.Item.damage;
			}
		}
		else if ((float)player.ownedProjectileCounts[ModContent.ProjectileType<DormantBrimseekerAura>()] <= 0f)
		{
			int p3 = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<DormantBrimseekerAura>(), damage * 2, knockback, player.whoAmI);
			if (Main.projectile.IndexInRange(p3))
			{
				Main.projectile[p3].originalDamage = base.Item.damage * 2;
			}
		}
		return false;
	}
}
