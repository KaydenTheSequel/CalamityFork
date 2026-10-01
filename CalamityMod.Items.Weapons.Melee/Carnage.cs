using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Carnage : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 54;
		base.Item.damage = 130;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 21;
		base.Item.useStyle = 1;
		base.Item.useTime = 21;
		base.Item.knockBack = 5.25f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(hitbox.TopLeft(), hitbox.Width, hitbox.Height, 5);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 120);
		if (target.life <= 0 && target.IsAnEnemy(allowStatues: false))
		{
			OnHitEffects(player, target, hit.Knockback);
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		if (target.statLife <= 0)
		{
			OnHitEffects(player, target, base.Item.knockBack);
		}
	}

	private void OnHitEffects(Player player, Entity target, float kb)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		SoundEngine.PlaySound(in SoundID.Item74, target.Center);
		for (int i = 0; i < 15; i++)
		{
			int idx = Dust.NewDust(target.position, target.width, target.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale = 0.5f;
				Main.dust[idx].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 25; j++)
		{
			int idx2 = Dust.NewDust(target.position, target.width, target.height, 5, 0f, 0f, 100, default(Color), 3f);
			Main.dust[idx2].noGravity = true;
			Dust obj2 = Main.dust[idx2];
			obj2.velocity *= 5f;
			idx2 = Dust.NewDust(target.position, target.width, target.height, 5, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[idx2];
			obj3.velocity *= 2f;
		}
		int bloodAmt = Main.rand.Next(6, 9);
		int bloodDamage = player.CalcIntDamage<MeleeDamageClass>(0.3f * (float)base.Item.damage);
		for (int k = 0; k < bloodAmt; k++)
		{
			Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
			Projectile.NewProjectile(source, target.Center, velocity, ModContent.ProjectileType<Blood>(), bloodDamage, kb, player.whoAmI);
		}
	}
}
