using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class CorpusAvertor : RogueWeapon
{
	public override float StealthDamageMultiplier => 2.5f;

	public override float StealthKnockbackMultiplier => 2f;

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 44;
		base.Item.damage = 98;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 15);
		base.Item.useStyle = 1;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<CorpusAvertorProj>();
		base.Item.shootSpeed = 14f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		int lifeAmount = player.statLifeMax2 - player.statLife;
		damage.Base += (float)lifeAmount * 0.1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int dagger = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<CorpusAvertorStealth>(), damage, knockback, player.whoAmI);
			if (dagger.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[dagger].Calamity().stealthStrike = true;
			}
			player.statLife -= 6;
			if (Main.myPlayer == player.whoAmI)
			{
				player.HealEffect(-6);
			}
			if (player.statLife <= 0)
			{
				player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.CorpusAvertor").ToNetworkText(player.name)), 1000.0, 0);
			}
			return false;
		}
		return true;
	}
}
