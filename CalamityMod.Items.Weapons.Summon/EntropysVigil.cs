using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "BlightedEyeStaff" })]
public class EntropysVigil : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
		ItemID.Sets.StaffMinionSlotsRequired[base.Type] = 2f;
	}

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 52;
		base.Item.damage = 42;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.buffType = ModContent.BuffType<EntropysVigilBuff>();
		base.Item.shoot = ModContent.ProjectileType<Calamitamini>();
		base.Item.knockBack = 2f;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.mana = 10;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item82;
		base.Item.shootSpeed = 1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Vector2 mouse = player.ClampedMouseWorld();
		float randomAngleOffset = Main.rand.NextFloat((float)Math.PI * 2f);
		for (int i = 0; i < 3; i++)
		{
			Vector2 spawnVelocity = ((float)Math.PI * 2f / 3f * (float)i + randomAngleOffset).ToRotationVector2() * 5f;
			switch (i)
			{
			case 0:
				type = ModContent.ProjectileType<Calamitamini>();
				break;
			case 1:
				type = ModContent.ProjectileType<Catastromini>();
				break;
			case 2:
				type = ModContent.ProjectileType<Cataclymini>();
				break;
			}
			Projectile.NewProjectileDirect(source, mouse, spawnVelocity, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		}
		return false;
	}
}
