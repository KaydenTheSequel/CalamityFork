using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class Cosmilamp : ModItem, ILocalizedModType, IModType
{
	public const int BeamShootRate = 105;

	public const float MaxTargetingDistance = 1360f;

	public const float BeamHomeSpeed = 17f;

	public const float LanternSummonCost = 2f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 2f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 60;
		base.Item.damage = 127;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 4f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item44;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<CosmilampBuff>();
		base.Item.shoot = ModContent.ProjectileType<CosmilampMinion>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool CanUseItem(Player player)
	{
		return player.maxMinions >= 2;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile pro = enumerator.Current;
			if (pro.type == type && pro.owner == player.whoAmI)
			{
				pro.ModProjectile<CosmilampMinion>().Timer = 0f;
				pro.netUpdate = true;
			}
		}
		int existingLamps = player.ownedProjectileCounts[type];
		Projectile projectile = Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
		projectile.originalDamage = base.Item.damage;
		projectile.ai[0] = existingLamps;
		return false;
	}
}
