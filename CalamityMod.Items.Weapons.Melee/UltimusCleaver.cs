using System;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class UltimusCleaver : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 62;
		base.Item.damage = 130;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.rare = 8;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.useStyle = 1;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.autoReuse = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.shoot = ModContent.ProjectileType<UltimusCleaverDust>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		return false;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(323, 360);
		SoundEngine.PlaySound(in SoundID.Item14, target.Center);
		int onHitDamage = player.CalcIntDamage<MeleeDamageClass>(base.Item.damage);
		Projectile.NewProjectileDirect(base.Item.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), onHitDamage, 0f, player.whoAmI, target.whoAmI).DamageType = base.Item.DamageType;
		Vector2 dustVelocity = (target.rotation - (float)Math.PI / 2f).ToRotationVector2() * ((Vector2)(ref target.velocity)).Length();
		for (int i = 0; i < 40; i++)
		{
			Vector2 position = target.Center + Main.rand.NextVector2Circular(target.width / 2, target.width / 2);
			Dust.NewDustPerfect(position, 174, dustVelocity * Main.rand.NextFloat() - Vector2.UnitY * 18f, 200, default(Color), 1.7f).noGravity = true;
			Dust dust = Dust.NewDustPerfect(position, 174, dustVelocity * Main.rand.NextFloat() - Vector2.UnitY * 12f, 100, Color.Crimson * 0.5f, 0.8f);
			dust.noGravity = true;
			dust.fadeIn = 1f;
		}
		for (int j = 0; j < 20; j++)
		{
			Dust.NewDustPerfect(target.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(target.velocity.ToRotation()) * ((float)target.width / 3f), 174, dustVelocity * (0.6f + 0.6f * Main.rand.NextFloat()) - Vector2.UnitY * 3f, 0, default(Color), 2f).noGravity = true;
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		if (player.whoAmI != Main.myPlayer || player.Calamity().bladeArmEnchant || (player.itemAnimation != (int)((double)player.itemAnimationMax * 0.1) && player.itemAnimation != (int)((double)player.itemAnimationMax * 0.3) && player.itemAnimation != (int)((double)player.itemAnimationMax * 0.5) && player.itemAnimation != (int)((double)player.itemAnimationMax * 0.7) && player.itemAnimation != (int)((double)player.itemAnimationMax * 0.9)))
		{
			return;
		}
		float sparkXVel = 0f;
		float sparkYVel = 0f;
		float sparkXSpawn = 0f;
		float sparkYSpawn = 0f;
		if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
		{
			sparkYVel = -10.5f;
			if (player.direction == -1)
			{
				sparkXSpawn = -8f;
			}
		}
		if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
		{
			sparkYVel = -9f;
			sparkXVel = 3f;
			sparkXSpawn = ((player.direction == -1) ? 20f : 26f);
		}
		if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5))
		{
			sparkYVel = -6f;
			sparkXVel = 6f;
		}
		if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
		{
			sparkXVel = 9f;
			sparkYVel = -3f;
			sparkXSpawn = -4f;
			sparkYSpawn = -20f;
		}
		if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
		{
			sparkXVel = 10.5f;
			sparkYSpawn = 6f;
		}
		sparkXVel *= (float)player.direction;
		sparkYVel *= player.gravDir;
		sparkXSpawn *= (float)player.direction;
		sparkYSpawn *= player.gravDir;
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), Damage: (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo((float)base.Item.damage * 0.1f), X: (float)(hitbox.X + hitbox.Width / 2) + sparkXSpawn, Y: (float)(hitbox.Y + hitbox.Height / 2) + sparkYSpawn, SpeedX: sparkXVel, SpeedY: sparkYVel, Type: ModContent.ProjectileType<UltimusCleaverDust>(), KnockBack: 0f, Owner: player.whoAmI);
	}
}
