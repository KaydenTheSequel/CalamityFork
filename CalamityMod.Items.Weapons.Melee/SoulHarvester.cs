using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SoulHarvester : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 62;
		base.Item.height = 64;
		base.Item.damage = 85;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.useStyle = 1;
		base.Item.useTurn = true;
		base.Item.knockBack = 7.5f;
		base.Item.UseSound = SoundID.Item71;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<SoulScythe>();
		base.Item.shootSpeed = 18f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 75);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
		target.AddBuff(39, 120);
		if (!((float)target.life <= (float)target.lifeMax * 0.15f))
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item14, target.Center);
		int onHitDamage = player.CalcIntDamage<MeleeDamageClass>(base.Item.damage);
		Projectile.NewProjectileDirect(base.Item.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<DirectStrike>(), onHitDamage, 0f, player.whoAmI, target.whoAmI).DamageType = base.Item.DamageType;
		for (int i = 0; i < 10; i++)
		{
			int plagueDust = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[plagueDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[plagueDust].scale = 0.5f;
				Main.dust[plagueDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 20; j++)
		{
			int plagueDust2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 89, 0f, 0f, 100, default(Color), 3f);
			Main.dust[plagueDust2].noGravity = true;
			Dust obj2 = Main.dust[plagueDust2];
			obj2.velocity *= 5f;
			plagueDust2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[plagueDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
		target.AddBuff(39, 120);
		if (!((float)target.statLife <= (float)target.statLifeMax * 0.15f))
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item14, target.Center);
		for (int i = 0; i < 10; i++)
		{
			int plagueDust = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[plagueDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[plagueDust].scale = 0.5f;
				Main.dust[plagueDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 20; j++)
		{
			int plagueDust2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 89, 0f, 0f, 100, default(Color), 3f);
			Main.dust[plagueDust2].noGravity = true;
			Dust obj2 = Main.dust[plagueDust2];
			obj2.velocity *= 5f;
			plagueDust2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 89, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[plagueDust2];
			obj3.velocity *= 2f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1327).AddIngredient<PlagueCellCanister>(10).AddIngredient(522, 20)
			.AddTile(134)
			.Register();
	}
}
