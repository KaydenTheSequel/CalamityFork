using CalamityMod.Buffs.StatBuffs;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class Mycoroot : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 32;
		base.Item.damage = 12;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 6;
		base.Item.useAnimation = 6;
		base.Item.useStyle = 1;
		base.Item.knockBack = 1.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.rare = 2;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<MycorootProj>();
		base.Item.shootSpeed = 20f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		int stealth = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.Calamity().StealthStrikeAvailable() && player.ownedProjectileCounts[ModContent.ProjectileType<ShroomerangSpore>()] < 20 && stealth.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[stealth].Calamity().stealthStrike = true;
			for (int i = 0; i < 8; i++)
			{
				Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<ShroomerangSpore>(), damage, knockback, player.whoAmI, 0f, 1f);
			}
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player other = enumerator.Current;
				if (!other.dead && ((other.team == player.team && player.team != 0) || player.whoAmI == other.whoAmI) && player.Distance(other.Center) <= 800f)
				{
					other.AddBuff(ModContent.BuffType<Mushy>(), 900, quiet: false);
				}
			}
		}
		return false;
	}
}
