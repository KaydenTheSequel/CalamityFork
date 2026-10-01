using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class WhirlwindAttunement : Attunement
{
	public override float DamageMultiplier => (float)OmegaBiomeBlade.WhirlwindAttunement_BaseDamage / (float)OmegaBiomeBlade.BaseDamage;

	public WhirlwindAttunement()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Whirlwind;
		tooltipColor = new Color(220, 105, 197);
		tooltipColor2 = new Color(132, 70, 232);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<SwordsmithsPride>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override void PassiveEffect(Player player, IEntitySource source, ref int UseTimer, ref bool Procced, Projectile projectile = null)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (Procced)
		{
			int damage = (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo(OmegaBiomeBlade.WhirlwindAttunement_PassiveBaseDamage);
			Vector2 velocity = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX) * 15f;
			Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<SwordsmithsPrideAstralBomber>(), damage, 0f, player.whoAmI);
			Procced = false;
		}
	}
}
