using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "ProfanedSword" })]
public class BrimstoneSword : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 52);
		base.Item.damage = 70;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = (base.Item.useTime = 28);
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 7.5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.shoot = ModContent.ProjectileType<BrimstoneSwordProj>();
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 10f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool? UseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.DamageType = DamageClass.MeleeNoSpeed;
			base.Item.noMelee = true;
		}
		else
		{
			base.Item.DamageType = DamageClass.Melee;
			base.Item.noMelee = false;
		}
		return base.UseItem(player);
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse != 2)
		{
			type = 0;
		}
	}

	public override void UseAnimation(Player player)
	{
		base.Item.noUseGraphic = false;
		if (player.altFunctionUse == 2)
		{
			base.Item.noUseGraphic = true;
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 300);
		Projectile.NewProjectile(source_ItemUse, target.Center, Vector2.Zero, ModContent.ProjectileType<Brimblast>(), base.Item.damage, base.Item.knockBack, Main.myPlayer);
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 300);
		Projectile.NewProjectile(source_ItemUse, target.Center, Vector2.Zero, ModContent.ProjectileType<Brimblast>(), base.Item.damage, base.Item.knockBack, Main.myPlayer);
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 235);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UnholyCore>(6).AddTile(134).Register();
	}
}
