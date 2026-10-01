using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class SolsticeClaymore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 86;
		base.Item.damage = 300;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 16;
		base.Item.useStyle = 1;
		base.Item.useTime = 16;
		base.Item.useTurn = true;
		base.Item.knockBack = 6.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<SolsticeBeam>();
		base.Item.shootSpeed = 16f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		int dustType = (Main.dayTime ? Utils.SelectRandom<int>(Main.rand, 6, 259, 158) : Utils.SelectRandom<int>(Main.rand, 173, 27, 234));
		if (Main.rand.NextBool(4))
		{
			int dust = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, dustType);
			Main.dust[dust].noGravity = true;
		}
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (Main.dayTime)
		{
			target.AddBuff(189, 300);
		}
		else
		{
			target.AddBuff(ModContent.BuffType<Nightwither>(), 300);
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		if (!Main.dayTime)
		{
			target.AddBuff(ModContent.BuffType<Nightwither>(), 300);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(723).AddIngredient<AstralBar>(20).AddIngredient(3467, 5)
			.AddIngredient<GalacticaSingularity>(5)
			.AddTile(412)
			.Register();
	}
}
