using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

[LegacyName(new string[] { "GodspawnHelixStaff" })]
public class StarspawnHelixStaff : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetDefaults()
	{
		base.Item.width = 54;
		base.Item.height = 52;
		base.Item.damage = 103;
		base.Item.knockBack = 1.25f;
		base.Item.mana = 10;
		base.Item.buffType = ModContent.BuffType<AstralProbeBuff>();
		base.Item.shoot = ModContent.ProjectileType<AstralProbeSummon>();
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.DamageType = DamageClass.Summon;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item44;
		base.Item.rare = 9;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		player.AddBuff(base.Item.buffType, 2);
		Projectile projectile = Projectile.NewProjectileDirect(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 1f);
		projectile.originalDamage = base.Item.damage;
		projectile.ModProjectile<AstralProbeSummon>().ProbeIndex = player.ownedProjectileCounts[type];
		int bladeIndex = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile pro = enumerator.Current;
			if (pro.type == type && pro.owner == player.whoAmI)
			{
				pro.ModProjectile<AstralProbeSummon>().ProbeIndex = bladeIndex++;
				pro.ModProjectile<AstralProbeSummon>().AITimer = 0f;
				pro.netUpdate = true;
			}
		}
		return false;
	}
}
