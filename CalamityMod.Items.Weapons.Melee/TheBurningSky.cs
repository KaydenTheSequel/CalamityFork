using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TheBurningSky : ModItem, ILocalizedModType, IModType
{
	private const int ProjectilesPerBarrage = 6;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 102;
		base.Item.height = 146;
		base.Item.damage = 147;
		base.Item.knockBack = 2.5f;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.useTime = 8;
		base.Item.useAnimation = 8;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.UseSound = SoundID.Item105;
		base.Item.shoot = ModContent.ProjectileType<BurningMeteor>();
		base.Item.shootSpeed = 14f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override bool RangedPrefix()
	{
		return true;
	}

	public override bool MeleePrefix()
	{
		return false;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item70, player.Center);
		float speed = ((Vector2)(ref velocity)).Length();
		for (int i = 0; i < 6; i++)
		{
			float randomSpeed = speed * Main.rand.NextFloat(0.7f, 1.4f);
			CalamityUtils.ProjectileRain(source, player.ClampedMouseWorld(), 290f, 130f, 850f, 1100f, randomSpeed, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}
}
