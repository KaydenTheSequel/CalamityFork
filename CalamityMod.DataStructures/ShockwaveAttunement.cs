using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

public class ShockwaveAttunement : Attunement
{
	public override float DamageMultiplier => (float)OmegaBiomeBlade.ShockwaveAttunement_BaseDamage / (float)OmegaBiomeBlade.BaseDamage;

	public ShockwaveAttunement()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		id = AttunementID.Shockwave;
		tooltipColor = new Color(71, 191, 71);
		tooltipColor2 = new Color(122, 213, 233);
	}

	public override void ApplyStats(Item item)
	{
		item.channel = true;
		item.noUseGraphic = true;
		item.useStyle = 5;
		item.shoot = ModContent.ProjectileType<EarthenTides>();
		item.shootSpeed = 12f;
		item.UseSound = null;
		item.noMelee = true;
	}

	public override void PassiveEffect(Player player, IEntitySource source, ref int UseTimer, ref bool Procced, Projectile projectile = null)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		if (UseTimer % 120 == 119)
		{
			int damage = (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo(OmegaBiomeBlade.ShockwaveAttunement_PassiveBaseDamage);
			Projectile.NewProjectile(source, player.Center, Vector2.Zero, ModContent.ProjectileType<EarthenTidesShockwave>(), damage, 10f, player.whoAmI, 2f);
			UseTimer++;
		}
	}
}
