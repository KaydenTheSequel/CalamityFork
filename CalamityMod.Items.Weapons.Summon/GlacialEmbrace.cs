using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "ColdDivinity" })]
public class GlacialEmbrace : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 50;
		base.Item.damage = 48;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 4.5f;
		base.Item.UseSound = SoundID.Item30;
		base.Item.autoReuse = true;
		base.Item.buffType = ModContent.BuffType<GlacialEmbraceBuff>();
		base.Item.shoot = ModContent.ProjectileType<GlacialEmbracePointyThing>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		float totalMinionSlots = 0f;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile pro = enumerator.Current;
			if (pro.minion && pro.owner == player.whoAmI)
			{
				totalMinionSlots += pro.minionSlots;
			}
		}
		if (player.altFunctionUse != 2 && totalMinionSlots < (float)player.maxMinions)
		{
			player.AddBuff(base.Item.buffType, 2);
			position = player.ClampedMouseWorld();
			Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
			int pointyThingCount = 0;
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile pro2 = enumerator2.Current;
				if (pro2.type == type && pro2.owner == player.whoAmI && (pro2.ModProjectile as GlacialEmbracePointyThing).circlingPlayer)
				{
					pointyThingCount++;
				}
			}
			float angleVariance = (float)Math.PI * 2f / (float)pointyThingCount;
			float angle = 0f;
			ActiveEntityIterator<Projectile>.Enumerator enumerator3 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator3.MoveNext())
			{
				Projectile pro3 = enumerator3.Current;
				if (pro3.type == type && pro3.owner == player.whoAmI && pro3.ai[1] == 0f && (pro3.ModProjectile as GlacialEmbracePointyThing).circlingPlayer)
				{
					pro3.ai[0] = angle;
					pro3.netUpdate = true;
					angle += angleVariance;
					for (int j = 0; j < 22; j++)
					{
						Dust dust = Dust.NewDustDirect(pro3.position, pro3.width, pro3.height, 80);
						dust.velocity = Vector2.UnitY * Main.rand.NextFloat(3f, 5.5f) * (float)Main.rand.NextBool().ToDirectionInt();
						dust.noGravity = true;
					}
				}
			}
		}
		return false;
	}

	public override bool AltFunctionUse(Player player)
	{
		return base.AltFunctionUse(player);
	}
}
