using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "SeaboundStaff" })]
public class BrittleStarStaff : ModItem, ILocalizedModType, IModType
{
	public float Knockback = 2f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 64;
		base.Item.damage = 10;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = Knockback;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.UseSound = SoundID.Item44 with
		{
			Pitch = 0.5f
		};
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<BrittleStar>();
		base.Item.shoot = ModContent.ProjectileType<BrittleStarMinion>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse != 2)
		{
			base.Item.noUseGraphic = false;
			int SummonNumber = player.ownedProjectileCounts[type];
			Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, SummonNumber).originalDamage = base.Item.damage;
		}
		if (player.altFunctionUse == 2)
		{
			base.Item.noUseGraphic = true;
		}
		int bladeIndex = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == type && p.owner == player.whoAmI)
			{
				p.ModProjectile<BrittleStarMinion>().StarIndex = bladeIndex++;
				p.ModProjectile<BrittleStarMinion>().AITimer = 0f;
				p.netUpdate = true;
			}
		}
		return false;
	}
}
