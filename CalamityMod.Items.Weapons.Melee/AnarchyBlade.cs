using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class AnarchyBlade : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 114;
		base.Item.height = 122;
		base.Item.damage = 120;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 19;
		base.Item.useTime = 19;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 7.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
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
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 235);
		}
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		int lifeAmount = player.statLifeMax2 - player.statLife;
		damage.Base += (float)lifeAmount * 0.1f;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<BrimstoneBoom>(), base.Item.damage, base.Item.knockBack, Main.myPlayer);
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 300);
		if ((float)player.statLife <= (float)player.statLifeMax2 * 0.5f && Main.rand.NextBool(5) && !CalamityPlayer.areThereAnyDamnBosses && CalamityGlobalNPC.ShouldAffectNPC(target))
		{
			target.life = 0;
			target.HitEffect();
			target.active = false;
			target.NPCLoot();
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<BrimstoneBoom>(), base.Item.damage, base.Item.knockBack, Main.myPlayer);
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 300);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(426).AddIngredient<UnholyCore>(5).AddIngredient<CoreofCalamity>()
			.AddTile(134)
			.Register();
	}
}
