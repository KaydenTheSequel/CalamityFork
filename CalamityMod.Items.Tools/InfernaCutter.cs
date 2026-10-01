using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class InfernaCutter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 80;
		base.Item.height = 66;
		base.Item.damage = 110;
		base.Item.knockBack = 7f;
		base.Item.useTime = 8;
		base.Item.useAnimation = 12;
		base.Item.axe = 27;
		base.Item.tileBoost++;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
	}

	public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		hitbox = CalamityUtils.FixSwingHitbox(54f, 54f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AxeofPurity>().AddIngredient(547, 8).AddIngredient<EssenceofHavoc>(3)
			.AddTile(134)
			.Register();
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		if (player.whoAmI == Main.myPlayer && (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7) || player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9)))
		{
			float sparkYVel = 0f;
			float sparkXVel = 0f;
			float sparkYSpawn = 0f;
			float sparkXSpawn = 0f;
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
			{
				sparkYVel = -7f;
			}
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
			{
				sparkYVel = -6f;
				sparkXVel = 2f;
			}
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.5))
			{
				sparkYVel = -4f;
				sparkXVel = 4f;
			}
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
			{
				sparkYVel = -2f;
				sparkXVel = 6f;
			}
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
			{
				sparkXVel = 7f;
			}
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
			{
				sparkXSpawn = 26f;
			}
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.3))
			{
				sparkXSpawn -= 4f;
				sparkYSpawn -= 20f;
			}
			if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.1))
			{
				sparkYSpawn += 6f;
			}
			if (player.direction == -1)
			{
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.9))
				{
					sparkXSpawn -= 8f;
				}
				if (player.itemAnimation == (int)((double)player.itemAnimationMax * 0.7))
				{
					sparkXSpawn -= 6f;
				}
			}
			sparkYVel *= 1.5f;
			sparkXVel *= 1.5f;
			sparkXSpawn *= (float)player.direction;
			sparkYSpawn *= player.gravDir;
			int spark = Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), Damage: (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo((float)base.Item.damage * 0.2f), X: (float)(hitbox.X + hitbox.Width / 2) + sparkXSpawn, Y: (float)(hitbox.Y + hitbox.Height / 2) + sparkYSpawn, SpeedX: (float)player.direction * sparkXVel, SpeedY: sparkYVel * player.gravDir, Type: 504, KnockBack: 0f, Owner: player.whoAmI);
			if (spark.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[spark].DamageType = DamageClass.Melee;
			}
		}
		if (Main.rand.NextBool(4))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 6);
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (hit.Crit)
		{
			int boom = Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<FuckYou>(), base.Item.damage, base.Item.knockBack, player.whoAmI, 0f, 0.85f + Main.rand.NextFloat() * 1.15f);
			if (boom.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[boom].DamageType = DamageClass.Melee;
			}
		}
		target.AddBuff(323, 300);
	}
}
