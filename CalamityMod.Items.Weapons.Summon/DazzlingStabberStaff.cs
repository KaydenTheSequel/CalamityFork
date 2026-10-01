using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class DazzlingStabberStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 60;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.UseSound = SoundID.DD2_DarkMageHealImpact;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.mana = 10;
		base.Item.damage = 35;
		base.Item.knockBack = 2f;
		base.Item.autoReuse = true;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.buffType = ModContent.BuffType<DazzlingStabberBuff>();
		base.Item.shoot = ModContent.ProjectileType<DazzlingStabber>();
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Summon/DazzlingStabberStaffGlow", (AssetRequestMode)2).Value);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		float usedMinionSlots = 0f;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile minions = enumerator.Current;
			if (minions.owner == player.whoAmI)
			{
				usedMinionSlots += minions.minionSlots;
			}
		}
		bool hasSlotsForSummon = true;
		if (usedMinionSlots + 1f > (float)player.maxMinions)
		{
			hasSlotsForSummon = false;
		}
		player.AddBuff(base.Item.buffType, 2);
		int projCount = player.ownedProjectileCounts[type] + (hasSlotsForSummon ? 3 : 0);
		if (hasSlotsForSummon)
		{
			for (int i = 0; i < 3; i++)
			{
				Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, i + 1).originalDamage = base.Item.damage;
			}
		}
		float angleMax = MathHelper.ToRadians(360f);
		if (projCount == 100)
		{
			angleMax = 0f;
		}
		float index = 1f;
		if (projCount > 30)
		{
			angleMax += MathHelper.ToRadians((float)(projCount - 30) * 2.5f);
		}
		angleMax = ((angleMax > MathHelper.ToRadians(360f)) ? MathHelper.ToRadians(360f) : angleMax);
		ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator2.MoveNext())
		{
			Projectile p = enumerator2.Current;
			if (p.type == type && p.owner == player.whoAmI)
			{
				int adjustedProjCount = projCount;
				p.ai[1] = index / (float)adjustedProjCount * angleMax - angleMax / 2f;
				p.netUpdate = true;
				index++;
			}
		}
		return false;
	}
}
