using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MK2FlaskSummon : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Items/Weapons/Summon/FuelCellBundle";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 240;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		base.Projectile.velocity.Y += 0.2f;
		base.Projectile.rotation += 0.1f * (float)(base.Projectile.velocity.X > 0f).ToDirectionInt();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		for (int i = 0; i < Main.rand.Next(18, 21); i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Unit() * Main.rand.NextFloat(10f), 46, Main.rand.NextVector2Unit() * Main.rand.NextFloat(1f, 4f));
		}
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.Center);
		int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.UnitY * 6f, ModContent.ProjectileType<PlaguebringerMK2>(), base.Projectile.damage, 4f, base.Projectile.owner);
		if (Main.projectile.IndexInRange(p))
		{
			Main.projectile[p].originalDamage = base.Projectile.originalDamage;
		}
		int beeArrayIndex = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile pro = enumerator.Current;
			if (pro.owner == base.Projectile.owner && pro.type == ModContent.ProjectileType<PlaguebringerMK2>())
			{
				pro.ai[1] = beeArrayIndex;
				beeArrayIndex++;
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
